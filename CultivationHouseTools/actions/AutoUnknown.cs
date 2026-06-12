using CultivationHouseTools.lib;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace CultivationHouseTools.actions
{

    internal class AutoUnknown
    {
        private MainWindow _form;
        private CancellationTokenSource _tokenSource;
        private int timeoutMs = 10000;
        private int interval = 100;
        private int elapsed = 0;
        private string _unknownIndex;
        private int _index;
        private string _front;
        private AutomationElement _scrollViewer;
        private AutomationElement _notice;
        private static Random _rnd = new Random();

        private int _switchCount = 0;
        private const int MaxSwitchCount = 5;

        public AutoUnknown(MainWindow form)
        {
            _form = form;
        }

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        public async void run()
        {
            Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，开始执行盲盒");
            if (_tokenSource != null)
            {
                Common.addMessage(_form.message, "当前无法开始盲盒，请先结束盲盒");
                return;
            }

            AutomationElement exit = Common.getWindow("心愿盲盒");
            if (exit != null)
            {
                // 关闭幸运商店窗口，以备重新打开重置状态
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
            Common.clickButton(mainWindow, "心愿盲盒");
            AutomationElement window = null;
            elapsed = 0;
            while (elapsed < timeoutMs)
            {
                window = Common.getWindow("心愿盲盒");

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

            _unknownIndex = _form.unknownIndex.Text.Trim();

            Common.clickButton(window, "最大化");
            getUnknownIndex(window);
            if (_front == "ERROR")
            {
                Common.addMessage(_form.message, "请输入有效盲盒序号");
                return;
            }

            AutomationElementCollection automationElementCollection = window.FindAll(
                    TreeScope.Descendants,
                    new PropertyCondition(
                        AutomationElement.ClassNameProperty,
                        "ScrollViewer"));

            _scrollViewer = automationElementCollection[_index];
            _switchCount = 0;

            string s = _form.unknownNum.Text.Trim();
            if (int.TryParse(s, out int num))
            {
                _tokenSource = new CancellationTokenSource();
                int count = 0;
                _notice = Common.getElById(window, "TiShiLabel");
                bool flag = moveToNextBox(false, window, automationElementCollection);
                if (flag)
                {
                    return;
                }
                long currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                await Task.Run(() =>
                {
                    while (_tokenSource != null && !_tokenSource.Token.IsCancellationRequested)
                    {
                        bool clicked = autoOpenUnknown();
                        if (!clicked) {
                            stop("已无位置可开，停止");
                            return;
                        }
                        if (_notice != null)
                        {
                            if (_notice.Current.Name.IndexOf("剩：0") > 0)
                            {
                                stop("你的钥匙不足, 停止");
                                return;
                            }
                            else if (_notice.Current.Name.IndexOf("该盲盒剩特殊物品：0") > 0)
                            {
                                // 没有特殊物品，切换盲盒，1-5均无特殊物品后停止
                                flag = moveToNextBox(true, window, automationElementCollection);
                                if (flag)
                                {
                                    return;
                                }
                                currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                            }
                            else if (_notice.Current.Name == "你没有钥匙，无法开启盲盒（钥匙通过设置 - 发红包下面 - 发灵包获得）")
                            {
                                stop("你的钥匙不足, 停止");
                                return;
                            }
                        }

                        count++;

                        if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - currentTime >= 60000)
                        {
                            flag = refreshUnknown(window, automationElementCollection);
                            if (flag) {
                                return;
                            }
                            currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        }

                        if (count >= num)
                        {
                            stop($"已完成{num}次盲盒, 停止");
                            return;
                        }
                    }
                },
                _tokenSource.Token
                );
            }
            else
            {
                Common.addMessage(_form.message, "盲盒次数请输入有效的整数");
            }
        }

        public bool refreshUnknown(AutomationElement window, AutomationElementCollection automationElementCollection)
        {
            Common.clickButton(window, "刷新当前盲盒数据");
            Thread.Sleep(1000);
            // 检查是否还有特殊物品
            if (_notice.Current.Name.IndexOf("该盲盒剩余特殊物品：0") > 0)
            {
                // 没有特殊物品，切换盲盒
                return moveToNextBox(true, window, automationElementCollection);
            }
            else
            {
                // 排除已开位置
                removeOpened();
            }
            return false;
        }

        public void stop(string msg)
        {
            _tokenSource?.Cancel();
            _tokenSource = null;
            Common.addMessage(_form.message, msg);
        }

        public void removeOpened()
        {
            AutomationElementCollection allLabel = _scrollViewer.FindAll(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ClassNameProperty, "TextBlock"));

            for (int i = 0; i < allLabel.Count; i++)
            {
                if (allLabel[i].Current.Name != null && allLabel[i].Current.Name != "")
                {
                    UnknownLib.RemainMap[_index].Remove(i);
                }
            }
        }

        private bool moveToNextBox(bool getNext, AutomationElement window, AutomationElementCollection automationElementCollection)
        {
            if (getNext)
            {
                int.TryParse(_unknownIndex, out int unknownIndex);
                int nextUnknownIndex = GetNextBox(unknownIndex);

                _unknownIndex = nextUnknownIndex.ToString();
                getUnknownIndex(window);
                _scrollViewer = automationElementCollection[_index];

                _switchCount++;

                if (_switchCount >= MaxSwitchCount)
                {
                    string mes = nextUnknownIndex >= 1 && nextUnknownIndex <= 5 ? "1-5" : "6-10";
                    stop($"盲盒{mes}均已没有特殊物品，停止");
                    return true;
                }
            }
            if ((getNext && _unknownIndex != _form.unknownIndex.Text) || !getNext) {
                return refreshUnknown(window, automationElementCollection);
            }
            return false;
        }

        private int GetNextBox(int current)
        {
            var group = GetGroup(current);

            int next = current + 1;

            if (next > group.End)
                next = group.Start;

            return next;
        }

        private (int Start, int End) GetGroup(int boxNum)
        {
            if (boxNum <= 5)
                return (1, 5);

            return (6, 10);
        }

        private void getUnknownIndex(AutomationElement window)
        {
            switch (_unknownIndex)
            {
                case "1":
                    Common.clickButton(window, "切换盲盒1");
                    Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，切换盲盒1");
                    _front = "A";
                    _index = 0;
                    break;
                case "2":
                    Common.clickButton(window, "切换盲盒2");
                    Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，切换盲盒2");
                    _front = "B";
                    _index = 1;
                    break;
                case "3":
                    Common.clickButton(window, "切换盲盒3");
                    Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，切换盲盒3");
                    _front = "C";
                    _index = 2;
                    break;
                case "4":
                    Common.clickButton(window, "切换盲盒4");
                    Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，切换盲盒4");
                    _front = "D";
                    _index = 3;
                    break;
                case "5":
                    Common.clickButton(window, "切换盲盒5");
                    Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，切换盲盒5");
                    _front = "E";
                    _index = 4;
                    break;
                case "6":
                    Common.clickButton(window, "切换盲盒6");
                    Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，切换盲盒6");
                    _front = "F";
                    _index = 5;
                    break;
                case "7":
                    Common.clickButton(window, "切换盲盒7");
                    Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，切换盲盒7");
                    _front = "G";
                    _index = 6;
                    break;
                case "8":
                    Common.clickButton(window, "切换盲盒8");
                    Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，切换盲盒8");
                    _front = "H";
                    _index = 7;
                    break;
                case "9":
                    Common.clickButton(window, "切换盲盒9");
                    Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，切换盲盒9");
                    _front = "I";
                    _index = 8;
                    break;
                case "10":
                    Common.clickButton(window, "切换盲盒10");
                    Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，切换盲盒10");
                    _front = "J";
                    _index = 9;
                    break;
                default:
                    _front = "ERROR";
                    _index = -1;
                    break;
            }
        }


        public bool autoOpenUnknown()
        {
            if (TryGetNext(out int key))
            {
                Point p = ClickCell(key);

                SetCursorPos((int)p.X, (int)p.Y);
                Thread.Sleep(300);

                WinApi.LeftClick();
                Thread.Sleep(300);

                Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，点击盲盒{_unknownIndex}第{key}个位置，{_notice.Current.Name}");
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryGetNext(out int value)
        {
            var list = UnknownLib.RemainMap[_index];

            if (list.Count == 0)
            {
                value = -1;
                return false;
            }

            int last = list.Count - 1;

            value = list[last];

            list.RemoveAt(last);

            return true;
        }

        private const int Rows = 20;
        private const int Cols = 20;

        private const int CellW = 95;
        private const int CellH = 70;

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
