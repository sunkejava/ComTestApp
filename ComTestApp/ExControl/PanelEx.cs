using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComTestApp.ExControl
{
    public partial class PanelEx : Control
    {
        public PanelEx()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.Opaque, true); this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override CreateParams CreateParams { 
            get 
            { 
                CreateParams cp = base.CreateParams; 
                cp.ExStyle = 0x20; 
                return cp; 
            }
        }
    }
}
