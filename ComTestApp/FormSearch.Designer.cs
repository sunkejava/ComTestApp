namespace ComTestApp
{
    partial class FormSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearch));
            this.Panel_Top = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_Clear = new System.Windows.Forms.Button();
            this.Btn_Serach = new System.Windows.Forms.Button();
            this.Cmb_PortList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Cmb_Status = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Cmb_HardList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Text_BrantchNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Time_End = new System.Windows.Forms.DateTimePicker();
            this.Time_Begin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel_Bottom = new System.Windows.Forms.Panel();
            this.Grid_Data = new System.Windows.Forms.DataGridView();
            this.Panel_Page = new System.Windows.Forms.Panel();
            this.Cmb_PageSize = new System.Windows.Forms.ComboBox();
            this.Btn_Jonp = new System.Windows.Forms.Button();
            this.Text_PageNo = new System.Windows.Forms.TextBox();
            this.Btn_EndPage = new System.Windows.Forms.Button();
            this.Btn_NextPage = new System.Windows.Forms.Button();
            this.Btn_PrevPage = new System.Windows.Forms.Button();
            this.Btn_FirstPage = new System.Windows.Forms.Button();
            this.Lb_PageInfo = new System.Windows.Forms.Label();
            this.Panel_Top.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Panel_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Data)).BeginInit();
            this.Panel_Page.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Top
            // 
            this.Panel_Top.Controls.Add(this.groupBox1);
            this.Panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Top.Location = new System.Drawing.Point(0, 0);
            this.Panel_Top.Name = "Panel_Top";
            this.Panel_Top.Size = new System.Drawing.Size(1355, 141);
            this.Panel_Top.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btn_Clear);
            this.groupBox1.Controls.Add(this.Btn_Serach);
            this.groupBox1.Controls.Add(this.Cmb_PortList);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Cmb_Status);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Cmb_HardList);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Text_BrantchNumber);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Time_End);
            this.groupBox1.Controls.Add(this.Time_Begin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1355, 141);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // Btn_Clear
            // 
            this.Btn_Clear.Location = new System.Drawing.Point(1078, 37);
            this.Btn_Clear.Name = "Btn_Clear";
            this.Btn_Clear.Size = new System.Drawing.Size(75, 66);
            this.Btn_Clear.TabIndex = 49;
            this.Btn_Clear.Text = "清除";
            this.Btn_Clear.UseVisualStyleBackColor = true;
            this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // Btn_Serach
            // 
            this.Btn_Serach.Location = new System.Drawing.Point(1176, 37);
            this.Btn_Serach.Name = "Btn_Serach";
            this.Btn_Serach.Size = new System.Drawing.Size(75, 66);
            this.Btn_Serach.TabIndex = 48;
            this.Btn_Serach.Text = "查询";
            this.Btn_Serach.UseVisualStyleBackColor = true;
            this.Btn_Serach.Click += new System.EventHandler(this.Btn_Serach_Click);
            // 
            // Cmb_PortList
            // 
            this.Cmb_PortList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_PortList.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Cmb_PortList.FormattingEnabled = true;
            this.Cmb_PortList.Location = new System.Drawing.Point(633, 83);
            this.Cmb_PortList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_PortList.Name = "Cmb_PortList";
            this.Cmb_PortList.Size = new System.Drawing.Size(164, 31);
            this.Cmb_PortList.TabIndex = 46;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label6.Location = new System.Drawing.Point(557, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 23);
            this.label6.TabIndex = 47;
            this.label6.Text = "COM口:";
            // 
            // Cmb_Status
            // 
            this.Cmb_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Status.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cmb_Status.FormattingEnabled = true;
            this.Cmb_Status.Items.AddRange(new object[] {
            "",
            "未检测",
            "成功",
            "失败"});
            this.Cmb_Status.Location = new System.Drawing.Point(633, 35);
            this.Cmb_Status.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_Status.Name = "Cmb_Status";
            this.Cmb_Status.Size = new System.Drawing.Size(164, 31);
            this.Cmb_Status.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(554, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 23);
            this.label5.TabIndex = 44;
            this.label5.Text = "检测结果";
            // 
            // Cmb_HardList
            // 
            this.Cmb_HardList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_HardList.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cmb_HardList.FormattingEnabled = true;
            this.Cmb_HardList.Items.AddRange(new object[] {
            "",
            "A1",
            "C1"});
            this.Cmb_HardList.Location = new System.Drawing.Point(363, 82);
            this.Cmb_HardList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_HardList.Name = "Cmb_HardList";
            this.Cmb_HardList.Size = new System.Drawing.Size(164, 31);
            this.Cmb_HardList.TabIndex = 43;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(284, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 23);
            this.label4.TabIndex = 42;
            this.label4.Text = "硬件类型";
            // 
            // Text_BrantchNumber
            // 
            this.Text_BrantchNumber.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Text_BrantchNumber.Location = new System.Drawing.Point(99, 85);
            this.Text_BrantchNumber.Name = "Text_BrantchNumber";
            this.Text_BrantchNumber.Size = new System.Drawing.Size(161, 29);
            this.Text_BrantchNumber.TabIndex = 41;
            this.Text_BrantchNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Text_PageNo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(22, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 23);
            this.label3.TabIndex = 40;
            this.label3.Text = "批 次 号 ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(282, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 23);
            this.label2.TabIndex = 39;
            this.label2.Text = "结束时间";
            // 
            // Time_End
            // 
            this.Time_End.CustomFormat = "yyyy年MM月dd";
            this.Time_End.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Time_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Time_End.Location = new System.Drawing.Point(363, 37);
            this.Time_End.Margin = new System.Windows.Forms.Padding(4);
            this.Time_End.Name = "Time_End";
            this.Time_End.Size = new System.Drawing.Size(164, 29);
            this.Time_End.TabIndex = 38;
            // 
            // Time_Begin
            // 
            this.Time_Begin.CustomFormat = "yyyy年MM月dd";
            this.Time_Begin.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Time_Begin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Time_Begin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Time_Begin.Location = new System.Drawing.Point(99, 37);
            this.Time_Begin.Margin = new System.Windows.Forms.Padding(4);
            this.Time_Begin.Name = "Time_Begin";
            this.Time_Begin.Size = new System.Drawing.Size(161, 29);
            this.Time_Begin.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(20, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 36;
            this.label1.Text = "开始时间";
            // 
            // Panel_Bottom
            // 
            this.Panel_Bottom.Controls.Add(this.Grid_Data);
            this.Panel_Bottom.Controls.Add(this.Panel_Page);
            this.Panel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Bottom.Location = new System.Drawing.Point(0, 141);
            this.Panel_Bottom.Name = "Panel_Bottom";
            this.Panel_Bottom.Size = new System.Drawing.Size(1355, 689);
            this.Panel_Bottom.TabIndex = 1;
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
            this.Grid_Data.Location = new System.Drawing.Point(0, 0);
            this.Grid_Data.Margin = new System.Windows.Forms.Padding(4);
            this.Grid_Data.Name = "Grid_Data";
            this.Grid_Data.ReadOnly = true;
            this.Grid_Data.RowHeadersWidth = 51;
            this.Grid_Data.RowTemplate.Height = 23;
            this.Grid_Data.Size = new System.Drawing.Size(1355, 637);
            this.Grid_Data.TabIndex = 14;
            // 
            // Panel_Page
            // 
            this.Panel_Page.Controls.Add(this.Cmb_PageSize);
            this.Panel_Page.Controls.Add(this.Btn_Jonp);
            this.Panel_Page.Controls.Add(this.Text_PageNo);
            this.Panel_Page.Controls.Add(this.Btn_EndPage);
            this.Panel_Page.Controls.Add(this.Btn_NextPage);
            this.Panel_Page.Controls.Add(this.Btn_PrevPage);
            this.Panel_Page.Controls.Add(this.Btn_FirstPage);
            this.Panel_Page.Controls.Add(this.Lb_PageInfo);
            this.Panel_Page.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_Page.Location = new System.Drawing.Point(0, 637);
            this.Panel_Page.Name = "Panel_Page";
            this.Panel_Page.Size = new System.Drawing.Size(1355, 52);
            this.Panel_Page.TabIndex = 0;
            // 
            // Cmb_PageSize
            // 
            this.Cmb_PageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cmb_PageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_PageSize.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Cmb_PageSize.FormatString = "000";
            this.Cmb_PageSize.FormattingEnabled = true;
            this.Cmb_PageSize.Items.AddRange(new object[] {
            "50",
            "100",
            "150",
            "200",
            "250",
            "300",
            "500",
            "800",
            "1000",
            "1500",
            "2000"});
            this.Cmb_PageSize.Location = new System.Drawing.Point(1204, 9);
            this.Cmb_PageSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Cmb_PageSize.Name = "Cmb_PageSize";
            this.Cmb_PageSize.Size = new System.Drawing.Size(123, 31);
            this.Cmb_PageSize.TabIndex = 47;
            this.Cmb_PageSize.SelectedIndexChanged += new System.EventHandler(this.Cmb_PageSize_SelectedIndexChanged);
            // 
            // Btn_Jonp
            // 
            this.Btn_Jonp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Jonp.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Jonp.Location = new System.Drawing.Point(1105, 8);
            this.Btn_Jonp.Name = "Btn_Jonp";
            this.Btn_Jonp.Size = new System.Drawing.Size(75, 36);
            this.Btn_Jonp.TabIndex = 43;
            this.Btn_Jonp.Text = "GO";
            this.Btn_Jonp.UseVisualStyleBackColor = true;
            this.Btn_Jonp.Click += new System.EventHandler(this.Btn_Jonp_Click);
            // 
            // Text_PageNo
            // 
            this.Text_PageNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_PageNo.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Text_PageNo.Location = new System.Drawing.Point(995, 12);
            this.Text_PageNo.Name = "Text_PageNo";
            this.Text_PageNo.Size = new System.Drawing.Size(95, 29);
            this.Text_PageNo.TabIndex = 42;
            this.Text_PageNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Text_PageNo_KeyPress);
            // 
            // Btn_EndPage
            // 
            this.Btn_EndPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_EndPage.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_EndPage.Location = new System.Drawing.Point(895, 8);
            this.Btn_EndPage.Name = "Btn_EndPage";
            this.Btn_EndPage.Size = new System.Drawing.Size(75, 36);
            this.Btn_EndPage.TabIndex = 4;
            this.Btn_EndPage.Text = "末页";
            this.Btn_EndPage.UseVisualStyleBackColor = true;
            this.Btn_EndPage.Click += new System.EventHandler(this.Btn_EndPage_Click);
            // 
            // Btn_NextPage
            // 
            this.Btn_NextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_NextPage.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_NextPage.Location = new System.Drawing.Point(805, 8);
            this.Btn_NextPage.Name = "Btn_NextPage";
            this.Btn_NextPage.Size = new System.Drawing.Size(75, 36);
            this.Btn_NextPage.TabIndex = 3;
            this.Btn_NextPage.Text = "下一页";
            this.Btn_NextPage.UseVisualStyleBackColor = true;
            this.Btn_NextPage.Click += new System.EventHandler(this.Btn_NextPage_Click);
            // 
            // Btn_PrevPage
            // 
            this.Btn_PrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_PrevPage.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_PrevPage.Location = new System.Drawing.Point(720, 8);
            this.Btn_PrevPage.Name = "Btn_PrevPage";
            this.Btn_PrevPage.Size = new System.Drawing.Size(75, 36);
            this.Btn_PrevPage.TabIndex = 2;
            this.Btn_PrevPage.Text = "上一页";
            this.Btn_PrevPage.UseVisualStyleBackColor = true;
            this.Btn_PrevPage.Click += new System.EventHandler(this.Btn_PrevPage_Click);
            // 
            // Btn_FirstPage
            // 
            this.Btn_FirstPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_FirstPage.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_FirstPage.Location = new System.Drawing.Point(633, 8);
            this.Btn_FirstPage.Name = "Btn_FirstPage";
            this.Btn_FirstPage.Size = new System.Drawing.Size(75, 36);
            this.Btn_FirstPage.TabIndex = 1;
            this.Btn_FirstPage.Text = "首页";
            this.Btn_FirstPage.UseVisualStyleBackColor = true;
            this.Btn_FirstPage.Click += new System.EventHandler(this.Btn_FirstPage_Click);
            // 
            // Lb_PageInfo
            // 
            this.Lb_PageInfo.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lb_PageInfo.Location = new System.Drawing.Point(23, 8);
            this.Lb_PageInfo.Name = "Lb_PageInfo";
            this.Lb_PageInfo.Size = new System.Drawing.Size(487, 35);
            this.Lb_PageInfo.TabIndex = 0;
            this.Lb_PageInfo.Text = "共0条记录     0/0";
            this.Lb_PageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 830);
            this.Controls.Add(this.Panel_Bottom);
            this.Controls.Add(this.Panel_Top);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检测记录查询";
            this.Load += new System.EventHandler(this.FormSearch_Load);
            this.Panel_Top.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Panel_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Data)).EndInit();
            this.Panel_Page.ResumeLayout(false);
            this.Panel_Page.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Top;
        private System.Windows.Forms.Panel Panel_Bottom;
        private System.Windows.Forms.Panel Panel_Page;
        private System.Windows.Forms.DataGridView Grid_Data;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker Time_End;
        private System.Windows.Forms.DateTimePicker Time_Begin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Text_BrantchNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Cmb_HardList;
        private System.Windows.Forms.ComboBox Cmb_Status;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Cmb_PortList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Btn_Serach;
        private System.Windows.Forms.Button Btn_Clear;
        private System.Windows.Forms.Label Lb_PageInfo;
        private System.Windows.Forms.Button Btn_FirstPage;
        private System.Windows.Forms.Button Btn_PrevPage;
        private System.Windows.Forms.TextBox Text_PageNo;
        private System.Windows.Forms.Button Btn_EndPage;
        private System.Windows.Forms.Button Btn_NextPage;
        private System.Windows.Forms.Button Btn_Jonp;
        private System.Windows.Forms.ComboBox Cmb_PageSize;
    }
}