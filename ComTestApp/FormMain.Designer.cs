namespace ComTestApp
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.Panel_Top = new System.Windows.Forms.Panel();
            this.Cmb_StartPort = new System.Windows.Forms.ComboBox();
            this.Btn_Revit = new System.Windows.Forms.Button();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.Cmb_Listcod = new System.Windows.Forms.ComboBox();
            this.Btn_search = new System.Windows.Forms.Button();
            this.Num_Waiting = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.Btn_End = new System.Windows.Forms.Button();
            this.Btn_Start = new System.Windows.Forms.Button();
            this.Cmb_CheckModel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Cmb_CheckNum = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Text_Number = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Cmb_HardList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Cmb_PortList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_LoadComPort = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.Panel_Midel = new System.Windows.Forms.Panel();
            this.Lb_Msg = new System.Windows.Forms.Label();
            this.Panel_View = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.Panel_Bottom = new System.Windows.Forms.Panel();
            this.Group_DriveInfo = new System.Windows.Forms.GroupBox();
            this.Grid_Data = new System.Windows.Forms.DataGridView();
            this.Panel_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Waiting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Text_Number)).BeginInit();
            this.Panel_Midel.SuspendLayout();
            this.Panel_View.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Panel_Bottom.SuspendLayout();
            this.Group_DriveInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_Top
            // 
            this.Panel_Top.Controls.Add(this.Cmb_StartPort);
            this.Panel_Top.Controls.Add(this.Btn_Revit);
            this.Panel_Top.Controls.Add(this.Btn_Send);
            this.Panel_Top.Controls.Add(this.Cmb_Listcod);
            this.Panel_Top.Controls.Add(this.Btn_search);
            this.Panel_Top.Controls.Add(this.Num_Waiting);
            this.Panel_Top.Controls.Add(this.label6);
            this.Panel_Top.Controls.Add(this.Btn_End);
            this.Panel_Top.Controls.Add(this.Btn_Start);
            this.Panel_Top.Controls.Add(this.Cmb_CheckModel);
            this.Panel_Top.Controls.Add(this.label5);
            this.Panel_Top.Controls.Add(this.Cmb_CheckNum);
            this.Panel_Top.Controls.Add(this.label4);
            this.Panel_Top.Controls.Add(this.Text_Number);
            this.Panel_Top.Controls.Add(this.label3);
            this.Panel_Top.Controls.Add(this.Cmb_HardList);
            this.Panel_Top.Controls.Add(this.label2);
            this.Panel_Top.Controls.Add(this.Cmb_PortList);
            this.Panel_Top.Controls.Add(this.label1);
            this.Panel_Top.Controls.Add(this.Btn_LoadComPort);
            this.Panel_Top.Controls.Add(this.label7);
            this.Panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Top.Location = new System.Drawing.Point(0, 0);
            this.Panel_Top.Name = "Panel_Top";
            this.Panel_Top.Size = new System.Drawing.Size(1924, 92);
            this.Panel_Top.TabIndex = 0;
            // 
            // Cmb_StartPort
            // 
            this.Cmb_StartPort.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Cmb_StartPort.FormattingEnabled = true;
            this.Cmb_StartPort.Items.AddRange(new object[] {
            "指定端口测试"});
            this.Cmb_StartPort.Location = new System.Drawing.Point(579, 54);
            this.Cmb_StartPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_StartPort.Name = "Cmb_StartPort";
            this.Cmb_StartPort.Size = new System.Drawing.Size(136, 31);
            this.Cmb_StartPort.TabIndex = 50;
            this.Cmb_StartPort.SelectedIndexChanged += new System.EventHandler(this.Cmb_StartPort_SelectedIndexChanged);
            // 
            // Btn_Revit
            // 
            this.Btn_Revit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Revit.BackColor = System.Drawing.SystemColors.Control;
            this.Btn_Revit.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Revit.Location = new System.Drawing.Point(1430, 9);
            this.Btn_Revit.Name = "Btn_Revit";
            this.Btn_Revit.Size = new System.Drawing.Size(116, 32);
            this.Btn_Revit.TabIndex = 49;
            this.Btn_Revit.Text = "复位";
            this.Btn_Revit.UseVisualStyleBackColor = false;
            this.Btn_Revit.Click += new System.EventHandler(this.Btn_Revit_Click);
            // 
            // Btn_Send
            // 
            this.Btn_Send.BackColor = System.Drawing.SystemColors.Control;
            this.Btn_Send.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Send.Location = new System.Drawing.Point(399, 55);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(62, 32);
            this.Btn_Send.TabIndex = 48;
            this.Btn_Send.Text = "发送";
            this.Btn_Send.UseVisualStyleBackColor = false;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // Cmb_Listcod
            // 
            this.Cmb_Listcod.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Cmb_Listcod.FormattingEnabled = true;
            this.Cmb_Listcod.Items.AddRange(new object[] {
            "指定端口测试"});
            this.Cmb_Listcod.Location = new System.Drawing.Point(253, 55);
            this.Cmb_Listcod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_Listcod.Name = "Cmb_Listcod";
            this.Cmb_Listcod.Size = new System.Drawing.Size(136, 31);
            this.Cmb_Listcod.TabIndex = 47;
            this.Cmb_Listcod.SelectedIndexChanged += new System.EventHandler(this.Cmb_Listcod_SelectedIndexChanged);
            // 
            // Btn_search
            // 
            this.Btn_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_search.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_search.Location = new System.Drawing.Point(1832, 9);
            this.Btn_search.Name = "Btn_search";
            this.Btn_search.Size = new System.Drawing.Size(80, 32);
            this.Btn_search.TabIndex = 46;
            this.Btn_search.Text = "查询";
            this.Btn_search.UseVisualStyleBackColor = true;
            this.Btn_search.Click += new System.EventHandler(this.Btn_search_Click);
            // 
            // Num_Waiting
            // 
            this.Num_Waiting.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Num_Waiting.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.Num_Waiting.Location = new System.Drawing.Point(84, 54);
            this.Num_Waiting.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.Num_Waiting.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.Num_Waiting.Name = "Num_Waiting";
            this.Num_Waiting.Size = new System.Drawing.Size(136, 29);
            this.Num_Waiting.TabIndex = 44;
            this.Num_Waiting.Value = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.Num_Waiting.ValueChanged += new System.EventHandler(this.Num_Waiting_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(30, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 23);
            this.label6.TabIndex = 43;
            this.label6.Text = "等待:";
            // 
            // Btn_End
            // 
            this.Btn_End.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_End.Enabled = false;
            this.Btn_End.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_End.Location = new System.Drawing.Point(1699, 9);
            this.Btn_End.Name = "Btn_End";
            this.Btn_End.Size = new System.Drawing.Size(116, 32);
            this.Btn_End.TabIndex = 42;
            this.Btn_End.Text = "结束";
            this.Btn_End.UseVisualStyleBackColor = true;
            this.Btn_End.Click += new System.EventHandler(this.Btn_End_Click);
            // 
            // Btn_Start
            // 
            this.Btn_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Start.BackColor = System.Drawing.SystemColors.Control;
            this.Btn_Start.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Start.Location = new System.Drawing.Point(1566, 9);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(116, 32);
            this.Btn_Start.TabIndex = 41;
            this.Btn_Start.Text = "开始";
            this.Btn_Start.UseVisualStyleBackColor = false;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // Cmb_CheckModel
            // 
            this.Cmb_CheckModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_CheckModel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cmb_CheckModel.FormattingEnabled = true;
            this.Cmb_CheckModel.Items.AddRange(new object[] {
            "顺序",
            "随机"});
            this.Cmb_CheckModel.Location = new System.Drawing.Point(1110, 13);
            this.Cmb_CheckModel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_CheckModel.Name = "Cmb_CheckModel";
            this.Cmb_CheckModel.Size = new System.Drawing.Size(121, 31);
            this.Cmb_CheckModel.TabIndex = 40;
            this.Cmb_CheckModel.SelectedIndexChanged += new System.EventHandler(this.Cmb_CheckModel_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(1027, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 23);
            this.label5.TabIndex = 39;
            this.label5.Text = "检测模式：";
            // 
            // Cmb_CheckNum
            // 
            this.Cmb_CheckNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_CheckNum.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cmb_CheckNum.FormattingEnabled = true;
            this.Cmb_CheckNum.Items.AddRange(new object[] {
            "单次",
            "连续"});
            this.Cmb_CheckNum.Location = new System.Drawing.Point(873, 11);
            this.Cmb_CheckNum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_CheckNum.Name = "Cmb_CheckNum";
            this.Cmb_CheckNum.Size = new System.Drawing.Size(121, 31);
            this.Cmb_CheckNum.TabIndex = 38;
            this.Cmb_CheckNum.SelectedIndexChanged += new System.EventHandler(this.Cmb_CheckNum_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(788, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 23);
            this.label4.TabIndex = 37;
            this.label4.Text = "检测次数：";
            // 
            // Text_Number
            // 
            this.Text_Number.Enabled = false;
            this.Text_Number.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Text_Number.Location = new System.Drawing.Point(686, 14);
            this.Text_Number.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.Text_Number.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Text_Number.Name = "Text_Number";
            this.Text_Number.ReadOnly = true;
            this.Text_Number.Size = new System.Drawing.Size(63, 29);
            this.Text_Number.TabIndex = 36;
            this.Text_Number.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Text_Number.ValueChanged += new System.EventHandler(this.Text_Number_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(642, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 23);
            this.label3.TabIndex = 34;
            this.label3.Text = "数量：";
            // 
            // Cmb_HardList
            // 
            this.Cmb_HardList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_HardList.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cmb_HardList.FormattingEnabled = true;
            this.Cmb_HardList.Location = new System.Drawing.Point(493, 12);
            this.Cmb_HardList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_HardList.Name = "Cmb_HardList";
            this.Cmb_HardList.Size = new System.Drawing.Size(121, 31);
            this.Cmb_HardList.TabIndex = 33;
            this.Cmb_HardList.SelectedIndexChanged += new System.EventHandler(this.Cmb_HardList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(407, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 23);
            this.label2.TabIndex = 32;
            this.label2.Text = "硬件类型：";
            // 
            // Cmb_PortList
            // 
            this.Cmb_PortList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_PortList.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Cmb_PortList.FormattingEnabled = true;
            this.Cmb_PortList.Location = new System.Drawing.Point(84, 11);
            this.Cmb_PortList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_PortList.Name = "Cmb_PortList";
            this.Cmb_PortList.Size = new System.Drawing.Size(136, 31);
            this.Cmb_PortList.TabIndex = 18;
            this.Cmb_PortList.SelectedIndexChanged += new System.EventHandler(this.Cmb_PortList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "COM口:";
            // 
            // Btn_LoadComPort
            // 
            this.Btn_LoadComPort.BackColor = System.Drawing.SystemColors.Control;
            this.Btn_LoadComPort.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_LoadComPort.Location = new System.Drawing.Point(253, 11);
            this.Btn_LoadComPort.Name = "Btn_LoadComPort";
            this.Btn_LoadComPort.Size = new System.Drawing.Size(131, 32);
            this.Btn_LoadComPort.TabIndex = 45;
            this.Btn_LoadComPort.Text = "加载COM口";
            this.Btn_LoadComPort.UseVisualStyleBackColor = false;
            this.Btn_LoadComPort.Click += new System.EventHandler(this.Btn_LoadComPort_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(493, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 23);
            this.label7.TabIndex = 51;
            this.label7.Text = "起始端口：";
            // 
            // Panel_Midel
            // 
            this.Panel_Midel.Controls.Add(this.Lb_Msg);
            this.Panel_Midel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Midel.Location = new System.Drawing.Point(0, 92);
            this.Panel_Midel.Name = "Panel_Midel";
            this.Panel_Midel.Size = new System.Drawing.Size(1924, 68);
            this.Panel_Midel.TabIndex = 1;
            // 
            // Lb_Msg
            // 
            this.Lb_Msg.BackColor = System.Drawing.SystemColors.Control;
            this.Lb_Msg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lb_Msg.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lb_Msg.Location = new System.Drawing.Point(0, 0);
            this.Lb_Msg.Name = "Lb_Msg";
            this.Lb_Msg.Size = new System.Drawing.Size(1924, 68);
            this.Lb_Msg.TabIndex = 0;
            this.Lb_Msg.Text = "当前批次【xxxx】已检测耗时xx秒，共计检测端口xx个，成功率为x";
            this.Lb_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_View
            // 
            this.Panel_View.Controls.Add(this.tabControl1);
            this.Panel_View.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_View.Location = new System.Drawing.Point(0, 160);
            this.Panel_View.Name = "Panel_View";
            this.Panel_View.Size = new System.Drawing.Size(1924, 581);
            this.Panel_View.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1924, 581);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Location = new System.Drawing.Point(4, 32);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1916, 545);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "设备1";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 32);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1916, 545);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "设备2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 32);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1916, 545);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "设备3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 32);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1916, 545);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "设备4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 32);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1916, 545);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "设备5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 32);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1916, 545);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "设备6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 32);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(1916, 545);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "设备7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(4, 32);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(1916, 545);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "设备8";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new System.Drawing.Point(4, 32);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(1916, 545);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "设备9";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new System.Drawing.Point(4, 32);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(1916, 545);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "设备10";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // Panel_Bottom
            // 
            this.Panel_Bottom.Controls.Add(this.Group_DriveInfo);
            this.Panel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Bottom.Location = new System.Drawing.Point(0, 741);
            this.Panel_Bottom.Name = "Panel_Bottom";
            this.Panel_Bottom.Size = new System.Drawing.Size(1924, 91);
            this.Panel_Bottom.TabIndex = 3;
            // 
            // Group_DriveInfo
            // 
            this.Group_DriveInfo.Controls.Add(this.Grid_Data);
            this.Group_DriveInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Group_DriveInfo.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Group_DriveInfo.Location = new System.Drawing.Point(0, 0);
            this.Group_DriveInfo.Name = "Group_DriveInfo";
            this.Group_DriveInfo.Size = new System.Drawing.Size(1924, 91);
            this.Group_DriveInfo.TabIndex = 2;
            this.Group_DriveInfo.TabStop = false;
            this.Group_DriveInfo.Text = "设备数据";
            // 
            // Grid_Data
            // 
            this.Grid_Data.AllowUserToAddRows = false;
            this.Grid_Data.AllowUserToDeleteRows = false;
            this.Grid_Data.AllowUserToResizeColumns = false;
            this.Grid_Data.AllowUserToResizeRows = false;
            this.Grid_Data.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Grid_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_Data.Location = new System.Drawing.Point(3, 25);
            this.Grid_Data.Margin = new System.Windows.Forms.Padding(4);
            this.Grid_Data.Name = "Grid_Data";
            this.Grid_Data.ReadOnly = true;
            this.Grid_Data.RowHeadersWidth = 51;
            this.Grid_Data.RowTemplate.Height = 23;
            this.Grid_Data.Size = new System.Drawing.Size(1918, 63);
            this.Grid_Data.TabIndex = 13;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 832);
            this.Controls.Add(this.Panel_Bottom);
            this.Controls.Add(this.Panel_View);
            this.Controls.Add(this.Panel_Midel);
            this.Controls.Add(this.Panel_Top);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "鲸鱼池硬件检测工具V2.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Panel_Top.ResumeLayout(false);
            this.Panel_Top.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Waiting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Text_Number)).EndInit();
            this.Panel_Midel.ResumeLayout(false);
            this.Panel_View.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.Panel_Bottom.ResumeLayout(false);
            this.Group_DriveInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Top;
        private System.Windows.Forms.Panel Panel_Midel;
        private System.Windows.Forms.Panel Panel_View;
        private System.Windows.Forms.Panel Panel_Bottom;
        private System.Windows.Forms.GroupBox Group_DriveInfo;
        private System.Windows.Forms.DataGridView Grid_Data;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Cmb_PortList;
        private System.Windows.Forms.ComboBox Cmb_HardList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown Text_Number;
        private System.Windows.Forms.ComboBox Cmb_CheckNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Cmb_CheckModel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.Button Btn_End;
        private System.Windows.Forms.Label Lb_Msg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.NumericUpDown Num_Waiting;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Btn_LoadComPort;
        private System.Windows.Forms.Button Btn_search;
        private System.Windows.Forms.ComboBox Cmb_Listcod;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.Button Btn_Revit;
        private System.Windows.Forms.ComboBox Cmb_StartPort;
        private System.Windows.Forms.Label label7;
    }
}

