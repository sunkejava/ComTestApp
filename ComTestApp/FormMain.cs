using AllIn.Core.Form.Helpers;
using AllIn.Core.Util;
using ComTestApp.Common;
using ComTestApp.Entitys;
using ComTestApp.ExControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComTestApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            //利用反射设置DataGridView的双缓冲
            Type dgvType = this.Grid_Data.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.Grid_Data, true, null);
        }

        #region 公共变量

        private string hardType = "C1";//默认硬件类型
        private string SerialPortName = "";//当前串口名称
        string[] PortNames; //端口数组
        int hardNum = 1;//设备数量
        CheckEnum.CheckNum checkNum = CheckEnum.CheckNum.Single;//检测次数
        CheckEnum.CheckModel checkModel = CheckEnum.CheckModel.Order;//检测模式
        DateTime startTime,endTime;//开始时间、结束时间
        int portCount = 0, portSuc = 0;//总端口数及成功端口数
        volatile bool stop = false;//是否停止
        //bool IsRead = false;//是否读取
        AppConfig appConfig = null;
        ComPort comport = new ComPort(3500);
        #endregion

        #region 窗口控件事件

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                #region 控件初始化
                InitColInfo();
                Cmb_HardList.DataSource = new List<string>() { "C1", "A1" };
                LoadComInfo();
                setDataGridStyle();
                #endregion

                #region 数据库初始化
                this.BaseEntityHelp().CheckTableExist(new UsbPortEntity());
                this.BaseEntityHelp().CheckTableExist(new AppConfig());
                #endregion

                #region 加载配置
                var ags = ExtensionHelp.SelectEntity(new AppConfig() { Id = 1 });
                if (ags.Count > 0) appConfig = ags[0];
                if (appConfig == null)
                {
                    appConfig = new AppConfig()
                    {
                        comIndex = 0,
                        hardTypeIndex = 0,
                        hardNum = 1,
                        checkNumIndex = 0,
                        checkModelIndex = 0,
                        waitNum = 2500
                    };
                    this.BaseEntityHelp().Insert(appConfig);
                    appConfig = ExtensionHelp.SelectEntity(new AppConfig() { Id = 1 }).First();
                }
                if (Cmb_PortList.Items.Count > 0) { Cmb_PortList.SelectedIndex = appConfig.comIndex; SerialPortName = Cmb_PortList.SelectedValue.ToString(); }
                Text_Number.Value = appConfig.hardNum;
                hardNum = (int)appConfig.hardNum;
                Cmb_CheckNum.SelectedIndex = appConfig.checkNumIndex;
                Cmb_CheckModel.SelectedIndex = appConfig.checkModelIndex;
                Num_Waiting.Value = appConfig.waitNum;
                Cmb_HardList.SelectedIndex = appConfig.hardTypeIndex;
                hardType = Cmb_HardList.SelectedValue.ToString();
                comport.waitingNum = (int)appConfig.waitNum;  
                InitHardView();
                LoadHardWareInfo(1);
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.ToLog("初始化加载失败,原因为：" + ex);                
            }
            
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BaseEntityHelp().Update(appConfig,appConfig.Id.ToString());
            if (MessageBox.Show("是否确认退出程序", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
            this.Dispose();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;//用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }

        #region u盘属性
        public const int WM_DEVICECHANGE = 0x219;//U盘插入后，OS的底层会自动检测到，然后向应用程序发送“硬件设备状态改变“的消息
        public const int DBT_DEVICEARRIVAL = 0x8000;  //就是用来表示U盘可用的。一个设备或媒体已被插入一块，现在可用。
        public const int DBT_CONFIGCHANGECANCELED = 0x0019;  //要求更改当前的配置（或取消停靠码头）已被取消。
        public const int DBT_CONFIGCHANGED = 0x0018;  //当前的配置发生了变化，由于码头或取消固定。
        public const int DBT_CUSTOMEVENT = 0x8006; //自定义的事件发生。 的Windows NT 4.0和Windows 95：此值不支持。
        public const int DBT_DEVICEQUERYREMOVE = 0x8001;  //审批要求删除一个设备或媒体作品。任何应用程序也不能否认这一要求，并取消删除。
        public const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;  //请求删除一个设备或媒体片已被取消。
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;  //一个设备或媒体片已被删除。
        public const int DBT_DEVICEREMOVEPENDING = 0x8003;  //一个设备或媒体一块即将被删除。不能否认的。
        public const int DBT_DEVICETYPESPECIFIC = 0x8005;  //一个设备特定事件发生。
        public const int DBT_DEVNODES_CHANGED = 0x0007;  //一种设备已被添加到或从系统中删除。
        public const int DBT_QUERYCHANGECONFIG = 0x0017;  //许可是要求改变目前的配置（码头或取消固定）。
        public const int DBT_USERDEFINED = 0xFFFF;  //此消息的含义是用户定义的
        #endregion

        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == WM_DEVICECHANGE)
                {
                    switch (m.WParam.ToInt32())
                    {
                        case WM_DEVICECHANGE:
                            break;
                        case DBT_DEVICEARRIVAL://U盘插入
                            //if (stop) return;
                            //IsRead = true;
                            //LogHelper.ToLog("U盘插入");
                            break;
                        case DBT_CONFIGCHANGECANCELED:
                            break;
                        case DBT_CONFIGCHANGED:
                            break;
                        case DBT_CUSTOMEVENT:
                            break;
                        case DBT_DEVICEQUERYREMOVE:
                            break;
                        case DBT_DEVICEQUERYREMOVEFAILED:
                            break;
                        case DBT_DEVICEREMOVECOMPLETE: //U盘卸载
                            //LogHelper.ToLog("U盘拔出");
                            //IsRead = false;
                            break;
                        case DBT_DEVICEREMOVEPENDING:
                            break;
                        case DBT_DEVICETYPESPECIFIC:
                            break;
                        case DBT_DEVNODES_CHANGED:
                            break;
                        case DBT_QUERYCHANGECONFIG:
                            break;
                        case DBT_USERDEFINED:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                {
                    LogHelper.ToLog("判断U盘状态:" + ex);
                }
            }
            //拦截双击标题栏、移动窗体的系统消息
            if (m.Msg != 0xA3 && m.Msg != 0x0003 && m.WParam != (IntPtr)0xF012)
            {
                base.WndProc(ref m);
            }
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_HardList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(appConfig != null) appConfig.hardTypeIndex = Cmb_HardList.SelectedIndex;
            if (Cmb_PortList != null && Cmb_PortList.SelectedValue != null)
            {
                hardType = Cmb_HardList.SelectedValue.ToString();
                Text_Number.Enabled = hardType.Equals("A1");
                Text_Number.ReadOnly = hardType.Equals("C1");
                hardNum = (int)Text_Number.Value;//ConstValue.GetBoxCodesDic(hardType, hardNum.ToString()).Count;
                if (appConfig != null) appConfig.hardNum = hardNum;
                InitHardView();
                LoadHardWareInfo(1);
            }
        }

        /// <summary>
        /// 选择串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_PortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (appConfig != null) appConfig.comIndex = Cmb_PortList.SelectedIndex;
            SerialPortName = Cmb_PortList.SelectedValue.ToString();            
        }

        /// <summary>
        /// 设备数量变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_Number_ValueChanged(object sender, EventArgs e)
        {
            if (appConfig != null) appConfig.hardNum = Text_Number.Value;
            hardNum = (int)Text_Number.Value;
            InitHardView();
            LoadHardWareInfo(1);            
        }

        /// <summary>
        /// 页夹变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHardWareInfo(tabControl1.SelectedIndex + 1);
        }

        /// <summary>
        /// 检测次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_CheckNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(appConfig != null) appConfig.checkNumIndex = Cmb_CheckNum.SelectedIndex;
            if (Cmb_CheckNum.Text.Equals("单次"))
            {
                checkNum = CheckEnum.CheckNum.Single;
            }
            else
            {
                checkNum = CheckEnum.CheckNum.Continuity;
            }            
        }
        /// <summary>
        /// 检测模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_CheckModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(appConfig != null) appConfig.checkModelIndex = Cmb_CheckModel.SelectedIndex;
            switch (Cmb_CheckModel.Text.Trim())
            {
                case "顺序":
                    checkModel = CheckEnum.CheckModel.Order;
                    break;
                case "随机":
                    checkModel = CheckEnum.CheckModel.Random;
                    break;
                case "冒烟":
                    checkModel = CheckEnum.CheckModel.Bubbling;
                    break;
                default:
                    break;
            }
        }

        ManualResetEvent mre = new ManualResetEvent(false);
        Thread th = null;
        /// <summary>
        /// 开始暂停事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            //检测
            if (Cmb_CheckNum.Text.IsEmpty() || Cmb_CheckModel.Text.IsEmpty())
            {
                MessageBox.Show("请确认是否选择检测次数或模式！");
                return;
            }
            if (Btn_Start.Text.Trim().Equals("开始") || Btn_Start.Text.Trim().Equals("继续"))
            {
                if (Btn_Start.Text.Trim().Equals("开始"))
                {
                    InitHardView();
                    portCount = portSuc = 0;
                    //switch (hardNum)
                    //{
                    //    case 1:
                    //        while (tabPage1.Controls.Count == 0) { await Task.Delay(100); }
                    //        break;
                    //    case 2:
                    //        while (tabPage2.Controls.Count == 0) { await Task.Delay(100); }
                    //        break;
                    //    case 3:
                    //        while (tabPage3.Controls.Count == 0) { await Task.Delay(100); }
                    //        break;
                    //    case 4:
                    //        while (tabPage4.Controls.Count == 0) { await Task.Delay(100); }
                    //        break;
                    //    default:
                    //        break;
                    //}
                    startTime = DateTime.Now;
                    if (th != null && !th.IsAlive)
                    {
                        th.Abort();
                    }
                    stop = false;
                    //开始事件
                    th = new Thread(CheckAllInfo);
                    //th.IsBackground = true;                    
                    mre.Set();
                    th.Start();
                }
                else
                {
                    //暂停后继续开始事件
                    mre.Set();
                }
                Btn_Start.Text = "暂停";
                Btn_End.Enabled = true;                
            }
            else
            {
                Btn_End.Enabled = false;
                Btn_Start.Text = "继续";
                //暂停事件
                mre.Reset();
            }
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_End_Click(object sender, EventArgs e)
        {
            try
            {
                Btn_Start.Text = "开始";
                endTime = DateTime.Now;
                if (th.IsAlive)
                {
                    stop = true;
                    mre.Set();                    
                    //th.Abort();
                }
                Btn_End.Enabled = false;
            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                {
                    LogHelper.ToLog("终止线程异常，" + ex);
                }
            }
            
        }

        private void Btn_LoadComPort_Click(object sender, EventArgs e)
        {
            LoadComInfo();
        }

        private void Num_Waiting_ValueChanged(object sender, EventArgs e)
        {
            if (Num_Waiting.Value < Num_Waiting.Minimum)
            {
                Num_Waiting.Value = Num_Waiting.Minimum;
            }
            if (appConfig != null) appConfig.waitNum = Num_Waiting.Value;
            comport.waitingNum = (int)Num_Waiting.Value;            
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            FormSearch frs = new FormSearch();
            frs.ShowDialog();
        }

        private async void Btn_Send_Click(object sender, EventArgs e)
        {
            string seletxt = Cmb_Listcod.Text;
            await Task.Run(() =>
            {
                if (seletxt.IsEmpty()) return;
                startTime = DateTime.Now;
                var boxCode = seletxt.Substring(0, 2);
                var banCode = seletxt.Substring(2, 2);
                var portId = seletxt.Substring(4, 2);
                var ban = int.Parse(banCode) % 3;
                var banStr = ban == 1 ? "下层" : ban == 2 ? "中层" : "上层";
                Lb_Msg.DelegateControl(() =>
                {
                    Lb_Msg.Text = string.Format("正在检测箱子{0}板子{1}的端口{2}是否正常,请稍后！", boxCode, banCode, portId);
                    Lb_Msg.Refresh();
                });
                var usps = Grid_Data.DataSource.ToSerializeObject().ToDeserializeObject<List<UsbPortEntity>>();
                var portEntity = usps[0];
                portEntity.BoxId = boxCode;
                portEntity.CardId = banCode;
                portEntity.PortId = portId;
                portEntity.CardName = banStr;
                UpdateDevieInfo(portEntity);
                var IsSuc = comport.ReadCommPort(portEntity);
                portEntity.PortStatus = IsSuc ? "成功" : "失败";
                endTime = DateTime.Now;
                Lb_Msg.DelegateControl(
                    () => {
                        Lb_Msg.Text = string.Format("箱子{0}板子{1}的端口{2},共耗时{3}！", boxCode, banCode, portId + "检测" + portEntity.PortStatus, ExtensionHelp.DateDiff(startTime, endTime));
                        Lb_Msg.Refresh();
                    });
                UpdateDevieInfo(portEntity);
            });
        }

        private async void Cmb_Listcod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seletxt = Cmb_Listcod.Text;
            await Task.Run(() =>
            {
                if (seletxt.IsEmpty()) return;
                startTime = DateTime.Now;
                var boxCode = seletxt.Substring(0, 2);
                var banCode = seletxt.Substring(2, 2);
                var portId = seletxt.Substring(4, 2);
                Lb_Msg.DelegateControl(() => 
                { 
                    Lb_Msg.Text = string.Format("正在检测箱子{0}板子{1}的端口{2}是否正常,请稍后！", boxCode, banCode, portId);
                    Lb_Msg.Refresh();
                });
                var usps = Grid_Data.DataSource.ToSerializeObject().ToDeserializeObject<List<UsbPortEntity>>();
                var portEntity = usps.Find(o => o.BoxId == boxCode && o.CardId == banCode && o.PortId == portId);
                UpdateDevieInfo(portEntity);
                var IsSuc = comport.ReadCommPort(portEntity);
                portEntity.PortStatus = IsSuc ? "成功" : "失败";
                endTime = DateTime.Now;
                Lb_Msg.DelegateControl(
                    () => { 
                        Lb_Msg.Text = string.Format("箱子{0}板子{1}的端口{2},共耗时{3}！", boxCode, banCode, portId +"检测"+ portEntity.PortStatus, ExtensionHelp.DateDiff(startTime, endTime));
                        Lb_Msg.Refresh();
                    });
                UpdateDevieInfo(portEntity);
            });
        }

        #endregion

        #region 自定义方法
        /// <summary>
        /// 加载串口信息
        /// </summary>
        private void LoadComInfo()
        {
            PortNames = SerialPort.GetPortNames();
            List<string> list = new List<string>();
            foreach (string pName in PortNames)
            { if(!pName.Equals("COM1")) list.Add(pName); }
            Cmb_PortList.DataSource = list;
            if (Cmb_PortList.Items.Count > 0) 
            {
                Cmb_PortList.SelectedIndex = 0; 
                SerialPortName = Cmb_PortList.SelectedValue.ToString();
            }
            hardNum = (int)Text_Number.Value;
            if (appConfig != null) appConfig.hardNum = hardNum;
        }

        /// <summary>
        /// 加载设备信息
        /// </summary>
        private void LoadHardWareInfo(int boxNum)
        {
            TabPage tp = null;
            switch (boxNum)
            {
                case 1:
                    tp = tabPage1;
                    break;
                case 2:
                    tp = tabPage2;
                    break;
                case 3:
                    tp = tabPage3;
                    break;
                case 4:
                    tp = tabPage4;
                    break;
                default:
                    break;
            }
            tabControl1.DelegateControl(() => { tabControl1.SelectedTab = tp; });
            if (SerialPortName.IsEmpty() && Cmb_PortList.Items.Count > 0) SerialPortName = Cmb_PortList.Text;
            //设备箱体
            Dictionary<string, string> BoxDict = ConstValue.GetBoxCodesDic(hardType, hardNum.ToString());
            string boxValue = "";
            foreach (var item in BoxDict)
            {
                string val = AppedNumber(boxNum  - 1);
                if (item.Key.Equals(val))
                {
                    boxValue = item.Value;
                }
            }
            //抽屉层级
            Dictionary<string, string> CardDict = ConstValue.GetDrawerCodeDic(hardType, boxValue);
            CardDict = CardDict.OrderBy(p => p.Key).ToDictionary(p => p.Key,o => o.Value);  
            //接口信息
            Dictionary<string, string> UsbPortDict = ConstValue.GetPortCodeDic(hardType);
            List<UsbPortEntity> portEntitys = new List<UsbPortEntity>();
            int i = 1;
            List<string> codeStrs = new List<string>();
            codeStrs.Add(string.Empty);
            //foreach (var BoxItem in BoxDict)
            //{
            foreach (var CardItem in CardDict)
                {
                    foreach (var item in UsbPortDict)
                    {
                        portEntitys.Add(new UsbPortEntity() { Num = i,BatchNumber = DateTime.Now.ToString("yyyyMMddHHmmss"),BoxId = boxValue, CardId = CardItem.Key,CardName = CardItem.Value,PortId = item.Value,PortStatus = "未检测",SerialPortName = SerialPortName,HardType = hardType });
                        codeStrs.Add(boxValue + CardItem.Key + item.Value);
                        i++;
                    }
                }
            //}    
            Grid_Data.DelegateControl(() => 
            {
                Grid_Data.DataSource = new BindingList<UsbPortEntity>(portEntitys);
            });
            Cmb_Listcod.DelegateControl(() => { Cmb_Listcod.DataSource = codeStrs; });
        }
        /// <summary>
        /// 初始化DataGrid列
        /// </summary>
        private void InitColInfo()
        {
            Grid_Data.Columns.Clear();
            var fieldInfos = (new UsbPortEntity()).GetFieldInfo();
            foreach (var item in fieldInfos)
            {
                if (!item.IsCheckBox)
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    if (item.IsDate) col.DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm:ss" };
                    col.DataPropertyName = item.ColumnName;
                    col.FillWeight = 85.2792F;
                    col.HeaderText = item.Notes;
                    col.Name = item.ColumnName;
                    col.Visible = item.VisibleForDv;
                    Grid_Data.Columns.Add(col);
                }
                else
                {
                    DataGridViewCheckBoxColumn ck = new DataGridViewCheckBoxColumn();
                    ck.FillWeight = 50.76142F;
                    ck.HeaderText = item.Notes;
                    ck.Name = item.ColumnName;
                    ck.DataPropertyName = item.ColumnName;
                    ck.ReadOnly = true;
                    ck.Visible = item.VisibleForDv;
                    Grid_Data.Columns.Add(ck);
                }
            }
        }

        /// <summary>
        /// 加载设备端口控件
        /// </summary>
        private void InitHardView()
        {
            clearControl();
            tabPage2.Parent = tabPage3.Parent = tabPage4.Parent = null;
            if (hardType.Equals("A1"))
            {
                switch (hardNum)
                {
                    case 1:
                        tabPage1.Controls.Add(GenerateControl(1));
                        tabPage1.Text = hardType + "设备1信息";
                        break;
                    case 2:
                        tabPage2.Parent = tabPage1.Parent;
                        tabPage1.Controls.Add( GenerateControl(1));
                        tabPage2.Controls.Add( GenerateControl(2));
                        tabPage1.Text = hardType + "设备1信息";
                        tabPage2.Text = hardType + "设备2信息";
                        break;
                    case 3:
                        tabPage3.Parent =  tabPage2.Parent = tabPage1.Parent;
                        tabPage1.Controls.Add( GenerateControl(1));
                        tabPage2.Controls.Add( GenerateControl(2));
                        tabPage3.Controls.Add( GenerateControl(3));
                        tabPage1.Text = hardType + "设备1信息";
                        tabPage2.Text = hardType + "设备2信息";
                        tabPage3.Text = hardType + "设备3信息";
                        break;
                    case 4:
                        tabPage4.Parent = tabPage3.Parent = tabPage2.Parent = tabPage1.Parent;
                        tabPage1.Controls.Add( GenerateControl(1));
                        tabPage2.Controls.Add( GenerateControl(2));
                        tabPage3.Controls.Add( GenerateControl(3));
                        tabPage4.Controls.Add( GenerateControl(4));
                        tabPage1.Text = hardType + "设备1信息";
                        tabPage2.Text = hardType + "设备2信息";
                        tabPage3.Text = hardType + "设备3信息";
                        tabPage4.Text = hardType + "设备4信息";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                tabPage1.Text = hardType + "设备信息";
                tabPage1.Controls.Add( GenerateControl());
            }
            Lb_Msg.DelegateControl(() => {
                Lb_Msg.Text = string.Format("当前批次【{0}】已检测耗时{1}，共计检测端口{2}个，成功率为{3}%", "xxx", "xx秒", "xxx", "xx");
                Lb_Msg.Refresh();
            });
            tabControl1.Refresh();
        }

        private void clearControl()
        {
            var ctrl = tabPage1.Controls.OfType<Control>().Where(o => o.Name.Contains("PanelMain_")).FirstOrDefault();
            if (ctrl != null)
            {
                tabPage1.Controls.Remove(ctrl);
                tabPage1.Invalidate();
            }
            var ctrl2 = tabPage2.Controls.OfType<Control>().Where(o => o.Name.Contains("PanelMain_")).FirstOrDefault();
            if (ctrl2 != null)
            {
                tabPage2.Controls.Remove(ctrl2);
                tabPage2.Invalidate();
            }
            var ctrl3 = tabPage3.Controls.OfType<Control>().Where(o => o.Name.Contains("PanelMain_")).FirstOrDefault();
            if (ctrl3 != null)
            {
                tabPage3.Controls.Remove(ctrl3);
                tabPage3.Invalidate();
            }
            var ctrl4 = tabPage4.Controls.OfType<Control>().Where(o => o.Name.Contains("PanelMain_")).FirstOrDefault();
            if (ctrl4 != null)
            {
                tabPage4.Controls.Remove(ctrl4);
                tabPage4.Invalidate();
            }
        }

        /// <summary>
        /// 创建group
        /// </summary>
        /// <returns></returns>
        private Panel GenerateControl(int hardNum = 1)
        {
            Lb_Msg.DelegateControl(() =>
            {
                Lb_Msg.Text = "创建设备{" + hardNum + "}控件中，请稍后......";
                Lb_Msg.Refresh();
            }) ;
            
                //创建承载控件GroupBox
                Panel gropBox = new Panel()
                {
                    Name = "PanelMain_" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    Text = hardType + "设备端口显示",
                    Width = tabPage1.Width - 10,
                    Height = tabPage1.Height - 10,
                    Left = 5,
                    Top = 5
                };
            //Console.WriteLine("默认page大小为:1408,422");
            //Console.WriteLine(string.Format("Page的大小为{0}，group的大小为{1}", tabPage1.Size, gropBox.Size));
                bool IsA1 = hardType.Equals("A1");
                for (int i = 1; i <= (IsA1 ? 21 * 3 : 20); i++)
                {
                    int colIndex = IsA1 ? i % 21 == 0 ? 21 : i % 21 : (i - i % 5) / 5 + (i % 5 > 0 ? 1 : 0);
                    int rowIndex = IsA1 ? (i - (i % 21)) / 21 + (i % 21 > 0 ? 1 : 0) : i % 5 == 0 ? 5 : i % 5;
                //Console.WriteLine(string.Format("数字{0}的列为{1}行为{2}", i, colIndex, rowIndex));
                int sizeW = 55;
                ExLabel lbport = new ExLabel()
                {
                    Name = "lb_" + hardType + "_" + hardNum + "_" + (IsA1 ? colIndex.ToString() + "_" + rowIndex.ToString() : (i).ToString()),
                    Text = "端口" + AppedNumber(IsA1 ? colIndex : 5 * (colIndex - 1) + rowIndex),
                    Width = sizeW,
                    Height = sizeW,
                    Radius = 30,
                    Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                    UseMnemonic = false,
                    //FlatStyle = FlatStyle.Flat,
                    ControlState = ControlState.Normal,
                    RightToLeft  = RightToLeft.No,
                    RoundStyle = RoundStyle.All,
                    Left = IsA1 ? (gropBox.Width - (sizeW * 21)) / 22 * colIndex + sizeW * (colIndex - 1) : (gropBox.Width - (sizeW * 4)) / 5 * colIndex + sizeW * (colIndex - 1),
                    Top = IsA1 ? (gropBox.Height - (sizeW * 3)) / 4 * rowIndex + sizeW * (rowIndex - 1) : (gropBox.Height - (sizeW * 5)) / 6 * rowIndex + sizeW * (rowIndex - 1),
                    BaseColor = System.Drawing.SystemColors.AppWorkspace,
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.MiddleCenter
                 };
                //Console.WriteLine(string.Format("控件{0}的左边距为{1}上边距为{2}", lbport.Name, lbport.Left, lbport.Top));
                gropBox.Controls.Add(lbport);
                }
                return gropBox;
        }

        private string AppedNumber(int n)
        {
            switch (n.ToString().Length)
            {
                case 1:
                    return "0" + n.ToString();
                case 2:
                    return n.ToString();
                default:
                    return n.ToString();
            }
        }

        /// <summary>
        /// 更新标签
        /// </summary>
        /// <param name="batchNum">批次号</param>
        /// <param name="timeStr">耗时</param>
        /// <param name="portCount">总测试的端口数</param>
        /// <param name="ratioStr">成功比率</param>
        private void UpdateTakeUpInfo(string batchNum,string timeStr,string portCount,string ratioStr)
        {
            Lb_Msg.DelegateControl(() =>
            {
                Lb_Msg.Text = string.Format("当前批次【{0}】已检测耗时{1}，共计检测端口{2}个，成功率为{3}%", batchNum, timeStr, portCount, ratioStr);
                Lb_Msg.Refresh();
            });
        }
        #endregion

        #region 检测相关

        /// <summary>
        /// 全部检测
        /// </summary>
        /// <returns></returns>
        private void CheckAllInfo()
        {
            try
            {
                bool IsResult = false;
                ToDoThing:
                        {
                            if (stop)
                            {
                                return;
                            }
                            mre.WaitOne();
                            switch (checkModel)
                            {
                                case CheckEnum.CheckModel.Order:
                                    IsResult = OrderOfSend();
                                    break;
                                case CheckEnum.CheckModel.Random:
                                    IsResult = RandomAllOfSend();
                                    break;
                                case CheckEnum.CheckModel.Bubbling:
                                    IsResult = RandomAnyOfSend();
                                    break;
                                default:
                                    break;
                            }
                        }
                
                while (IsResult)
                {
                    //检测次数处理
                    if (checkNum == CheckEnum.CheckNum.Continuity)
                    {
                        switch (hardNum)
                        {
                            case 1:
                                UpdateAllStyle(tabPage1);
                                break;
                            case 2:
                                UpdateAllStyle(tabPage1);
                                UpdateAllStyle(tabPage2);
                                break;
                            case 3:
                                UpdateAllStyle(tabPage1);
                                UpdateAllStyle(tabPage2);
                                UpdateAllStyle(tabPage3);
                                break;
                            case 4:
                                UpdateAllStyle(tabPage1);
                                UpdateAllStyle(tabPage2);
                                UpdateAllStyle(tabPage3);
                                UpdateAllStyle(tabPage4);
                                break;
                            default:
                                break;
                        }
                        LoadHardWareInfo(1);
                        //CheckAllInfo();
                        goto ToDoThing;
                    }
                    else
                    {
                        stop = true;
                        //DelegateUpEnd(false, "结束");
                        Btn_End.DelegateControl(() =>
                        {
                            Btn_End.Text = "结束";
                            Btn_End.Enabled = false;
                        });
                        Btn_Start.DelegateControl(() =>
                        {
                            Btn_Start.Text = "开始";
                            Btn_Start.Enabled = true;
                        });
                        break;
                    }
                }
                stop = false;
            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                {
                    LogHelper.ToLog("全部检测出现异常,原因为：" + ex);
                }
            }
        }

        /// <summary>
        /// 更新控件样式
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Boolean UpdateDevieInfo(UsbPortEntity entity)
        {
            return GetLbControl(entity);
        }

        /// <summary>
        /// 获取控件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool GetLbControl(UsbPortEntity entity)
        {
            var typeNum = int.Parse(entity.BoxId);
            TabPage tp = null;
            switch (typeNum)
            {
                case 0:
                    tp = tabPage1;
                    break;
                case 1:
                    tp = tabPage2;
                    break;
                case 2:
                    tp = tabPage3;
                    break;
                case 3:
                    tp = tabPage4;
                    break;
                default:
                    break;
            }
            if (tp != null && tp.Controls.Count > 0)
            {
                ExLabel ct = null;
                if (hardType.Equals("A1"))
                {
                    int cId = int.Parse(entity.CardId) % 3;
                    cId = cId == 0 ? 3 : cId;
                    string cardId = (4-cId).ToString();
                    ct = tp.Controls[0].Controls.OfType<Control>().Where(o => o.Name.Split('_')[1].Equals(entity.HardType) && o.Name.Split('_')[2].Equals((int.Parse(entity.BoxId) + 1).ToString()) && o.Name.Split('_')[3].Equals(int.Parse(entity.PortId).ToString()) && o.Name.Split('_')[4].Equals(cardId)).FirstOrDefault() as ExLabel;
                }
                else
                {
                    ct = tp.Controls[0].Controls.OfType<Control>().Where(o => o.Name.Split('_')[1].Equals(entity.HardType) && o.Name.Split('_')[2].Equals((int.Parse(entity.BoxId) + 1).ToString()) && o.Name.Split('_')[3].Equals(int.Parse(entity.PortId).ToString())).FirstOrDefault() as ExLabel;
                }
                if (ct != null)
                {
                    if (entity.PortStatus.Equals("未检测"))
                    {
                        ct.DelegateControl(() => 
                        {
                            ct.BaseColor = Color.Gold;
                            //cn.BackColor = Color.Gold;
                            ct.Refresh();
                        });
                        
                        return true;
                    }
                    if (entity.PortStatus.Equals("成功"))
                    {
                        ct.DelegateControl(() =>
                        {
                            ct.BaseColor = Color.ForestGreen;
                            //cn.BackColor = Color.ForestGreen;
                            ct.Refresh();
                        });
                    }
                    else
                    {
                        ct.DelegateControl(() =>
                        {
                            ct.BaseColor = Color.IndianRed;
                            //cn.BackColor = Color.IndianRed;
                            ct.Refresh();
                        });
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新所有控件的样式为原始样式
        /// </summary>
        private void UpdateAllStyle(TabPage pg)
        {
            foreach (Control item in pg.Controls)
            {
                if (item is Panel && item.Name.Contains("PanelMain_"))
                {
                    foreach (Control citem in (item as Panel).Controls)
                    {
                        if (citem.Name.Contains("lb_") && citem.Text.Contains("端口"))
                        {
                            if (citem is ExLabel)
                            {
                                ((ExLabel)citem).BaseColor = System.Drawing.SystemColors.AppWorkspace;
                            }
                            else
                            {
                                citem.BackColor = System.Drawing.SystemColors.AppWorkspace;
                            }
                        }
                    }
                }
               
            }
        }

        /// <summary>
        /// 顺序检测
        /// </summary>
        private bool OrderOfSend()
        {
            try
            {
                //设备箱体
                Dictionary<string, string> BoxDict = ConstValue.GetBoxCodesDic(hardType, hardNum.ToString());
                Color defaultColor = Color.Black;
                foreach (var boxItem in BoxDict)
                {
                    LoadHardWareInfo(int.Parse(boxItem.Value) + 1);
                    foreach (DataGridViewRow item in Grid_Data.Rows)
                    {
                        if (stop)
                        {
                            return false;
                        }
                        mre.WaitOne();
                        Grid_Data.DelegateControl(() => 
                            {
                                Grid_Data.CurrentCell = item.Cells["Num"];
                            });
                        UsbPortEntity portEntity = item.DataBoundItem.ToSerializeObject().ToDeserializeObject<UsbPortEntity>();
                        UpdateDevieInfo(portEntity);
                        portEntity.StartTime = DateTime.Now;
                        item.Cells["StartTime"].Value = portEntity.StartTime;
                        item.Cells["BoxId"].Value = boxItem.Value;
                        portEntity.BoxId = boxItem.Value;
                        defaultColor = item.DefaultCellStyle.BackColor;
                        item.DefaultCellStyle.BackColor = Color.Gold;
                        bool IsSuc = comport.ReadCommPort(portEntity);
                        //Task.Delay((int)Num_Waiting.Value / 2);
                        endTime = DateTime.Now;
                        portEntity.EndTime = endTime;
                        item.Cells["EndTime"].Value = endTime;
                        //更新状态
                        portEntity.PortStatus = IsSuc ? "成功" : "失败";
                        item.Cells["PortStatus"].Value = portEntity.PortStatus;
                        //更新控件样式
                        UpdateDevieInfo(portEntity);
                        //插入数据库
                        //this.BaseEntityHelp().Insert(portEntity);
                        portCount++;
                        if (IsSuc) portSuc++;
                        UpdateTakeUpInfo(portEntity.BatchNumber, ExtensionHelp.DateDiff(startTime, endTime), portCount.ToString(), (portSuc * 1.0 / portCount * 100).ToString("0.0"));
                        item.DefaultCellStyle.BackColor = defaultColor;
                        //IsRead = false;
                    }
                    BatchSaveData();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                {
                    LogHelper.ToLog("顺序检测异常，原因为：" + ex);
                }
                return false;
            }
        }

        /// <summary>
        /// 随机检测所有
        /// </summary>
        private bool RandomAllOfSend()
        {
            try
            {
                Dictionary<string, string> BoxDict = ConstValue.GetBoxCodesDic(hardType, hardNum.ToString());
                Color defaultColor = Color.Black;               
                foreach (var boxItem in BoxDict)
                {
                    List<int> rlc = new List<int>();
                    LoadHardWareInfo(int.Parse(boxItem.Value) + 1);
                    while (rlc.Count < Grid_Data.Rows.Count)
                    {
                        if (stop)
                        {
                            return false;
                        }
                        mre.WaitOne();
                        Random rdm = new Random();
                        int i = rdm.Next(0, Grid_Data.Rows.Count);
                        while (rlc.Contains(i))
                        {
                            i = rdm.Next(0, Grid_Data.Rows.Count);
                        }
                        rlc.Add(i);
                        Grid_Data.DelegateControl(() => { Grid_Data.CurrentCell = Grid_Data.Rows[i].Cells["Num"]; });
                        Grid_Data.Rows[i].Cells["StartTime"].Value = DateTime.Now;
                        Grid_Data.Rows[i].Cells["BoxId"].Value = boxItem.Value;
                        UsbPortEntity portEntity = Grid_Data.Rows[i].DataBoundItem.ToSerializeObject().ToDeserializeObject<UsbPortEntity>();
                        UpdateDevieInfo(portEntity);
                        defaultColor = Grid_Data.Rows[i].DefaultCellStyle.BackColor;
                        Grid_Data.Rows[i].DefaultCellStyle.BackColor = Color.Gold;
                        bool IsSuc = comport.ReadCommPort(portEntity);
                        //Task.Delay((int)Num_Waiting.Value / 2);
                        endTime = DateTime.Now;
                        portEntity.EndTime = endTime;
                        Grid_Data.Rows[i].Cells["EndTime"].Value = endTime;
                        //更新状态
                        portEntity.PortStatus = IsSuc ? "成功" : "失败";
                        Grid_Data.Rows[i].Cells["PortStatus"].Value = portEntity.PortStatus;
                        //更新控件样式
                        UpdateDevieInfo(portEntity);
                        //插入数据库
                        //this.BaseEntityHelp().Insert(portEntity);
                        portCount++;
                        if (IsSuc) portSuc++;
                        UpdateTakeUpInfo(portEntity.BatchNumber, ExtensionHelp.DateDiff(startTime, endTime), portCount.ToString(), (portSuc * 1.0 / portCount * 100).ToString("0.0"));
                        Grid_Data.Rows[i].DefaultCellStyle.BackColor = defaultColor;
                        //IsRead = false;
                    }
                    BatchSaveData();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                {
                    LogHelper.ToLog("随机检测所有端口异常！" + ex);
                }
                return false;
            }
            
        }

        /// <summary>
        /// 随机检测5~8个
        /// </summary>
        private bool RandomAnyOfSend()
        {
            try
            {
                Dictionary<string, string> BoxDict = ConstValue.GetBoxCodesDic(hardType, hardNum.ToString());
                Color defaultColor = Color.Black;
                foreach (var boxItem in BoxDict)
                {
                    List<int> rlc = new List<int>();
                    LoadHardWareInfo(int.Parse(boxItem.Value) + 1);
                    //Thread.Sleep(1200);
                    int cc = new Random().Next(5, 8);
                    while (rlc.Count < cc)
                    {
                        if (stop)
                        {
                            return false;
                        }
                        mre.WaitOne();
                        Random rdm = new Random();
                        int i = rdm.Next(0, Grid_Data.Rows.Count);
                        while (rlc.Contains(i))
                        {
                            i = rdm.Next(0, Grid_Data.Rows.Count);
                        }
                        rlc.Add(i);
                        Grid_Data.DelegateControl(() => { Grid_Data.CurrentCell = Grid_Data.Rows[i].Cells["Num"]; });
                        Grid_Data.Rows[i].Cells["StartTime"].Value = DateTime.Now;
                        Grid_Data.Rows[i].Cells["BoxId"].Value = boxItem.Value;
                        UsbPortEntity portEntity = Grid_Data.Rows[i].DataBoundItem.ToSerializeObject().ToDeserializeObject<UsbPortEntity>();
                        UpdateDevieInfo(portEntity);
                        defaultColor = Grid_Data.Rows[i].DefaultCellStyle.BackColor;
                        Grid_Data.Rows[i].DefaultCellStyle.BackColor = Color.Gold;
                        bool IsSuc = comport.ReadCommPort(portEntity);
                        //await Task.Delay((int)Num_Waiting.Value / 2);
                        endTime = DateTime.Now;
                        portEntity.EndTime = endTime;
                        Grid_Data.Rows[i].Cells["EndTime"].Value = endTime;
                        //更新状态
                        portEntity.PortStatus = IsSuc ? "成功" : "失败";
                        Grid_Data.Rows[i].Cells["PortStatus"].Value = portEntity.PortStatus;
                        //更新控件样式
                        UpdateDevieInfo(portEntity);
                        //插入数据库
                        //this.BaseEntityHelp().Insert(portEntity);
                        portCount++;
                        if (IsSuc) portSuc++;
                        UpdateTakeUpInfo(portEntity.BatchNumber, ExtensionHelp.DateDiff(startTime, endTime), portCount.ToString(), (portSuc * 1.0 / portCount * 100).ToString("0.0"));
                        Grid_Data.Rows[i].DefaultCellStyle.BackColor = defaultColor;
                        //IsRead = false;
                    }
                    BatchSaveData();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                {
                    LogHelper.ToLog("随机检测5~10端口异常！" + ex);
                }
                return false;
            }
            
        }

        /// <summary>
        /// 批量保存数据到库
        /// </summary>
        /// <returns></returns>
        private bool BatchSaveData()
        {
            //每张表更新完毕之后进行批量插入操作
            var datas = Grid_Data.DataSource.ToSerializeObject().ToDeserializeObject<List<UsbPortEntity>>().FindAll(o => !o.PortStatus.Equals("未检测"));
            if (datas.Count > 0)
            {
                return datas.BatchInsert();
            }
            return false;
        }

        private void setDataGridStyle()
        {
            Grid_Data.BackgroundColor = Color.FromArgb(227, 227, 227);
            Grid_Data.GridColor = Color.FromArgb(230, 230, 230);
            Grid_Data.RowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 248);
            Grid_Data.RowsDefaultCellStyle.ForeColor = Color.FromArgb(53, 53, 53);
            Grid_Data.RowsDefaultCellStyle.Font = new Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            Grid_Data.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(207, 207, 207);
            Grid_Data.RowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(53, 53, 53);
            Grid_Data.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            Grid_Data.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(53, 53, 53);
            Grid_Data.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            Grid_Data.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Grid_Data.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            cellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            cellStyle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            cellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            cellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            cellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            Grid_Data.DefaultCellStyle = cellStyle;
        }

        #endregion
    }
}
