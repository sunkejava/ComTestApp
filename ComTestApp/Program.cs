﻿using ComTestApp.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComTestApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {            
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMain());
            //判断是否需要释放文件
            ReleaseResouce();
            /**
              * 当前用户是管理员的时候，直接启动应用程序
              * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
              */
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //如果是管理员，则直接运行
                Process process = RuningInstance();
                if (process == null)
                {
                    Application.Run(new FormMain());
                }
                else
                {
                    HandleRunningInstance(process);
                }
            }
            else
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Application.ExecutablePath;
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                try
                {
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch
                {
                    return;
                }
                //退出
                Application.Exit();
            }

        }

        static void ReleaseResouce()
        {
            byte[] byFont = Properties.Resources.创艺简中圆;//获取嵌入更新文件的字节数组
            string strPath = Application.StartupPath + @"\font\创艺简中圆.TTF";//设置释放路径
            //if (!Directory.Exists(Path.GetDirectoryName(strPath)))
            //{
            //    Directory.CreateDirectory(Path.GetDirectoryName(strPath));
            //}
            //if (!File.Exists(strPath))
            //{
            //    //创建字体文件（覆盖模式）
            //    using (FileStream fs = new FileStream(strPath, FileMode.Create))
            //    {
            //        fs.Write(byFont, 0, byFont.Length);
            //    }
            //}
            byFont = Properties.Resources.SQLite_Interop;
            strPath = Application.StartupPath + @"\SQLite.Interop.dll";
            if (!File.Exists(strPath))
            {
                using (FileStream fs = new FileStream(strPath, FileMode.Create))
                {
                    fs.Write(byFont, 0, byFont.Length);
                }
            }
        }

        ///<summary>
        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int SW_SHOWNOMAL = 5;
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL);//显示
            SetForegroundWindow(instance.MainWindowHandle);//设置到最前端
        }
        private static Process RuningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in Processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
    }
}
