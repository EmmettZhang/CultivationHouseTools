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
using CultivationHouseTools.lib;

namespace CultivationHouseTools.actions
{

    internal class AutoUnknown
    {
        private MainWindow _form;
        private CancellationTokenSource _shopTokenSource;
        private int _cursor = 0;
        private Random _rnd = new Random();
        private List<int> _order;
        private int timeoutMs = 10000;
        private int interval = 100;
        private int elapsed = 0;

        public AutoUnknown(MainWindow form)
        {
            _form = form;
        }

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);


        public void run()
        {
            string unknownIndex = _form.unknownIndex.Text.Trim();
            // 如果不是1-10则报异常
            if (unknownIndex == null)
            {
                Common.addMessage(_form.message, "请输入盲盒序号");
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

            Common.clickButton(window, "最大化");
            int index = 0;
            switch (_form.unknownIndex.Text.Trim())
            {
                case "1":
                    Common.clickButton(window, "切换盲盒1");
                    index = 0;
                    break;
                case "2":
                    Common.clickButton(window, "切换盲盒2");
                    index = 1;
                    break;
                case "3":
                    Common.clickButton(window, "切换盲盒3");
                    index = 2;
                    break;
                case "4":
                    Common.clickButton(window, "切换盲盒4");
                    index = 3;
                    break;
                case "5":
                    Common.clickButton(window, "切换盲盒5");
                    index = 4;
                    break;
                case "6":
                    Common.clickButton(window, "切换盲盒6");
                    index = 5;
                    break;
                case "7":
                    Common.clickButton(window, "切换盲盒7");
                    index = 6;
                    break;
                case "8":
                    Common.clickButton(window, "切换盲盒8");
                    index = 7;
                    break;
                case "9":
                    Common.clickButton(window, "切换盲盒9");
                    index = 8;
                    break;
                case "10":
                    Common.clickButton(window, "切换盲盒10");
                    index = 9;
                    break;
                default:
                    Common.addMessage(_form.message, "请输入有效盲盒序号");
                    return;
            }

            var scrollViewer = window.FindAll(
                    TreeScope.Descendants,
                    new PropertyCondition(
                        AutomationElement.ClassNameProperty,
                        "ScrollViewer"))[index];

            _order = Enumerable.Range(1, 400).OrderBy(x => _rnd.Next()).ToList();
            _cursor = 0;

            string s = _form.unknownNum.Text.Trim();
            if (int.TryParse(s, out int num))
            {
                for (int i = 0; i < num; i++)
                {

                    TryGetNext(out int key);

                    Point p = ClickCell(scrollViewer, key);

                    SetCursorPos((int)p.X, (int)p.Y);

                    Thread.Sleep(750);

                    WinApi.LeftClick();

                    Common.addMessage(_form.message, $"点击第{key}个位置，X:{p.X},Y:{p.Y}");
                }
            }
            else
            {
                Common.addMessage(_form.message, "盲盒次数请输入有效的整数");
            }

            //你没有钥匙，无法开启盲盒
        }

        public bool TryGetNext(out int value)
        {
            while (_cursor < _order.Count)
            {
                int v = _order[_cursor++];

                if (UnknownLib.clickedSet.Add(getUnknownIndex() + v))
                {
                    value = v;
                    return true;
                }
            }

            value = -1;
            return false;
        }

        private string getUnknownIndex()
        {
            switch (_form.unknownIndex.Text.Trim())
            {
                case "1":
                    return "A";
                case "2": 
                    return "B";
                case "3":
                    return "C";
                case "4":
                    return "D";
                case "5":
                    return "E";
                case "6":
                    return "F";
                case "7":
                    return "G";
                case "8":
                    return "H";
                case "9":
                    return "I";
                case "10":
                    return "J";
                default:
                    return null;
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
