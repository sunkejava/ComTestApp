using AllIn.Core.Form.Helpers;
using AllIn.Core.Util;
using ComTestApp.Entitys;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComTestApp.Common
{
    /// <summary>
    /// 端口测试类
    /// </summary>
    public  class ComPort
    {

        public ComPort(int _at)
        {
            waitingNum = _at;
        }
        static StringBuilder Builder = new StringBuilder();
        public int waitingNum = 2500;//默认等待时间
        public bool ReadCommPort(UsbPortEntity entity,CheckEnum.DeviceCheckMode deviceCheckMode)
        {            
            try
            {
                var boxCode = entity.BoxId;  //箱体 名称 
                string bkName = entity.CardName;
                string portName = entity.PortId;
                var drawerCode = entity.CardId;
                var drawerCodes = ConstValue.GetDrawerCodeDic(entity.HardType, boxCode); //获取 是 C1 还是 A1
                if (entity.HardType.Equals("B2"))
                {
                    drawerCodes.Clear();
                    for (int r = 0; r < 5; r++)
                    {
                        drawerCodes.Add((r + 1 + (int.Parse(boxCode)) * 5).ToString().PadLeft(2,'0'), (r + 1 + (int.Parse(boxCode)) * 5).ToString().PadLeft(2,'0'));
                    }
                }
                if (null != drawerCodes && drawerCodes.Keys.Any())
                {
                    foreach (var key in drawerCodes.Keys)
                    {
                        if (!string.IsNullOrEmpty(drawerCodes[key]) && drawerCodes[key].Equals(bkName))
                        { drawerCode = key; }
                    }
                }
                var sb = new StringBuilder();
                sb.Append(boxCode);
                sb.Append(drawerCode);
                var code = entity.HardType.Equals("B2") ? Convert.ToInt32(portName) : 22 - Convert.ToInt32(portName);
                sb.Append(code.ToString().Length == 1 ? "0" + code : code.ToString());
                return ReadCommByUkeyAddAsync(sb.ToString().Trim(), entity, deviceCheckMode);
            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                {
                    LogHelper.ToLog("发送数据到端口方式异常，原因为： " + ex);
                }
                return false;
            }
        }

        public bool ReadCommByUkeyAddAsync(string ukeyadd, UsbPortEntity pe,CheckEnum.DeviceCheckMode deviceCheckMode)
        {
            try
            {
                bool IsSuc = false;
                if (string.IsNullOrEmpty(pe.SerialPortName))      //Com端口 
                { return IsSuc; }
                SerialPort sp = new SerialPort();
                if (ukeyadd.Length != 6)         //Ukey6位 编码 
                {
                    LogHelper.ToLog(ConstValue.AlertInfo.ComAlert.UkeyError);
                    return IsSuc;
                }
                if (!string.IsNullOrEmpty(pe.SerialPortName))
                {
                    sp.PortName = pe.SerialPortName;        //赋值 
                }
                else
                {
                    var portNames = SerialPort.GetPortNames();    //获取本机串口名称，存入PortNames数组中
                    if (portNames.Count() > 1)
                    {
                        LogHelper.ToLog("没有配置默认端口号，请前往系统配置默认端口号!");
                        return false;
                    }
                    else
                    { UserContext.SysConfig.ComPort = portNames[0]; }
                }
                sp.ReadTimeout = 32;
                try
                {
                    sp.Open();
                    var ban = Convert.ToInt32(ukeyadd.Substring(2, 2));
                    var usb = Convert.ToInt32(ukeyadd.Substring(4, 2));
                    if (ban <= 0) ban = 255;
                    if (usb <= 0) ban = 255;
                    //存放待发送的一包数据（包括帧头，命令号，帧长，帧数据，校验，帧尾）
                    var package = new byte[5];
                    package[0] = 0xF5;//帧头 
                    package[1] = (Byte)(UtilHelper.Convert10To16(ban));//板ID      
                    package[2] = (Byte)(UtilHelper.Convert10To16(usb));//usbid  
                    package[3] = ((byte)(ban >= 255 ? 0xFF : (Byte)(UtilHelper.Convert10To16(ban + usb))));//校验和
                    package[4] = 0xC4;//帧尾

                    if (!sp.IsOpen)
                    {
                        LogHelper.ToLog("端口打开失败！");
                        return IsSuc;
                    }
                    sp.Write(package, 0, 5);//向串口发送一包（18字节）的数据  
                    if (ukeyadd != "000000")
                    {
                        //LogHelper.ToLog("Ukey编码：" + ukeyadd + "=====编号：" + pe.PortId + "成功！");
                        //Console.WriteLine("编号：" + pe.PortId + "成功！");
                        //Console.WriteLine("begin:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                        Thread.Sleep(waitingNum);
                        //Console.WriteLine("end:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                        switch (deviceCheckMode)
                        {
                            case CheckEnum.DeviceCheckMode.Drive:
                                IsSuc = IsHaveDrive(ukeyadd);
                                break;
                            case CheckEnum.DeviceCheckMode.ShCa:
                                IsSuc = IsReadCaName(ukeyadd);//IsHaveShCa(ukeyadd);
                                break;
                            default:
                                break;
                        }                                               
                    }
                    //添加事件注册
                    sp.DataReceived += Sp_DataReceived;
                    while (sp.IsOpen)
                    {
                        sp.Close();
                    }
                    return IsSuc;
                }
                catch (Exception ex)
                {
                    while (sp.IsOpen)
                    {
                        sp.Close();
                    }
                    if (!(ex is ThreadAbortException))
                    {
                        LogHelper.ToLog(ex);
                    }
                    return IsSuc;
                }
            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                {
                    LogHelper.ToLog(ex);
                }
                return false;
            }
            
        }

        private bool IsReadCaName(string ukeyadd)
        {
            bool IsExists = false;
            Thread.Sleep(200);
            var baseAddress = ECHelper.GetBaseAddress("UniClient");
            var c1 = ECHelper.ReadMemoryValue(baseAddress + 0x00493D64, "UniClient");
            var c2 = ECHelper.ReadMemoryValue(c1 + 0x8, "UniClient");
            var c3 = ECHelper.ReadMemoryValue(c2 + 0x0, "UniClient");
            var c4 = ECHelper.ReadMemoryValue(c3 + 0x28, "UniClient");
            var c5 = ECHelper.ReadMemoryValue(c4 + 0xF8, "UniClient");//0x1EC
            var c6 = ECHelper.ReadMemoryValue(c5 + 0x280, "UniClient", 50);
            if (c6.IsEmpty())
            {
                Thread.Sleep(200);
                c6 = GetCaName();
            }
            c6 = c6.Contains("删除") ? "" : c6;
            if(c6.Contains("测试全力"))
            {
                IsExists = true;             
            }
            Thread.Sleep(200);
            LogHelper.ToLog(String.Format("指令{0}加载上海CA{1}！", ukeyadd, IsExists ? c6 + "成功" : "失败"));
            return IsExists;
        }

        private string GetCaName()
        {
            var baseAddress = ECHelper.GetBaseAddress("UniClient");
            var c1 = ECHelper.ReadMemoryValue(baseAddress + 0x00493D64, "UniClient");
            var c2 = ECHelper.ReadMemoryValue(c1 + 0x8, "UniClient");
            var c3 = ECHelper.ReadMemoryValue(c2 + 0x0, "UniClient");
            var c4 = ECHelper.ReadMemoryValue(c3 + 0x28, "UniClient");
            var c5 = ECHelper.ReadMemoryValue(c4 + 0x1EC, "UniClient");//0xF8
            var c6 = ECHelper.ReadMemoryValue(c5 + 0x280, "UniClient", 50);
            return c6;
        }

        //private bool IsHaveShCa(string ukeyadd)
        //{
        //    Thread.Sleep(waitingNum);
        //    bool IsExists = false;
        //    var caName = "";
        //    for (int i = 0; i < 8; i++)
        //    {
        //        X509Store store = new X509Store((StoreName)(i + 1), StoreLocation.CurrentUser);
        //        store.Open(OpenFlags.ReadOnly);
        //        foreach (var item in store.Certificates)
        //        {
        //            if (item.GetName().Contains("测试全力"))
        //            {
        //                IsExists = true;
        //                caName = item.GetName();
        //                break;
        //            }
        //        }
        //    }
        //    LogHelper.ToLog(String.Format("指令{0}加载上海CA{1}！", ukeyadd,IsExists ? caName + "成功":"失败"));
        //    return IsExists;
        //}

        private  void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = sender as SerialPort;
                if (sp.IsOpen)
                {
                    int n = sp.BytesToRead;
                    byte[] buf = new byte[n]; //声明一个临时数组存储当前来的串口数据      
                    sp.Read(buf, 0, n); //读取缓冲数据
                    Builder.Clear(); //清除字符串构造器的内容
                    {
                        //依次的拼接出16进制字符串
                        foreach (byte b in buf)
                        {
                            Builder.Append(b.ToString("X2") + " ");
                        }
                    };
                    Console.WriteLine("Port读取接口数据:" + Builder.ToString());
                }
                while (sp.IsOpen)
                {
                    sp.Close();
                }
            }
            catch (Exception)
            { }
        }

        private bool IsHaveDrive(string ukeyadd)
        {
            Thread.Sleep(waitingNum);
            DriveInfo[] s = DriveInfo.GetDrives();
            foreach (DriveInfo i in s)
            {
                if (i.DriveType == DriveType.Removable)
                {
                    //LogHelper.ToLog(String.Format("指令{0}加载U盘成功！",ukeyadd));
                    return true;
                }
            }
            LogHelper.ToLog(String.Format("指令{0}加载U盘失败！", ukeyadd));
            return false;
        }
    }
}
