using ComTestApp.Common;
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
    public partial class FormLoading : Form
    {
        public FormLoading()
        {
            InitializeComponent();
        }

        private void FormLoading_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#FF5DB3AB"); //171, 179, 93
        }
    }
}
