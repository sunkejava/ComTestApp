using ComTestApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComTestApp.ExControl
{
    public partial class ExForm : Form
    {
        public ExForm() : base()
        {
            InitializeComponent();
            this.SetWindowTransparent(200);
            this.SetStyle(
            ControlStyles.UserPaint | //控件自行绘制，而不使用操作系统的绘制
            ControlStyles.AllPaintingInWmPaint | //忽略擦出的消息，减少闪烁。
            ControlStyles.OptimizedDoubleBuffer |//在缓冲区上绘制，不直接绘制到屏幕上，减少闪烁。
            ControlStyles.ResizeRedraw | //控件大小发生变化时，重绘。
            ControlStyles.SupportsTransparentBackColor, true);//支持透明背景颜色
        }

        private Color _baseColor = Color.FromArgb(51, 161, 224);//基颜色
        private ControlState _controlState;//控件状态
        private int _imageWidth = 18;
        private RoundStyle _roundStyle = RoundStyle.All;//圆角
        private int _radius = 8; //圆角半径

        #region 属性
        [DefaultValue(typeof(Color), "51, 161, 224")]
        public Color BaseColor
        {
            get { return _baseColor; }
            set
            {
                _baseColor = value;
                base.Invalidate();
            }
        }
        [DefaultValue(18)]//默认值为18px，最小12px
        public int ImageWidth
        {
            get { return _imageWidth; }
            set
            {
                if (value != _imageWidth)
                {
                    _imageWidth = value < 12 ? 12 : value;
                    base.Invalidate();
                }
            }
        }
        [DefaultValue(typeof(RoundStyle), "1")]//默认全部都是圆角
        public RoundStyle RoundStyle
        {
            get { return _roundStyle; }
            set
            {
                if (_roundStyle != value)
                {
                    _roundStyle = value;
                    base.Invalidate();
                }
            }
        }
        [DefaultValue(8)]//设置圆角半径，默认值为8，最小值为4px
        public int Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value < 4 ? 4 : value;
                    base.Invalidate();
                }
            }
        }
        public ControlState ControlState //控件的状态
        {
            get { return _controlState; }
            set
            {
                if (_controlState != value)
                {
                    _controlState = value;
                    base.Invalidate();
                }
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Graphics g = e.Graphics;
            //Rectangle imageRect;//图像区域
            //Rectangle textRect;//文字区域

            //this.CalculateRect(out imageRect, out textRect);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color baseColor;
            Color borderColor;
            Color innerBorderColor = this._baseColor;//Color.FromArgb(200, 255, 255, 255); ;

            if (Enabled)
            {
                baseColor = _baseColor;
                borderColor = _baseColor;
            }
            else
            {
                baseColor = SystemColors.ControlDark;
                borderColor = SystemColors.ControlDark;
            }

            this.RenderBackgroundInternal(
            g,
            ClientRectangle,
            baseColor,
            borderColor,
            innerBorderColor,
            RoundStyle,
            Radius,
            0.35f,
            false,
            true,
            LinearGradientMode.Vertical);

            //if (Image != null)
            //{
            //    g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            //    g.DrawImage(Image, imageRect, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel);
            //}
            //TextRenderer.DrawText(g, Text, Font, new Point(60, 55), ForeColor, GetTextFormatFlags(TextAlign, RightToLeft == RightToLeft.No));
            g.Flush();
        }

        private TextFormatFlags GetTextFormatFlags(ContentAlignment alignment, bool rightToleft)
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak |
            TextFormatFlags.SingleLine;
            if (rightToleft)
            {
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }

            switch (alignment)
            {
                case ContentAlignment.BottomCenter:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.BottomLeft:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;
                case ContentAlignment.BottomRight:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
                case ContentAlignment.MiddleCenter:
                    flags |= TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.VerticalCenter;
                    break;
                case ContentAlignment.MiddleLeft:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case ContentAlignment.MiddleRight:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;
                case ContentAlignment.TopCenter:
                    flags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.TopLeft:
                    flags |= TextFormatFlags.Top | TextFormatFlags.Left;
                    break;
                case ContentAlignment.TopRight:
                    flags |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;
            }
            return flags;
        }

        protected virtual int CheckRectWidth
        {
            get { return 12; }
        }
        private static readonly ContentAlignment RightAlignment = ContentAlignment.TopRight | ContentAlignment.BottomRight | ContentAlignment.MiddleRight;
        private static readonly ContentAlignment LeftAlignment = ContentAlignment.TopLeft | ContentAlignment.BottomLeft | ContentAlignment.MiddleLeft;

        private void CalculateRect(out Rectangle circleRect, out Rectangle textRect)
        {
            ContentAlignment CheckAlign = ContentAlignment.MiddleLeft;

            circleRect = new Rectangle(
            0, 0, CheckRectWidth, CheckRectWidth);
            textRect = Rectangle.Empty;
            bool bCheckAlignLeft = (int)(LeftAlignment & CheckAlign) != 0;
            bool bCheckAlignRight = (int)(RightAlignment & CheckAlign) != 0;
            bool bRightToLeft = (RightToLeft == RightToLeft.Yes);

            if ((bCheckAlignLeft && !bRightToLeft) ||
            (bCheckAlignRight && bRightToLeft))
            {
                switch (CheckAlign)
                {
                    case ContentAlignment.TopRight:
                    case ContentAlignment.TopLeft:
                        circleRect.Y = 2;
                        break;
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.MiddleLeft:
                        circleRect.Y = (Height - CheckRectWidth) / 2;
                        break;
                    case ContentAlignment.BottomRight:
                    case ContentAlignment.BottomLeft:
                        circleRect.Y = Height - CheckRectWidth - 2;
                        break;
                }

                circleRect.X = 1;

                textRect = new Rectangle(
                circleRect.Right + 2,
                0,
                Width - circleRect.Right - 4,
                Height);
            }
            else if ((bCheckAlignRight && !bRightToLeft)
            || (bCheckAlignLeft && bRightToLeft))
            {
                switch (CheckAlign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                        circleRect.Y = 2;
                        break;
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                        circleRect.Y = (Height - CheckRectWidth) / 2;
                        break;
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        circleRect.Y = Height - CheckRectWidth - 2;
                        break;
                }

                circleRect.X = Width - CheckRectWidth - 1;

                textRect = new Rectangle(
                2, 0, Width - CheckRectWidth - 6, Height);
            }
            else
            {
                switch (CheckAlign)
                {
                    case ContentAlignment.TopCenter:
                        circleRect.Y = 2;
                        textRect.Y = circleRect.Bottom + 2;
                        textRect.Height = Height - CheckRectWidth - 6;
                        break;
                    case ContentAlignment.MiddleCenter:
                        circleRect.Y = (Height - CheckRectWidth) / 2;
                        textRect.Y = 0;
                        textRect.Height = Height;
                        break;
                    case ContentAlignment.BottomCenter:
                        circleRect.Y = Height - CheckRectWidth - 2;
                        textRect.Y = 0;
                        textRect.Height = Height - CheckRectWidth - 6;
                        break;
                }

                circleRect.X = (Width - CheckRectWidth) / 2;

                textRect.X = 2;
                textRect.Width = Width - 4;
            }
        }


        private Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;
            if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

            return Color.FromArgb(a, r, g, b);
        }


        internal void RenderBackgroundInternal(
        Graphics g,
        Rectangle rect,
        Color baseColor,
        Color borderColor,
        Color innerBorderColor,
        RoundStyle style,
        int roundWidth,//圆角半径
        float basePosition,
        bool drawBorder,
        bool drawGlass,
        LinearGradientMode mode)
        {
            if (drawBorder)//是否画边框
            {
                rect.Width--;
                rect.Height--;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, mode))
            {
                Color[] colors = new Color[4];
                colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                colors[1] = GetColor(baseColor, 0, 0, 0, 0);
                colors[2] = baseColor;
                colors[3] = GetColor(baseColor, 0, 0, 0, 0);

                ColorBlend blend = new ColorBlend();
                blend.Positions = new float[] { 0.0f, basePosition, basePosition, 1.0f };
                blend.Colors = colors;
                brush.InterpolationColors = blend;
                if (style != RoundStyle.None)
                {
                    using (GraphicsPath path = GraphicsPathHelper.CreatePath(rect, roundWidth, style, true))
                    {
                        g.FillPath(brush, path);
                    }

                    //if (baseColor.A > 80)
                    //{
                    //    Rectangle rectTop = rect;
                    //    if (mode == LinearGradientMode.Vertical)
                    //    {
                    //        rectTop.Height = (int)(rectTop.Height * basePosition);
                    //    }
                    //    else
                    //    {
                    //        rectTop.Width = (int)(rect.Width * basePosition);
                    //    }
                    //    using (GraphicsPath pathTop = GraphicsPathHelper.CreatePath(rectTop, roundWidth, RoundStyle.All, false))
                    //    {
                    //        using (SolidBrush brushAlpha =
                    //        new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                    //        {
                    //            g.FillPath(brushAlpha, pathTop);
                    //        }
                    //    }
                    //}

                }
            }
        }


        #region 移动窗体
        /// <summary>
        /// 重写WndProc方法,实现窗体移动和禁止双击最大化
        /// </summary>
        /// <param name="m">Windows 消息</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x4e:
                case 0xd:
                case 0xe:
                case 0x14:
                    base.WndProc(ref m);
                    break;
                case 0x84://鼠标点任意位置后可以拖动窗体
                    this.DefWndProc(ref m);
                    if (m.Result.ToInt32() == 0x01)
                    {
                        m.Result = new IntPtr(0x02);
                    }
                    break;
                case 0xA3://禁止双击最大化
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        #region 窗体圆角
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            //Common.RenderHelper.SetFormRoundRectRgn(this, this.Width);
        }
        #endregion

        #region 窗体透明
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.Parent = DesktopWinAPI.GetDesktopWindow();
                cp.ExStyle = 0x00000080 | 0x00000008;//WS_EX_TOOLWINDOW | WS_EX_TOPMOST

                return cp;
            }
        }
        private void SetWindowTransparent(byte bAlpha)
        {
            try
            {
                DesktopWinAPI.SetWindowLong(this.Handle, (int)DesktopWinAPI.WindowStyle.GWL_EXSTYLE,
                    DesktopWinAPI.GetWindowLong(this.Handle, (int)DesktopWinAPI.WindowStyle.GWL_EXSTYLE) | (uint)DesktopWinAPI.ExWindowStyle.WS_EX_LAYERED);

                DesktopWinAPI.SetLayeredWindowAttributes(this.Handle, 0, bAlpha, DesktopWinAPI.LWA_COLORKEY | DesktopWinAPI.LWA_ALPHA);
            }
            catch (Exception)
            {

            }
        }
        #endregion
    }
}
