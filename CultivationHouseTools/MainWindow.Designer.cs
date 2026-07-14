namespace CultivationHouseTools
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
            this.refresh = new System.Windows.Forms.Button();
            this.flip = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flipNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.stopRefresh = new System.Windows.Forms.Button();
            this.stopFlip = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.TextBox();
            this.shopNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.stopUnknownBox = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.unknownNum = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.unknownBox = new System.Windows.Forms.Button();
            this.dayTask = new System.Windows.Forms.Button();
            this.unknownIndex = new System.Windows.Forms.TextBox();
            this.dailySet = new System.Windows.Forms.Button();
            this.dailyMessage = new System.Windows.Forms.TextBox();
            this.dailyStatus = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flipMaxCount = new System.Windows.Forms.TextBox();
            this.stopLadder = new System.Windows.Forms.Button();
            this.ladder = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(183, 3);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(75, 23);
            this.refresh.TabIndex = 0;
            this.refresh.Text = "开始购物";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_ClickAsync);
            // 
            // flip
            // 
            this.flip.Location = new System.Drawing.Point(183, 62);
            this.flip.Name = "flip";
            this.flip.Size = new System.Drawing.Size(75, 23);
            this.flip.TabIndex = 1;
            this.flip.Text = "开始翻卡";
            this.flip.UseVisualStyleBackColor = true;
            this.flip.Click += new System.EventHandler(this.flip_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "最大翻卡数";
            // 
            // flipNum
            // 
            this.flipNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flipNum.Location = new System.Drawing.Point(14, 91);
            this.flipNum.Name = "flipNum";
            this.flipNum.Size = new System.Drawing.Size(53, 23);
            this.flipNum.TabIndex = 3;
            this.flipNum.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "心悦卡牌次数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "幸运商店";
            // 
            // title
            // 
            this.title.ForeColor = System.Drawing.SystemColors.WindowText;
            this.title.Location = new System.Drawing.Point(95, 12);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(149, 21);
            this.title.TabIndex = 6;
            this.title.Text = "修仙小屋 v2.31.0";
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
            // stopRefresh
            // 
            this.stopRefresh.Location = new System.Drawing.Point(183, 33);
            this.stopRefresh.Name = "stopRefresh";
            this.stopRefresh.Size = new System.Drawing.Size(75, 23);
            this.stopRefresh.TabIndex = 8;
            this.stopRefresh.Text = "结束购物";
            this.stopRefresh.UseVisualStyleBackColor = true;
            this.stopRefresh.Click += new System.EventHandler(this.stopRefresh_Click);
            // 
            // stopFlip
            // 
            this.stopFlip.Location = new System.Drawing.Point(183, 91);
            this.stopFlip.Name = "stopFlip";
            this.stopFlip.Size = new System.Drawing.Size(75, 23);
            this.stopFlip.TabIndex = 9;
            this.stopFlip.Text = "结束翻卡";
            this.stopFlip.UseVisualStyleBackColor = true;
            this.stopFlip.Click += new System.EventHandler(this.stopFlip_Click);
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(12, 254);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.ReadOnly = true;
            this.message.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.message.Size = new System.Drawing.Size(261, 296);
            this.message.TabIndex = 10;
            // 
            // shopNum
            // 
            this.shopNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shopNum.Location = new System.Drawing.Point(102, 33);
            this.shopNum.Name = "shopNum";
            this.shopNum.Size = new System.Drawing.Size(53, 23);
            this.shopNum.TabIndex = 12;
            this.shopNum.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "购物次数";
            // 
            // stopUnknownBox
            // 
            this.stopUnknownBox.Location = new System.Drawing.Point(183, 149);
            this.stopUnknownBox.Name = "stopUnknownBox";
            this.stopUnknownBox.Size = new System.Drawing.Size(75, 23);
            this.stopUnknownBox.TabIndex = 17;
            this.stopUnknownBox.Text = "结束抽盲盒";
            this.stopUnknownBox.UseVisualStyleBackColor = true;
            this.stopUnknownBox.Click += new System.EventHandler(this.stopUnknownBox_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "心愿盲盒";
            // 
            // unknownNum
            // 
            this.unknownNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.unknownNum.Location = new System.Drawing.Point(102, 149);
            this.unknownNum.Name = "unknownNum";
            this.unknownNum.Size = new System.Drawing.Size(53, 23);
            this.unknownNum.TabIndex = 15;
            this.unknownNum.Text = "50";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "盲盒次数";
            // 
            // unknownBox
            // 
            this.unknownBox.Location = new System.Drawing.Point(183, 120);
            this.unknownBox.Name = "unknownBox";
            this.unknownBox.Size = new System.Drawing.Size(75, 23);
            this.unknownBox.TabIndex = 13;
            this.unknownBox.Text = "开始抽盲盒";
            this.unknownBox.UseVisualStyleBackColor = true;
            this.unknownBox.Click += new System.EventHandler(this.unknownBox_Click);
            // 
            // dayTask
            // 
            this.dayTask.Location = new System.Drawing.Point(292, 43);
            this.dayTask.Name = "dayTask";
            this.dayTask.Size = new System.Drawing.Size(75, 23);
            this.dayTask.TabIndex = 18;
            this.dayTask.Text = "自动日常";
            this.dayTask.UseVisualStyleBackColor = true;
            this.dayTask.Click += new System.EventHandler(this.dayTask_Click);
            // 
            // unknownIndex
            // 
            this.unknownIndex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.unknownIndex.Location = new System.Drawing.Point(14, 149);
            this.unknownIndex.Name = "unknownIndex";
            this.unknownIndex.Size = new System.Drawing.Size(53, 23);
            this.unknownIndex.TabIndex = 20;
            this.unknownIndex.Text = "1";
            // 
            // dailySet
            // 
            this.dailySet.Location = new System.Drawing.Point(373, 43);
            this.dailySet.Name = "dailySet";
            this.dailySet.Size = new System.Drawing.Size(75, 23);
            this.dailySet.TabIndex = 21;
            this.dailySet.Text = "日常配置";
            this.dailySet.UseVisualStyleBackColor = true;
            this.dailySet.Click += new System.EventHandler(this.dailySet_Click);
            // 
            // dailyMessage
            // 
            this.dailyMessage.Location = new System.Drawing.Point(279, 254);
            this.dailyMessage.Multiline = true;
            this.dailyMessage.Name = "dailyMessage";
            this.dailyMessage.ReadOnly = true;
            this.dailyMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dailyMessage.Size = new System.Drawing.Size(180, 296);
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
            this.textBox1.Location = new System.Drawing.Point(279, 72);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(180, 176);
            this.textBox1.TabIndex = 25;
            this.textBox1.Text = "自动日常说明\r\n每日八点自动签到、领取当前每日签到福利、葫芦签到、播撒灵露、自动开始单双修、门派演武、报名boss、购买金币精力和金币福袋、购买仙币幸运点和福袋、" + "购买每日兑换、发红包和福包。\r\n每日八点十分、十三点十分收割并播种门派后山。\r\n每日十二点打真BOSS。每周一十二点收获葫芦。每周五十二点自动领取天梯奖励\r\n每" + "日十六点自动开始历练，获取boss结果，每周五十六点获取门派本周分成。\r\n";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flipMaxCount);
            this.panel1.Controls.Add(this.stopLadder);
            this.panel1.Controls.Add(this.ladder);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.refresh);
            this.panel1.Controls.Add(this.flip);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.flipNum);
            this.panel1.Controls.Add(this.unknownIndex);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.stopRefresh);
            this.panel1.Controls.Add(this.stopFlip);
            this.panel1.Controls.Add(this.stopUnknownBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.shopNum);
            this.panel1.Controls.Add(this.unknownNum);
            this.panel1.Controls.Add(this.unknownBox);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(12, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 209);
            this.panel1.TabIndex = 26;
            // 
            // flipMaxCount
            // 
            this.flipMaxCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flipMaxCount.Location = new System.Drawing.Point(102, 91);
            this.flipMaxCount.Name = "flipMaxCount";
            this.flipMaxCount.Size = new System.Drawing.Size(53, 23);
            this.flipMaxCount.TabIndex = 24;
            this.flipMaxCount.Text = "21";
            // 
            // stopLadder
            // 
            this.stopLadder.Location = new System.Drawing.Point(183, 178);
            this.stopLadder.Name = "stopLadder";
            this.stopLadder.Size = new System.Drawing.Size(75, 23);
            this.stopLadder.TabIndex = 23;
            this.stopLadder.Text = "结束天梯";
            this.stopLadder.UseVisualStyleBackColor = true;
            this.stopLadder.Click += new System.EventHandler(this.stopLadder_Click);
            // 
            // ladder
            // 
            this.ladder.Location = new System.Drawing.Point(92, 178);
            this.ladder.Name = "ladder";
            this.ladder.Size = new System.Drawing.Size(75, 23);
            this.ladder.TabIndex = 22;
            this.ladder.Text = "开始天梯";
            this.ladder.UseVisualStyleBackColor = true;
            this.ladder.Click += new System.EventHandler(this.ladder_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "天梯20次";
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
            this.ClientSize = new System.Drawing.Size(471, 562);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dailyStatus);
            this.Controls.Add(this.dailyMessage);
            this.Controls.Add(this.dailySet);
            this.Controls.Add(this.dayTask);
            this.Controls.Add(this.message);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.title);
            this.Name = "MainWindow";
            this.Text = "CultivationHouseTools";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Button flip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox title;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button stopRefresh;
        private System.Windows.Forms.Button stopFlip;
        public System.Windows.Forms.TextBox message;
        public System.Windows.Forms.TextBox shopNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button stopUnknownBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button unknownBox;
        private System.Windows.Forms.Button dayTask;
        private System.Windows.Forms.Button dailySet;
        public System.Windows.Forms.TextBox dailyMessage;
        private System.Windows.Forms.Label dailyStatus;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox unknownIndex;
        public System.Windows.Forms.TextBox unknownNum;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Button stopLadder;
        private System.Windows.Forms.Button ladder;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox flipMaxCount;
        public System.Windows.Forms.TextBox flipNum;
    }
}

