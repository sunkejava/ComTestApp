using AllIn.Core.Util;
using ComTestApp.Common;
using ComTestApp.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComTestApp
{
    public partial class FormSearch : Form
    {

        #region 分页变量

        int _pageNow = 1;//当前页码
        int PageNow
        {
            get { return _pageNow; }
            set
            {
                _pageNow = value;
                OnPageEdit(_pageNow);
            }
        }
        int _pageCount = 1;//总页数
        int PageCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;
                //OnPageEdit(_pageCount);
            }
        }

        #endregion

        List<UsbPortEntity> resultEntitys = new List<UsbPortEntity>();
        public FormSearch()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            //利用反射设置DataGridView的双缓冲
            Type dgvType = this.Grid_Data.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.Grid_Data, true, null);
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            Time_Begin.Text = Time_End.Text = Text_BrantchNumber.Text = "";
            Time_Begin.Value = Time_End.Value = DateTime.Now;
            Cmb_HardList.SelectedIndex = Cmb_PortList.SelectedIndex = Cmb_Status.SelectedIndex = 0;
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            InitColInfo();
            LoadComInfo();
            Grid_Data.RowsDefaultCellStyle.BackColor = Color.Bisque;
            Grid_Data.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            Grid_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Grid_Data.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;  //设置列标题不换行
            //OnPageEdit += FormSearch_OnPageEdit;
            if (Cmb_PageSize.Items.Count > 0)
            {
                Cmb_PageSize.SelectedIndex = 0;
            }

            #region 分页控件位置调整
            Btn_FirstPage.Anchor = Btn_PrevPage.Anchor = Btn_NextPage.Anchor = Btn_EndPage.Anchor = Text_PageNo.Anchor
                = Btn_Jonp.Anchor = Cmb_PageSize.Anchor = AnchorStyles.None;
            int PandingWidth = 10;
            Btn_FirstPage.Location = new Point(500, 8);
            Btn_PrevPage.Location = new Point(Btn_FirstPage.Left + PandingWidth + Btn_FirstPage.Width, 8);
            Btn_NextPage.Location = new Point(Btn_PrevPage.Left + PandingWidth + Btn_PrevPage.Width, 8);
            Btn_EndPage.Location = new Point(Btn_NextPage.Left + PandingWidth + Btn_NextPage.Width, 8);
            Text_PageNo.Location = new Point(Btn_EndPage.Left + PandingWidth + Btn_EndPage.Width, 8);
            Btn_Jonp.Location = new Point(Text_PageNo.Left + PandingWidth + Text_PageNo.Width, 8);
            Cmb_PageSize.Location = new Point(Btn_Jonp.Left + PandingWidth + Btn_Jonp.Width, 8);
            Btn_FirstPage.Anchor = Btn_PrevPage.Anchor = Btn_NextPage.Anchor = Btn_EndPage.Anchor = Text_PageNo.Anchor
                = Btn_Jonp.Anchor = Cmb_PageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            #endregion

            Lb_PageInfo.Text = "共0条记录     0/0";
        }

        private void FormSearch_OnPageEdit(int val)
        {
            LoadPageDate();
        }

        private void Btn_Serach_Click(object sender, EventArgs e)
        {
            try
            {
                OnPageEdit -= FormSearch_OnPageEdit;
                var ListSous = ExtensionHelp.SelectEntity(new UsbPortEntity()
                {
                    HardType = Cmb_HardList.Text,
                    PortStatus = Cmb_Status.Text,
                    SerialPortName = Cmb_PortList.Text,
                    BatchNumber = Text_BrantchNumber.Text.Trim()
                });
                resultEntitys = ListSous.FindAll(o => o.StartTime >= DateTime.Parse(Time_Begin.Value.ToString("yyyy-MM-dd")) && o.EndTime <= DateTime.Parse(Time_End.Value.AddDays(1).ToString("yyyy-MM-dd")));
                resultEntitys = resultEntitys.OrderBy(i => i.BatchNumber).ThenBy(i => i.Num).ToList();
                _pageNow = 1;
                LoadPageDate();
                OnPageEdit += FormSearch_OnPageEdit;
            }
            catch (Exception ex)
            {
                LogHelper.ToLog("数据查询异常，原以为:" + ex);
            }
        }


        #region 自定义函数


        /// <summary>
        /// 加载串口信息
        /// </summary>
        private void LoadComInfo()
        {
            string[] PortNames = SerialPort.GetPortNames();
            List<string> list = new List<string>();
            list.Add("");
            foreach (string pName in PortNames)
            { if (!pName.Equals("COM1")) list.Add(pName); }            
            Cmb_PortList.DataSource = list;
            if (Cmb_PortList.Items.Count > 0)
                Cmb_PortList.SelectedIndex = 0;
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
        /// 加载分页数据
        /// </summary>
        private void LoadPageDate()
        {
            int countRecord = resultEntitys.Count;
            //if (countRecord == 0) return;
            int pageSize = int.Parse(Cmb_PageSize.Text);
            PageCount = countRecord % pageSize == 0 ? countRecord / pageSize : (countRecord - (countRecord % pageSize)) / pageSize + 1;
            Lb_PageInfo.Text = string.Format("共{0}条记录     {1}/{2}", countRecord, PageNow, PageCount);
            Grid_Data.DataSource = new BindingList<UsbPortEntity>(resultEntitys.Skip((PageNow -1) * pageSize).Take(pageSize).ToList());
        }

        delegate void PageChange(int val);

        event PageChange OnPageEdit;
        #endregion

        private void Btn_FirstPage_Click(object sender, EventArgs e)
        {
            if (PageNow == 1)
            {
                MessageBox.Show("已经是第一页！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PageNow = 1;
        }

        private void Btn_EndPage_Click(object sender, EventArgs e)
        {
            if (PageNow == PageCount)
            {
                MessageBox.Show("已经是最后一页！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PageNow = PageCount;
        }

        private void Btn_PrevPage_Click(object sender, EventArgs e)
        {
            if (PageNow == 1)
            {
                MessageBox.Show("已经是第一页！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PageNow--;
        }

        private void Btn_NextPage_Click(object sender, EventArgs e)
        {
            if (PageNow == PageCount)
            {
                MessageBox.Show("已经是最后一页！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PageNow++;
        }

        private void Btn_Jonp_Click(object sender, EventArgs e)
        {
            if (Text_PageNo.Text.IsEmpty())
            {
                MessageBox.Show("请输入需要跳转的页码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Text_PageNo.Focus();
                return;
            }
            int zp = int.Parse(Text_PageNo.Text);
            if (zp > PageCount || zp < 0)
            {
                MessageBox.Show("请确认输入页码是否正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Text_PageNo.Focus();
                return;
            }
            if (zp == PageNow)
            {
                Text_PageNo.Focus();
                return;
            }
            PageNow = int.Parse(Text_PageNo.Text);
        }

        private void Cmb_PageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int countRecord = resultEntitys.Count;
            int pageSize = int.Parse(Cmb_PageSize.Text);
            PageCount = countRecord % pageSize == 0 ? countRecord / pageSize : (countRecord - (countRecord % pageSize)) / pageSize + 1;
            if (PageNow > PageCount && PageNow != 1) { PageNow = PageCount; } else { LoadPageDate(); }
        }

        private void Text_PageNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8 || e.KeyChar == 9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
