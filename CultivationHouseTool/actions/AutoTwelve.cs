using CultivationHouseTool.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace CultivationHouseTool.actions
{
    internal class AutoTwelve
    {
        private CancellationTokenSource _cts;
        private Task _task;
        private Random _random = new Random();
        private int timeoutMs = 10000;
        private int interval = 100;
        private int elapsed = 0;

        private MainWindow _form;
        private static List<TimeSpan> times = new List<TimeSpan>() { new TimeSpan(12, 0, 0) };

        public AutoTwelve(MainWindow form)
        {
            _form = form;
        }

        public void run()
        {
            if (_cts != null)
            {
                Common.addMessage(_form.dailyMessage, "自动真Boss已开始，请先停止");
                return;
            }

            _cts = new CancellationTokenSource();

            _task = Task.Run(() => RunSchedule(_cts.Token));

            Common.addMessage(_form.dailyMessage, "自动真Boss已开始");
        }

        private async Task RunSchedule(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                DateTime now = DateTime.Now;

                DateTime? next = null;
                // 0~30 秒随机浮动
                int jitter = _random.Next(0, 30);

                foreach (var t in times)
                {
                    DateTime dt = now.Date.Add(t);

                    if (dt > now)
                    {
                        next = dt.AddSeconds(jitter);
                        break;
                    }
                }

                if (next == null)
                {
                    next = now.Date.AddDays(1).Add(times[0]).AddSeconds(jitter);
                }

                TimeSpan wait = next.Value - now;

                Common.addMessage(_form.dailyMessage, "自动真Boss下次执行：" + next.Value);

                await Task.Delay(wait, token);

                DoWork();
            }
        }

        /**
         * 
         * 自动日常说明
         * 每日八点自动签到、葫芦签到、播撒灵露、门派演武、报名boss、购买金币精力和金币福袋、购买每日兑换。
         * 每日八点十分、十三点十分自动收割并播种门派后山。
         * 每日十六点自动获取boss结果，周五十六点，自动获取门派分成。
         * 每周一八点自动收获葫芦。
         * 
         */
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

            realBoss(mainWindow);

            // 如果是周一
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                hulu(mainWindow);
            }

            // 如果是周五
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                ladder(mainWindow);
            }
        }

        private void realBoss(AutomationElement mainWindow)
        {
            Common.changeTab(mainWindow, "BOSS", 0);
            Common.clickButton(mainWindow, "真·BOSS烛龙");

            AutomationElement bossWindow = null;
            elapsed = 0;
            while (elapsed < timeoutMs)
            {
                bossWindow = Common.getWindow("真·世界BOSS");

                if (bossWindow != null)
                    break;

                Thread.Sleep(interval);
                elapsed += interval;
            }

            if (bossWindow != null)
            {
                if (DailySet.attackMethod == "物攻")
                {
                    Common.clickButton(bossWindow, "3秒自动物理攻击");
                    Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},真BOSS自动物理攻击");
                }
                else if (DailySet.attackMethod == "道攻")
                {
                    Common.clickButton(bossWindow, "3秒自动道术攻击");
                    Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},真BOSS自动道术攻击");
                }

                // 等待十分钟，确保能打完，加5-10秒随机偏移，避免每次都在同一时间点点击抽奖
                Thread.Sleep((10 * 60 * 1000) + new Random().Next(5000, 10000));
                Common.clickButton(bossWindow, "抽取奖励");
                Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},抽取真BOSS奖励");
                Common.clickButtonById(bossWindow, "Close");
            }
        }


        // 葫芦
        public void hulu(AutomationElement mainWindow)
        {
            Common.clickButton(mainWindow, "仙玉商店");
            AutomationElement jadeWindow = null;
            elapsed = 0;
            while (elapsed < timeoutMs)
            {
                jadeWindow = Common.getWindow("仙玉商店");

                if (jadeWindow != null)
                    break;

                Thread.Sleep(interval);
                elapsed += interval;
            }
            if (jadeWindow != null)
            {
                Common.clickButton(jadeWindow, "宝葫芦玩法");
                AutomationElement huluWindow = null;
                elapsed = 0;
                while (elapsed < timeoutMs)
                {
                    huluWindow = Common.getWindow("宝葫芦");

                    if (huluWindow != null)
                        break;

                    Thread.Sleep(interval);
                    elapsed += interval;
                }
                if (huluWindow != null)
                {
                    Common.clickButton(huluWindow, "收获葫芦");
                    Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},收获宝葫芦");
                    Thread.Sleep(new Random().Next(1000, 2000));
                    Common.clickButtonById(huluWindow, "Close");
                    Common.clickButtonById(jadeWindow, "Close");
                    Thread.Sleep(new Random().Next(500, 1000));
                }
            }
        }

        public void ladder(AutomationElement mainWindow)
        {
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

            Common.clickButton(ladderWindow, "领取每周段位奖励");
            Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},领取天梯赛每周段位奖励");
        }
        public void stop()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
                Common.addMessage(_form.dailyMessage, "自动真Boss已停止");
            }
        }
    }
}
