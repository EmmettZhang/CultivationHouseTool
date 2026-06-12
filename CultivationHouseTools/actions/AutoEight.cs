using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using CultivationHouseTools.lib;

namespace CultivationHouseTools.actions
{
    internal class AutoEight
    {
        private CancellationTokenSource _cts = null;
        private Task _task;
        private int timeoutMs = 10000;
        private int interval = 100;
        private int elapsed = 0;

        private MainWindow _form;
        private static List<TimeSpan> times = new List<TimeSpan>() { new TimeSpan(7, 55, 0) };
        private Random _random = new Random();

        public AutoEight(MainWindow form)
        {
            _form = form;
        }

        public void run()
        {
            if (_cts != null)
            {
                Common.addMessage(_form.dailyMessage, "自动签到已开始，请先停止");
                return;
            }

            _cts = new CancellationTokenSource();

            _task = Task.Run(() => RunSchedule(_cts.Token));

            Common.addMessage(_form.dailyMessage, "自动签到已开始");
        }

        private async Task RunSchedule(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                DateTime now = DateTime.Now;

                DateTime? next = null;
                // 0~600 秒随机浮动
                int jitter = _random.Next(0, 600);

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

                Common.addMessage(_form.dailyMessage, "自动签到下次执行：" + next.Value);

                await Task.Delay(wait, token);

                DoWork();
            }
        }

        /**
         * 
         * 自动日常说明
         * 每日八点自动签到、葫芦签到、播撒灵露、门派演武、报名boss、购买金币精力和金币福袋、购买仙币幸运点和福袋、发红包福包、购买每日兑换。
         * 每日八点十分、十三点十分自动收割并播种门派后山。
         * 每日十六点自动获取boss结果。
         * 每周一八点自动收获葫芦。
         * 
         */
        public void DoWork()
        {
            Common.clearMessage(_form.message);
            Common.clearMessage(_form.dailyMessage);
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

            // 购买每日兑换
            dailyExchange(mainWindow);

            // 签到弹窗
            signIn(mainWindow);

            // 葫芦
            hulu(mainWindow);

            // 门派演武
            sectArena(mainWindow);

            // 报名Boss
            signUpBoss(mainWindow);

            // 购买金币精力和金币福袋
            buyGoldExchange(mainWindow);

            // 购买仙币兑换
            buyXianBiExchange(mainWindow);

            // 购买每日兑换
            dailyExchange(mainWindow);

            // 发红包福包
            sendRedBag(mainWindow);
        }

        // 签到
        public void signIn(AutomationElement mainWindow)
        {
            // 签到弹窗
            Common.clickButton(mainWindow, "签到");
            AutomationElement signWindow = null;
            elapsed = 0;
            while (elapsed < timeoutMs)
            {
                signWindow = Common.getWindow("签到");

                if (signWindow != null)
                    break;

                Thread.Sleep(interval);
                elapsed += interval;
            }

            if (signWindow != null)
            {
                Common.clickButton(signWindow, "点击签到");
                Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},签到");
                // 1-3秒随机偏移
                Thread.Sleep(new Random().Next(1000, 3000));
                Common.clickButton(signWindow, "每日签到福利");
                Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},领取每日签到福利");
                Common.clickButtonById(signWindow, "Close");
                Thread.Sleep(new Random().Next(500, 1000));
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
                    Common.clickButton(huluWindow, "每日签到");
                    Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},宝葫芦签到");
                    Thread.Sleep(new Random().Next(1000, 2000));
                    Common.clickButton(huluWindow, "播撒全部灵露");
                    Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},播撒宝葫芦灵露");
                    Common.clickButtonById(huluWindow, "Close");
                    Common.clickButtonById(jadeWindow, "Close");
                    Thread.Sleep(new Random().Next(500, 1000));
                }
            }
        }

        // 门派演武
        public void sectArena(AutomationElement mainWindow)
        {
            Common.changeTab(mainWindow, "门派", 0);
            Common.changeTab(mainWindow, "建 筑", 1);
            Common.clickButton(mainWindow, "刷新数据");
            // 1-3秒随机偏移
            Thread.Sleep(new Random().Next(1000, 2000));
            Common.clickButton(mainWindow, "演武");
            Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},门派演武");
            // 1-3秒随机偏移
            Thread.Sleep(new Random().Next(1000, 2000));
        }

        // 报名Boss
        public void signUpBoss(AutomationElement mainWindow)
        {
            Common.changeTab(mainWindow, "BOSS", 0);
            int bossIndex = Dics.bossMap[DailySet.boss];
            Common.clickButton(mainWindow, "报名", bossIndex);
            Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},报名{DailySet.boss}");
            // 1-3秒随机偏移
            Thread.Sleep(new Random().Next(1000, 2000));
        }

        // 购买金币精力和金币福袋
        public void buyGoldExchange(AutomationElement mainWindow)
        {
            Common.changeTab(mainWindow, "兑换", 0);
            Common.changeTab(mainWindow, "金币兑换", 1);
            for (int i = 0; i < 10; i++)
            {
                if (i < 3)
                {
                    Common.clickButton(mainWindow, "点击兑换10精力（7000金币）");
                    Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},兑换10金币精力");
                }
                if (i >= 3)
                {
                    Common.clickButton(mainWindow, "点击兑换1精力（700金币）");
                    Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},兑换1金币精力");
                }
                Thread.Sleep(new Random().Next(500, 1000));
                Common.clickButton(mainWindow, "点击兑换1福袋（800金币）");
                Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},兑换1金币福袋");
                Thread.Sleep(new Random().Next(500, 1000));
            }
        }

        // 购买仙币兑换
        public void buyXianBiExchange(AutomationElement mainWindow)
        {
            if (DailySet.luckyCount == "是" || DailySet.happyBag == "是")
            {
                Common.changeTab(mainWindow, "兑换", 0);
                Common.changeTab(mainWindow, "仙币兑换", 1);
                for (int i = 0; i < 10; i++)
                {
                    if (i < 5 && DailySet.luckyCount == "是")
                    {
                        Common.clickButton(mainWindow, "兑换10幸运点（1600仙币）");
                        Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},兑换10幸运点");
                        Thread.Sleep(new Random().Next(500, 1000));
                    }
                    if (DailySet.happyBag == "是")
                    {
                        Common.clickButton(mainWindow, "点击兑换1福袋（400仙币）");
                        Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},兑换1仙币福袋");
                        Thread.Sleep(new Random().Next(500, 1000));
                    }
                }
            }
        }

        // 每日兑换
        public void dailyExchange(AutomationElement mainWindow)
        {
            // 购买每日兑换，仅周一至周五进行每日兑换
            // 周一：3W仙币兑换200幸运点
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && DailySet.monday)
            {
                Common.changeTab(mainWindow, "兑换", 0);
                Common.changeTab(mainWindow, "每日兑换", 1);
                Common.clickButton(mainWindow, "周一：3W仙币兑换200幸运点");
                Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},兑换周一每日兑换");
                Thread.Sleep(new Random().Next(500, 1000));
            }
            // 周二：3W仙币兑换100福袋
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && DailySet.tuesday)
            {
                Common.changeTab(mainWindow, "兑换", 0);
                Common.changeTab(mainWindow, "每日兑换", 1);
                Common.clickButton(mainWindow, "周二：3W仙币兑换100福袋");
                Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},兑换周二每日兑换");
                Thread.Sleep(new Random().Next(500, 1000));
            }
            // 周三：3W仙币兑换1000修为
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && DailySet.wednesday)
            {
                Common.changeTab(mainWindow, "兑换", 0);
                Common.changeTab(mainWindow, "每日兑换", 1);
                Common.clickButton(mainWindow, "周三：3W仙币兑换1000修为");
                Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},兑换周三每日兑换");
                Thread.Sleep(new Random().Next(500, 1000));
            }
            // 周四：3W仙币兑换1个随机灵宝碎片
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && DailySet.thursday)
            {
                Common.changeTab(mainWindow, "兑换", 0);
                Common.changeTab(mainWindow, "每日兑换", 1);
                Common.clickButton(mainWindow, "周四：3W仙币兑换1个随机灵宝碎片");
                Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},兑换周四每日兑换");
                Thread.Sleep(new Random().Next(500, 1000));
            }
            // 周五：3W仙币兑换兑20宝箱积分
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday && DailySet.friday)
            {
                Common.changeTab(mainWindow, "兑换", 0);
                Common.changeTab(mainWindow, "每日兑换", 1);
                Common.clickButton(mainWindow, "周五：3W仙币兑换兑20宝箱积分");
                Common.addMessage(_form.dailyMessage, $"{DateTime.Now.ToString()},兑换周五每日兑换");
                Thread.Sleep(new Random().Next(500, 1000));
            }
        }

        // 发红包福包
        public void sendRedBag(AutomationElement mainWindow)
        {
            Common.changeTab(mainWindow, "设置", 0);
            Common.clickButton(mainWindow, "发红包/福包");
            AutomationElement refBag = null;
            elapsed = 0;
            while (elapsed < timeoutMs)
            {
                refBag = Common.getWindow("发红包 / 福包");

                if (refBag != null)
                    break;

                Thread.Sleep(interval);
                elapsed += interval;
            }
            if (refBag != null)
            {
                Common.clickButton(refBag, "自动发放所有红包");
                Thread.Sleep(new Random().Next(500, 1000));
                Common.clickButton(refBag, "自动发放所有福包");
                Common.clickButtonById(refBag, "Close");
                Thread.Sleep(new Random().Next(500, 1000));
            }
        }

        public void stop()
        {
            if (_cts != null)
            {
                _cts?.Cancel();
                _cts = null;
                Common.addMessage(_form.dailyMessage, "自动签到已停止");
            }
        }
    }
}
