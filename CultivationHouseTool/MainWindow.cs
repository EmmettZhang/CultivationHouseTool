using CultivationHouseTool.actions;
using CommonUtils.lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using CommonUtils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CultivationHouseTool
{
    public partial class MainWindow : Form
    {
        private AutoEight _autoSignIn;
        private AutoHarvest _autoHarvest;
        private AutoSixteen _autoBoss;
        private AutoFourteen _autoFourteen;
        private AutoTwelve _autoTwelve;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DailySet.dailyStatus = "自动日常已停止";
            dailyStatus.Text = DailySet.dailyStatus;
            dailyStatus.ForeColor = Color.Red;

            DailySet.monday = false;
            DailySet.tuesday = false;
            DailySet.wednesday = false;
            DailySet.thursday = false;
            DailySet.friday = false;

            DailySet.boss = "饕餮";
            DailySet.luckyCount = "5";
            DailySet.happyBag = "0";
            DailySet.attackMethod = "物攻";
            DailySet.attribute = "金";
            Common.addMessage(dailyMessage, DailySet.print());

            _autoSignIn = new AutoEight(this);
            _autoHarvest = new AutoHarvest(this);
            _autoBoss = new AutoSixteen(this);
            _autoFourteen = new AutoFourteen(this);
            _autoTwelve = new AutoTwelve(this);

            UnknownLib.run();
        }

        private void dailySet_Click(object sender, EventArgs e)
        {
            DailySetWindow dailySet = new DailySetWindow(this);
            dailySet.Show();
        }

        private void dayTask_Click(object sender, EventArgs e)
        {
            if (DailySet.dailyStatus == "自动日常已停止")
            {
                DailySet.dailyStatus = "自动日常已开始";
                dailyStatus.Text = DailySet.dailyStatus;
                dailyStatus.ForeColor = Color.Green;

                _autoSignIn.run();
                _autoHarvest.run();
                _autoBoss.run();
                _autoTwelve.run();
                _autoFourteen.run();
            }
            else if (DailySet.dailyStatus == "自动日常已开始")
            {
                DailySet.dailyStatus = "自动日常已停止";
                dailyStatus.Text = DailySet.dailyStatus;
                dailyStatus.ForeColor = Color.Red;

                _autoSignIn.stop();
                _autoHarvest.stop();
                _autoBoss.stop();
                _autoTwelve.stop();
                _autoFourteen.stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
