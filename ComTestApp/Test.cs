using AllIn.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComTestApp
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            

        }

        private void buttonEx1_Click(object sender, EventArgs e)
        {
            foreach (var item in Win32Api.WLFindWindowExAll(IntPtr.Zero, IntPtr.Zero, "#32770", "", 0))
            {
                //文本
                IntPtr hwndex = Win32Api.FindWindowEx(item, IntPtr.Zero, "Edit", "");
                if (hwndex == IntPtr.Zero)
                {
                    continue;
                }
                Win32Api.SendMessage(hwndex, Win32Api.WM_SETTEXT, (int)IntPtr.Zero, "123456");
                //IntPtr downbtn = Win32Api.FindWindowEx(item, IntPtr.Zero, "Button", "确  定");
                //Win32Api.SendMessage(downbtn, Win32Api.WM_CLICK, (int)IntPtr.Zero, 0);
            }
        }
    }
}
