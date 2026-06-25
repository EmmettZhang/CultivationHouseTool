using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using CommonUtils;
using CommonUtils.lib;

namespace CultivationHouseTools.actions
{
    internal class AutoLadder
    {

        private CancellationTokenSource _cts;
        private Task _task;
        private Random _random = new Random();
        private int timeoutMs = 10000;
        private int interval = 100;
        private int elapsed = 0;
        private int runTimes = 0;

        private MainWindow _form;

        public AutoLadder(MainWindow form)
        {
            _form = form;
        }

        public void run()
        {
            if (_cts != null)
            {
                Common.addMessage(_form.message, "自动天梯已开始，请先停止");
                return;
            }
            runTimes = 0;
            _cts = new CancellationTokenSource();

            _task = Task.Run(() => RunSchedule(_cts.Token));

            Common.addMessage(_form.message, "自动天梯已开始");
        }

        private async Task RunSchedule(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                DoWork();
                runTimes++;
                if (runTimes >= 20) {
                    Common.addMessage(_form.message, "自动天梯已执行20次，自动停止");
                    stop();
                    return;
                }
                DateTime now = DateTime.Now;

                // 0~30 秒随机浮动
                int jitter = _random.Next(0, 30);
                DateTime next = now.AddMinutes(3).AddSeconds(jitter);

                TimeSpan wait = next - now;

                Common.addMessage(_form.message, "自动天梯下次执行：" + next);

                await Task.Delay(wait, token);
            }
        }

        public void DoWork()
        {
            AutomationElement mainWindow = null;
            elapsed = 0;
            while (elapsed < timeoutMs)
            {
                mainWindow = Common.getWindow(_form.title.Text.Trim());

                if (mainWindow != null)
                    break;

                Thread.Sleep(interval);
                elapsed += interval;
            }

            if (mainWindow == null)
            {
                Common.addMessage(_form.message, "未找到修仙小屋窗口，请确保游戏正在运行并且窗口标题正确");
                return;
            }

            Common.changeTab(mainWindow, "娱乐", 0);
            Common.clickButton(mainWindow, "天梯赛");

            AutomationElement ladderWindow = null;
            elapsed = 0;
            while (elapsed < timeoutMs)
            {
                ladderWindow = Common.getWindow("天梯赛");

                if (ladderWindow != null)
                    break;

                Thread.Sleep(interval);
                elapsed += interval;
            }

            if (ladderWindow == null)
            {
                return;
            }

            // 设置攻击方式
            if (DailySet.attackMethod == "物攻")
            {
                Common.selectRadioButtonById(ladderWindow, "wuLiGongJi");
            }
            else if (DailySet.attackMethod == "道攻")
            {
                Common.selectRadioButtonById(ladderWindow, "daoShuGongJi");
            }

            Common.clickButtonById(ladderWindow, "KaiShiPiPei_Button");
            Common.clickButtonById(ladderWindow, "KaiShiPiPei_Button");
        }

        public void stop()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
                Common.addMessage(_form.message, "自动天梯已停止");
            }
        }
    }
}
