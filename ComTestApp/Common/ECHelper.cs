using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ComTestApp.Common
{
    public class ECHelper
    {
        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory
            (
                IntPtr hProcess,
                IntPtr lpBaseAddress,
                IntPtr lpBuffer,
                int nSize,
                IntPtr lpNumberOfBytesRead
            );

        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess
            (
                int dwDesiredAccess,
                bool bInheritHandle,
                int dwProcessId
            );

        [DllImport("kernel32.dll")]
        private static extern void CloseHandle
            (
                IntPtr hObject
            );
        [DllImportAttribute("kernel32.dll", EntryPoint = "lstrlenW")]
        public static extern int lstrlenW(int lpAddress);
        //写内存
        [DllImportAttribute("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory
            (
                IntPtr hProcess,
                IntPtr lpBaseAddress,
                int[] lpBuffer,
                int nSize,
                IntPtr lpNumberOfBytesWritten
            );

        //获取窗体的进程标识ID
        public static int GetPid(string windowTitle)
        {
            int rs = 0;
            Process[] arrayProcess = Process.GetProcesses();
            foreach (Process p in arrayProcess)
            {
                if (p.MainWindowTitle.IndexOf(windowTitle) != -1)
                {
                    rs = p.Id;
                    break;
                }
            }

            return rs;
        }

        public static int GetBaseAddress(string processName)
        {
            int baseStr = 0x00;
            Process[] arrayProcess = Process.GetProcessesByName(processName);
            foreach (Process p in arrayProcess)
            {
                for (int i = 0; i < p.Modules.Count; i++)
                {
                    if (p.Modules[i].ModuleName == processName + ".exe")
                    {
                        baseStr = (int)p.Modules[i].BaseAddress;
                        return baseStr;
                    }
                }
            }
            return baseStr;
        }

        //根据进程名获取PID
        public static int GetPidByProcessName(string processName)
        {
            Process[] arrayProcess = Process.GetProcessesByName(processName);

            foreach (Process p in arrayProcess)
            {
                return p.Id;
            }
            return 0;
        }

        //根据窗体标题查找窗口句柄（支持模糊匹配）
        public static IntPtr FindWindow(string title)
        {
            Process[] ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (p.MainWindowTitle.IndexOf(title) != -1)
                {
                    return p.MainWindowHandle;
                }
            }
            return IntPtr.Zero;
        }

        //读取内存中的值
        public static int ReadMemoryValue(int baseAddress, string processName)
        {
            try
            {
                byte[] buffer = new byte[4];
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0); //获取缓冲区地址
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName));
                ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero); //将制定内存中的值读入缓冲区
                CloseHandle(hProcess);
                return Marshal.ReadInt32(byteAddress);
            }
            catch
            {
                return 0;
            }
        }

        public static byte[] ReadMemoryValuea(int baseAddress, string processName, int buf_size)
        {
            Byte[] ret_aray = new Byte[buf_size];
            try
            {
                byte[] buffer = new byte[4];
                //获取缓冲区地址
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                //打开一个已存在的进程对象  0x1F0FFF 最高权限
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName));
                //将指定内存中的值读入缓冲区：进程句柄，读取地址，接收地址，读取字节数，实际传送的字节数
                ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, buf_size, IntPtr.Zero);
                //关闭操作
                CloseHandle(hProcess);

                //从非托管内存中读取整个字节buf。
                for (int i = 0; i < buf_size; i++)
                {
                    ret_aray[i] = Marshal.ReadByte(byteAddress, i);
                }
                return ret_aray;
            }
            catch
            {
                return ReadMemoryValuea(baseAddress, processName, buf_size);
            }
        }


        public static string ReadMemoryValue(int baseAddress, string processName, int len = 50)
        {
            try
            {
                byte[] buffer = new byte[len];
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0); //获取缓冲区地址
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName));
                ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, len, IntPtr.Zero); //将制定内存中的值读入缓冲区
                CloseHandle(hProcess);
                byte[] txt = new byte[50];
                for (int i = 0; i < len; i++)
                {
                    txt[i] = Marshal.ReadByte(byteAddress, i);
                }
                var ces = Encoding.GetEncoding(0).GetString(txt).Trim().Replace("\0", "");
                return ces;
            }
            catch
            {
                return ReadMemoryValue(baseAddress, processName, len);
            }
        }

        //将值写入指定内存地址中
        public static void WriteMemoryValue(int baseAddress, string processName, int value)
        {
            IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName)); //0x1F0FFF 最高权限
            WriteProcessMemory(hProcess, (IntPtr)baseAddress, new int[] { value }, 4, IntPtr.Zero);
            CloseHandle(hProcess);
        }

    }
}
