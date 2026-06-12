using CultivationHouseTool.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Windows.Interop;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CultivationHouseTool
{
    internal class AutoFlip
    {
        private MainWindow _form;
        private CancellationTokenSource _tokenSource;
        private static Random _rnd = new Random();
        private int timeoutMs = 10000;
        private int interval = 100;
        private int elapsed = 0;
        private AutomationElement _scrollViewer;
        private AutomationElement _notice;
        private List<int> _order;
        public AutoFlip(MainWindow form)
        {
            _form = form;
        }

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        public static List<int> shuffle()
        {
            List<int> list = Enumerable.Range(0, 25).ToList();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rnd.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }

            return list;
        }

        public async void run()
        {
            if (_tokenSource != null)
            {
                Common.addMessage(_form.message, "当前无法开始翻卡，请先结束翻卡");
                return;
            }
            Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，开始执行翻卡");

            string s = _form.flipNum.Text.Trim();
            if (int.TryParse(s, out int num))
            {
                string v = _form.flipMaxCount.Text.Trim();
                if (int.TryParse(v, out int maxCount) && maxCount < 25)
                {
                    AutomationElement exit = Common.getWindow("心悦翻卡");
                    if (exit != null)
                    {
                        Common.clickButtonById(exit, "Close");
                    }

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
                    Common.clickButton(mainWindow, "心悦卡牌");
                    AutomationElement window = null;
                    elapsed = 0;
                    while (elapsed < timeoutMs)
                    {
                        window = Common.getWindow("心悦翻卡");

                        if (window != null)
                            break;

                        Thread.Sleep(interval);
                        elapsed += interval;
                    }

                    if (window == null)
                    {
                        Common.addMessage(_form.message, "窗口未启动");
                        return;
                    }

                    _scrollViewer = window.FindFirst(
                            TreeScope.Descendants,
                            new PropertyCondition(
                                AutomationElement.ClassNameProperty,
                                "ScrollViewer"));

                    _tokenSource = new CancellationTokenSource();
                    int count = 0;
                    _notice = Common.getElById(window, "TiShiLabel");

                    await Task.Run(() =>
                    {
                        while (_tokenSource != null && !_tokenSource.Token.IsCancellationRequested)
                        {
                            Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，第{count + 1}轮翻卡");
                            _order = shuffle();
                            Common.clickButton(window, "开启新卡池");
                            Thread.Sleep(300);

                            if (_notice.Current.Name.IndexOf("仙币不足") != -1)
                            {
                                stop($"你的仙币不足，无法开启，结束翻卡");
                                return;
                            } else if (_notice.Current.Name.IndexOf("次数不足") != -1)
                            {
                                stop($"你的开启次数不足，无法开启，结束翻卡");
                                return;
                            }

                            for (int i = 0; i < maxCount; i++)
                            {
                                int clicked = autoOpenFlip();
                                if (clicked == 0)
                                {
                                    stopFlip(window, $"翻到炸弹，结束当前卡池，本轮共翻卡 {i + 1} 次");
                                    break;
                                }
                                else if (clicked == 2)
                                {
                                    stopFlip(window, $"翻到称号，结束当前卡池，本轮共翻卡 {i + 1} 次");
                                    break;
                                }
                                else if (clicked == 3)
                                {
                                    stopFlip(window, $"翻到头像，结束当前卡池，本轮共翻卡 {i + 1} 次");
                                    break;
                                }
                                else if (clicked == 4)
                                {
                                    stopFlip(window, $"翻到背景，结束当前卡池，本轮共翻卡 {i + 1} 次");
                                    break;
                                }
                                if (i == maxCount - 1) {
                                    stopFlip(window, $"已达到最大翻卡数，翻卡结束，本轮共翻卡 {maxCount} 次");
                                }
                            }
                            Thread.Sleep(1000);
                            count++;
                            if (count >= num)
                            {
                                stop($"已完成{num}轮翻卡, 停止");
                            }
                        }
                    },
                    _tokenSource.Token
                    );
                }
                else
                {
                    Common.addMessage(_form.message, "输入的最大翻卡数不合法，请输入一个小于25的整数");
                    return;
                }
            }
            else
            {
                Common.addMessage(_form.message, "输入的翻卡次数不合法，请输入一个整数");
                return;
            }
            
        }

        public void stopFlip(AutomationElement window, string msg)
        {
            Common.addMessage(_form.message, msg);
            Common.clickButton(window, "结束当前卡池");
            Thread.Sleep(300);
            Common.clickButtonById(window, "6");
        }

        public void stop(string msg)
        {
            _tokenSource?.Cancel();
            _tokenSource = null;
            Common.addMessage(_form.message, msg);
        }


        public int autoOpenFlip()
        {
            if (TryGetNext(out int key))
            {
                Point p = ClickCell(key);

                SetCursorPos((int)p.X, (int)p.Y);
                Thread.Sleep(300);

                WinApi.LeftClick();
                WinApi.LeftClick();

                AutomationElementCollection allLabel = _scrollViewer.FindAll(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ClassNameProperty, "TextBlock"));
                
                Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，点击第{key + 1}个位置，{_notice.Current.Name}");

                if (_notice.Current.Name.IndexOf("炸弹") != -1)
                {
                    return 0;
                }
                else if (_notice.Current.Name.IndexOf("称号") != -1)
                {
                    return 2;
                }
                else if (_notice.Current.Name.IndexOf("头像") != -1)
                {
                    return 3;
                }
                else if (_notice.Current.Name.IndexOf("背景") != -1)
                {
                    return 4;
                }


                return 1;
            }
            else
            {
                return 0;
            }
        }

        public bool TryGetNext(out int value)
        {
            if (_order.Count == 0)
            {
                value = -1;
                return false;
            }

            int last = _order.Count - 1;

            value = _order[last];

            _order.RemoveAt(last);

            return true;
        }

        private const int Rows = 5;
        private const int Cols = 5;

        private const int CellW = 145;
        private const int CellH = 120;

        private const int TotalW = Cols * CellW;   //1900
        private const int TotalH = Rows * CellH;   //1400

        //==================================================
        // 主入口：点击某个格子
        //==================================================
        public Point ClickCell(int key)
        {
            var sp = GetScrollPattern(_scrollViewer);

            int row = key / Cols;
            int col = key % Cols;

            // 1. 先确保滚动到目标附近
            ScrollToCell(sp, row, col);

            // 2. 等待UI刷新
            Thread.Sleep(200);

            // 3. 获取最新滚动状态
            var info = GetScrollInfo(sp);

            // 4. 计算当前可见区域左上角
            double offsetX = info.OffsetX;
            double offsetY = info.OffsetY;

            // 5. 计算目标中心点（在Grid中的绝对位置）
            double gridX = col * CellW + CellW / 2.0;
            double gridY = row * CellH + CellH / 2.0;

            // 6. 转换为屏幕坐标
            var rect = _scrollViewer.Current.BoundingRectangle;

            int x = (int)(rect.Left + (gridX - offsetX));
            int y = (int)(rect.Top + (gridY - offsetY));

            return new Point(x, y);
        }

        //==================================================
        // 滚动到目标格子附近
        //==================================================
        private void ScrollToCell(ScrollPattern sp, int row, int col)
        {
            var info = GetScrollInfo(sp);

            if (info.CanScrollH)
            {
                double targetCol = Math.Max(0, Math.Min(col - info.VisibleCols / 2, Cols - info.VisibleCols));

                double hPercent = (Cols - info.VisibleCols) <= 0 ? 0 : targetCol / (Cols - info.VisibleCols) * 100;

                SetSafe(sp, hPercent, null);
            }

            if (info.CanScrollV)
            {
                double targetRow = Math.Max(0, Math.Min(row - info.VisibleRows / 2, Rows - info.VisibleRows));

                double vPercent = (Rows - info.VisibleRows) <= 0 ? 0 : targetRow / (Rows - info.VisibleRows) * 100;

                SetSafe(sp, null, vPercent);
            }
        }

        //==================================================
        // 安全滚动（避免“无法接收焦点”）
        //==================================================
        private void SetSafe(ScrollPattern sp, double? h, double? v)
        {
            try
            {
                sp.SetScrollPercent(
                    h ?? sp.Current.HorizontalScrollPercent,
                    v ?? sp.Current.VerticalScrollPercent);
            }
            catch
            {
                // fallback：鼠标滚轮
            }
        }

        //==================================================
        // 获取ScrollPattern
        //==================================================
        private ScrollPattern GetScrollPattern(AutomationElement el)
        {
            return (ScrollPattern)el.GetCurrentPattern(ScrollPattern.Pattern);
        }

        //==================================================
        // 获取滚动信息
        //==================================================
        private ScrollInfo GetScrollInfo(ScrollPattern sp)
        {
            double visibleCols = Cols * sp.Current.HorizontalViewSize / 100.0;

            double visibleRows = Rows * sp.Current.VerticalViewSize / 100.0;

            double maxX = Math.Max(0, Cols - visibleCols);
            double maxY = Math.Max(0, Rows - visibleRows);

            double offsetX = maxX * sp.Current.HorizontalScrollPercent / 100.0;

            double offsetY = maxY * sp.Current.VerticalScrollPercent / 100.0;

            return new ScrollInfo
            {
                CanScrollH = sp.Current.HorizontallyScrollable,
                CanScrollV = sp.Current.VerticallyScrollable,
                VisibleCols = visibleCols,
                VisibleRows = visibleRows,
                OffsetX = offsetX * CellW,
                OffsetY = offsetY * CellH
            };
        }

        //==================================================
        // 数据结构
        //==================================================
        private class ScrollInfo
        {
            public bool CanScrollH;
            public bool CanScrollV;

            public double VisibleCols;
            public double VisibleRows;

            public double OffsetX;
            public double OffsetY;
        }
    }
}
