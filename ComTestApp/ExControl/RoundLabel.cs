using ComTestApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComTestApp.ExControl
{
    public partial class RoundLabel : Label
    {

        Color _borderColor = Color.Blue;
        int _radian;

        #region 自定义属性
        [DefaultValue(typeof(Color), "240, 240, 240"), Description("边框颜色")]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(int), "10"), Description("圆角弧度大小")]
        public int Radius
        {
            get
            {
                return _radian;
            }
            set
            {
                _radian = value;
                base.Invalidate();
            }
        }

        #endregion

        public RoundLabel()
        {
            //InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint
                        | ControlStyles.DoubleBuffer
                        | ControlStyles.ResizeRedraw
                        | ControlStyles.AllPaintingInWmPaint
                        | ControlStyles.OptimizedDoubleBuffer
                        | ControlStyles.SupportsTransparentBackColor
                        , true);
        }

        #region 重写基类事件
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle rect = this.DisplayRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            e.Graphics.DrawPath(new Pen(this._borderColor), CreatePath(rect, _radian, false));
        }

        #endregion

        #region 实现圆角及绘制边框事件
        GraphicsPath CreatePath(Rectangle rect, int radius, bool correction)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(rect);
            }
            else
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            }
            path.CloseFigure();
            return path;
        }

        private void DrawBorder(Graphics g, Rectangle rect, int radius)
        {
            rect.Width -= 1;
            rect.Height -= 1;
            using (GraphicsPath path = CreatePath(rect, radius, false))
            {
                using (Pen pen = new Pen(this.BorderColor))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        #endregion

    }
}
