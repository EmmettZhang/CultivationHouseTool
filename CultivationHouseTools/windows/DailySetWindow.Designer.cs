namespace CultivationHouseTools
{
    partial class DailySetWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.boss = new System.Windows.Forms.ComboBox();
            this.setConfirm = new System.Windows.Forms.Button();
            this.monday = new System.Windows.Forms.CheckBox();
            this.tuesday = new System.Windows.Forms.CheckBox();
            this.wednesday = new System.Windows.Forms.CheckBox();
            this.thursday = new System.Windows.Forms.CheckBox();
            this.friday = new System.Windows.Forms.CheckBox();
            this.luckyCount = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.happyBag = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.attackMethod = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "是否进行每日兑换";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "boss报名";
            // 
            // boss
            // 
            this.boss.FormattingEnabled = true;
            this.boss.Items.AddRange(new object[] {
            "饕餮",
            "穷奇",
            "毕方",
            "朱厌",
            "麒麟",
            "貔貅",
            "白泽"});
            this.boss.Location = new System.Drawing.Point(136, 134);
            this.boss.Name = "boss";
            this.boss.Size = new System.Drawing.Size(121, 20);
            this.boss.TabIndex = 3;
            this.boss.Text = "饕餮";
            // 
            // setConfirm
            // 
            this.setConfirm.Location = new System.Drawing.Point(182, 304);
            this.setConfirm.Name = "setConfirm";
            this.setConfirm.Size = new System.Drawing.Size(75, 23);
            this.setConfirm.TabIndex = 4;
            this.setConfirm.Text = "确定";
            this.setConfirm.UseVisualStyleBackColor = true;
            this.setConfirm.Click += new System.EventHandler(this.setConfirm_Click);
            // 
            // monday
            // 
            this.monday.AutoSize = true;
            this.monday.Location = new System.Drawing.Point(161, 15);
            this.monday.Name = "monday";
            this.monday.Size = new System.Drawing.Size(48, 16);
            this.monday.TabIndex = 5;
            this.monday.Text = "周一";
            this.monday.UseVisualStyleBackColor = true;
            // 
            // tuesday
            // 
            this.tuesday.AutoSize = true;
            this.tuesday.Location = new System.Drawing.Point(161, 37);
            this.tuesday.Name = "tuesday";
            this.tuesday.Size = new System.Drawing.Size(48, 16);
            this.tuesday.TabIndex = 6;
            this.tuesday.Text = "周二";
            this.tuesday.UseVisualStyleBackColor = true;
            // 
            // wednesday
            // 
            this.wednesday.AutoSize = true;
            this.wednesday.Location = new System.Drawing.Point(161, 59);
            this.wednesday.Name = "wednesday";
            this.wednesday.Size = new System.Drawing.Size(48, 16);
            this.wednesday.TabIndex = 7;
            this.wednesday.Text = "周三";
            this.wednesday.UseVisualStyleBackColor = true;
            // 
            // thursday
            // 
            this.thursday.AutoSize = true;
            this.thursday.Location = new System.Drawing.Point(161, 81);
            this.thursday.Name = "thursday";
            this.thursday.Size = new System.Drawing.Size(48, 16);
            this.thursday.TabIndex = 8;
            this.thursday.Text = "周四";
            this.thursday.UseVisualStyleBackColor = true;
            // 
            // friday
            // 
            this.friday.AutoSize = true;
            this.friday.Location = new System.Drawing.Point(161, 103);
            this.friday.Name = "friday";
            this.friday.Size = new System.Drawing.Size(48, 16);
            this.friday.TabIndex = 9;
            this.friday.Text = "周五";
            this.friday.UseVisualStyleBackColor = true;
            // 
            // luckyCount
            // 
            this.luckyCount.FormattingEnabled = true;
            this.luckyCount.Items.AddRange(new object[] {
            "是",
            "否"});
            this.luckyCount.Location = new System.Drawing.Point(136, 194);
            this.luckyCount.Name = "luckyCount";
            this.luckyCount.Size = new System.Drawing.Size(121, 20);
            this.luckyCount.TabIndex = 11;
            this.luckyCount.Text = "是";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "幸运点";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "是否自动兑换5次幸运点和10次仙币福袋";
            // 
            // happyBag
            // 
            this.happyBag.FormattingEnabled = true;
            this.happyBag.Items.AddRange(new object[] {
            "是",
            "否"});
            this.happyBag.Location = new System.Drawing.Point(136, 225);
            this.happyBag.Name = "happyBag";
            this.happyBag.Size = new System.Drawing.Size(121, 20);
            this.happyBag.TabIndex = 14;
            this.happyBag.Text = "否";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "福袋";
            // 
            // attackMethod
            // 
            this.attackMethod.FormattingEnabled = true;
            this.attackMethod.Items.AddRange(new object[] {
            "物攻",
            "道攻"});
            this.attackMethod.Location = new System.Drawing.Point(136, 260);
            this.attackMethod.Name = "attackMethod";
            this.attackMethod.Size = new System.Drawing.Size(121, 20);
            this.attackMethod.TabIndex = 16;
            this.attackMethod.Text = "物攻";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 263);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "攻击方式";
            // 
            // DailySetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 353);
            this.Controls.Add(this.attackMethod);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.happyBag);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.luckyCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.friday);
            this.Controls.Add(this.thursday);
            this.Controls.Add(this.wednesday);
            this.Controls.Add(this.tuesday);
            this.Controls.Add(this.monday);
            this.Controls.Add(this.setConfirm);
            this.Controls.Add(this.boss);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DailySetWindow";
            this.Text = "DailySet";
            this.Load += new System.EventHandler(this.DailySetWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox boss;
        private System.Windows.Forms.Button setConfirm;
        private System.Windows.Forms.CheckBox monday;
        private System.Windows.Forms.CheckBox tuesday;
        private System.Windows.Forms.CheckBox wednesday;
        private System.Windows.Forms.CheckBox thursday;
        private System.Windows.Forms.CheckBox friday;
        private System.Windows.Forms.ComboBox luckyCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox happyBag;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox attackMethod;
        private System.Windows.Forms.Label label6;
    }
}