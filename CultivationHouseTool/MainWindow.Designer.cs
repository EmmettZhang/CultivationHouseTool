namespace CultivationHouseTool
{
    partial class MainWindow
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.title = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dayTask = new System.Windows.Forms.Button();
            this.dailySet = new System.Windows.Forms.Button();
            this.dailyMessage = new System.Windows.Forms.TextBox();
            this.dailyStatus = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.ForeColor = System.Drawing.SystemColors.WindowText;
            this.title.Location = new System.Drawing.Point(95, 12);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(149, 21);
            this.title.TabIndex = 6;
            this.title.Text = "修仙小屋 v2.30.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "小屋窗口标题";
            // 
            // dayTask
            // 
            this.dayTask.Location = new System.Drawing.Point(377, 65);
            this.dayTask.Name = "dayTask";
            this.dayTask.Size = new System.Drawing.Size(75, 23);
            this.dayTask.TabIndex = 18;
            this.dayTask.Text = "自动日常";
            this.dayTask.UseVisualStyleBackColor = true;
            this.dayTask.Click += new System.EventHandler(this.dayTask_Click);
            // 
            // dailySet
            // 
            this.dailySet.Location = new System.Drawing.Point(377, 94);
            this.dailySet.Name = "dailySet";
            this.dailySet.Size = new System.Drawing.Size(75, 23);
            this.dailySet.TabIndex = 21;
            this.dailySet.Text = "日常配置";
            this.dailySet.UseVisualStyleBackColor = true;
            this.dailySet.Click += new System.EventHandler(this.dailySet_Click);
            // 
            // dailyMessage
            // 
            this.dailyMessage.Location = new System.Drawing.Point(12, 159);
            this.dailyMessage.Multiline = true;
            this.dailyMessage.Name = "dailyMessage";
            this.dailyMessage.ReadOnly = true;
            this.dailyMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dailyMessage.Size = new System.Drawing.Size(447, 296);
            this.dailyMessage.TabIndex = 22;
            // 
            // dailyStatus
            // 
            this.dailyStatus.AutoSize = true;
            this.dailyStatus.Font = new System.Drawing.Font("宋体", 9F);
            this.dailyStatus.Location = new System.Drawing.Point(370, 16);
            this.dailyStatus.Name = "dailyStatus";
            this.dailyStatus.Size = new System.Drawing.Size(89, 12);
            this.dailyStatus.TabIndex = 23;
            this.dailyStatus.Text = "自动日常未开始";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 39);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(357, 114);
            this.textBox1.TabIndex = 25;
            this.textBox1.Text = "自动日常说明\r\n每日八点自动签到、领取当前每日签到福利、葫芦签到、播撒灵露、自动开始单双修、门派演武、报名boss、购买金币精力和金币福袋、购买仙币幸运点和福袋、" + "购买每日兑换、发红包和福包。\r\n每日八点十分、十三点十分收割并播种门派后山。\r\n每日十二点打真BOSS。每周一十二点收获葫芦。每周五十二点自动领取天梯奖励\r\n每" + "日十六点自动开始历练，获取boss结果，每周五十六点获取门派本周分成。\r\n";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(250, 16);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(29, 12);
            this.timeLabel.TabIndex = 27;
            this.timeLabel.Text = "时间";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 464);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dailyStatus);
            this.Controls.Add(this.dailyMessage);
            this.Controls.Add(this.dailySet);
            this.Controls.Add(this.dayTask);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.title);
            this.Name = "MainWindow";
            this.Text = "CultivationHouseTool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        public System.Windows.Forms.TextBox title;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button dayTask;
        private System.Windows.Forms.Button dailySet;
        public System.Windows.Forms.TextBox dailyMessage;
        private System.Windows.Forms.Label dailyStatus;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label timeLabel;
    }
}

