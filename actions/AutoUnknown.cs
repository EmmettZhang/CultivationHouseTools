using CultivationHouseTools.lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CultivationHouseTools.actions
{

    internal class AutoUnknown
    {
        private MainWindow _form;
        private CancellationTokenSource _tokenSource;
        private int _cursor = 0;
        private Random _rnd = new Random();
        private List<int> _order;
        private int timeoutMs = 10000;
        private int interval = 100;
        private int elapsed = 0;
        private string _unknownIndex;
        private int _index;
        private string _front;

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

            AutomationElement mainWindow = Common.getWindow(_form.title.Text.Trim());
            Common.clickButton(mainWindow, "心愿盲盒");
            AutomationElement window = null;
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

            //Common.clickButton(window, "最大化");
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

            AutomationElement scrollViewer = automationElementCollection[_index];

            string s = _form.shopNum.Text.Trim();
            if (int.TryParse(s, out int num))
            {
                _tokenSource = new CancellationTokenSource();
                int count = 0;
                AutomationElement noCount = null;

                await Task.Run(() =>
                {
                    while (_tokenSource != null && !_tokenSource.Token.IsCancellationRequested)
                    {

                        autoOpenUnknown(scrollViewer);
                        noCount = Common.getElById(window, "TiShiLabel");
                        if (noCount != null)
                        {

                            if (noCount.Current.Name.IndexOf("剩：0") > 0)
                            {
                                stop("你的钥匙不足, 停止");
                            }
                            else if (noCount != null && noCount.Current.Name.IndexOf("该盲盒剩特殊物品：0") > 0)
                            {
                                // 没有特殊物品，切换盲盒，1-5均无特殊物品后停止
                                moveToNextBox(window);
                            }
                            else if (noCount.Current.Name.IndexOf("你没有钥匙") > 0)
                            {
                                stop("你的钥匙不足, 停止");
                            }
                        }

                        count++;

                        if (count % 84 == 0)
                        {
                            Common.clickButton(window, "刷新当前盲盒数据");
                            noCount = Common.getElById(window, "TiShiLabel");
                            // 检查是否还有特殊物品
                            if (noCount != null && noCount.Current.Name.IndexOf("该盲盒剩余特殊物品：0") > 0)
                            {
                                // 没有特殊物品，切换盲盒
                                moveToNextBox(window);
                            }
                            // 排除已开位置
                        }


                        if (count >= num)
                        {
                            stop($"已完成{num}次盲盒, 停止");
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

        public void stop(string msg)
        {
            _tokenSource?.Cancel();
            _tokenSource = null;
            Common.addMessage(_form.message, msg);
        }

        private void moveToNextBox(AutomationElement window)
        {
            int.TryParse(_unknownIndex, out int unknownIndex);
            int nextUnknownIndex = GetNextBox(unknownIndex);

            _unknownIndex = nextUnknownIndex.ToString();
            getUnknownIndex(window);

            _switchCount++;

            if (_switchCount >= MaxSwitchCount)
            {
                string mes = nextUnknownIndex >= 1 && nextUnknownIndex <= 5 ? "1-5" : "6-10";
                stop($"盲盒{mes}均已没有特殊物品，停止");
            }
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


        public void autoOpenUnknown(AutomationElement scrollViewer)
        {
            TryGetNext(out int key);

            Point p = ClickCell(scrollViewer, key);

            SetCursorPos((int)p.X, (int)p.Y);

            Thread.Sleep(720);

            WinApi.LeftClick();

            Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，点击盲盒{_unknownIndex}第{key}个位置，X:{p.X},Y:{p.Y}");
        }

        public List<int> shuffle()
        {
            List<int> list = Enumerable.Range(1, 400).ToList();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rnd.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }

            return list;
        }

        public bool TryGetNext(out int value)
        {
            while (_cursor < _order.Count)
            {
                int v = _order[_cursor++];

                if (UnknownLib.clickedSet.Add(_front + v))
                {
                    value = v;
                    return true;
                }
            }

            value = -1;
            return false;
        }

        private bool getUnknownIndex(AutomationElement window)
        {
            _order = shuffle();
            _cursor = 0;
            switch (_unknownIndex)
            {
                case "1":
                    Common.clickButton(window, "切换盲盒1");
                    Common.addMessage(_form.message, "切换盲盒1");
                    _front = "A";
                    _index = 0;
                    return true;
                case "2":
                    Common.clickButton(window, "切换盲盒2");
                    Common.addMessage(_form.message, "切换盲盒2"); 
                    _front = "B";
                    _index = 1;
                    return true;
                case "3":
                    Common.clickButton(window, "切换盲盒3");
                    Common.addMessage(_form.message, "切换盲盒3");
                    _front = "C";
                    _index = 2;
                    return true;
                case "4":
                    Common.clickButton(window, "切换盲盒4");
                    Common.addMessage(_form.message, "切换盲盒4");
                    _front = "D";
                    _index = 3;
                    return true;
                case "5":
                    Common.clickButton(window, "切换盲盒5");
                    Common.addMessage(_form.message, "切换盲盒5");
                    _front = "E";
                    _index = 4;
                    return true;
                case "6":
                    Common.clickButton(window, "切换盲盒6");
                    Common.addMessage(_form.message, "切换盲盒6");
                    _front = "F";
                    _index = 5;
                    return true;
                case "7":
                    Common.clickButton(window, "切换盲盒7");
                    Common.addMessage(_form.message, "切换盲盒7");
                    _front = "G";
                    _index = 6;
                    return true;
                case "8":
                    Common.clickButton(window, "切换盲盒8");
                    Common.addMessage(_form.message, "切换盲盒8");
                    _front = "H";
                    _index = 7;
                    return true;
                case "9":
                    Common.clickButton(window, "切换盲盒9");
                    Common.addMessage(_form.message, "切换盲盒9");
                    _front = "I";
                    _index = 8;
                    return true;
                case "10":
                    Common.clickButton(window, "切换盲盒10");
                    Common.addMessage(_form.message, "切换盲盒10");
                    _front = "J";
                    _index = 9;
                    return true;
                default:
                    _front = "ERROR";
                    _index = -1;
                    return false;
            }
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
        public static Point ClickCell(AutomationElement scrollViewer,int key)
        {
            var sp = GetScrollPattern(scrollViewer);

            int row = (key - 1) / Cols;
            int col = (key - 1) % Cols;

            // 1. 先确保滚动到目标附近
            ScrollToCell(sp, row, col);

            // 2. 等待UI刷新
            System.Threading.Thread.Sleep(150);

            // 3. 获取最新滚动状态
            var info = GetScrollInfo(sp);

            // 4. 计算当前可见区域左上角
            double offsetX = info.OffsetX;
            double offsetY = info.OffsetY;

            // 5. 计算目标中心点（在Grid中的绝对位置）
            double gridX = col * CellW + CellW / 2.0;
            double gridY = row * CellH + CellH / 2.0;

            // 6. 转换为屏幕坐标
            var rect = scrollViewer.Current.BoundingRectangle;

            int x = (int)(rect.Left + (gridX - offsetX));
            int y = (int)(rect.Top + (gridY - offsetY));

            return new Point(x, y);
        }

        //==================================================
        // 滚动到目标格子附近
        //==================================================
        private static void ScrollToCell(ScrollPattern sp, int row, int col)
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
        private static void SetSafe(ScrollPattern sp, double? h, double? v)
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
        private static ScrollPattern GetScrollPattern(AutomationElement el)
        {
            return (ScrollPattern)el.GetCurrentPattern(ScrollPattern.Pattern);
        }

        //==================================================
        // 获取滚动信息
        //==================================================
        private static ScrollInfo GetScrollInfo(ScrollPattern sp)
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
