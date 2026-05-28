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
using System.Windows.Forms;
using System.Xml.Linq;
using System;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CultivationHouseTools.actions
{
    internal class AutoUnknown
    {
        private const int TotalRows = 20;
        private const int TotalCols = 20;

        private const int CellWidth = 95;
        private const int CellHeight = 70;

        private const int GridWidth = TotalCols * CellWidth;     //1900
        private const int GridHeight = TotalRows * CellHeight;   //1400

        private MainWindow _form;
        private CancellationTokenSource _shopTokenSource;

        public AutoUnknown(MainWindow form)
        {
            _form = form;
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        public void run()
        {
            AutomationElement exit = Common.getWindow("心愿盲盒");
            if (exit != null)
            {
                // 关闭幸运商店窗口，以备重新打开重置状态
                Common.clickButtonById(exit, "Close");
                Thread.Sleep(1000);
            }

            AutomationElement mainWindow = Common.getWindow(_form.title.Text.Trim());
            Common.clickButton(mainWindow, "心愿盲盒");
            Thread.Sleep(1000);

            AutomationElement window = Common.getWindow("心愿盲盒");
            Common.clickButton(window, "最大化");
            Thread.Sleep(1000);


            var scrollViewer = window.FindFirst(
                    TreeScope.Descendants,
                    new PropertyCondition(
                        AutomationElement.ClassNameProperty,
                        "ScrollViewer"));

            for (int i = 0; i < 10; i++)
            {
                Random rnd = new Random();

                int key = rnd.Next(1, 401);
                Common.addMessage(_form.message, key.ToString());

                ScrollToCell(scrollViewer, key);
                var p = GetCellCenter(scrollViewer, key);

                SetCursorPos((int)p.X, (int)p.Y);

                Thread.Sleep(700);

                WinApi.LeftClick();

                Common.addMessage(_form.message, $"点击X:{p.X},Y{p.Y}");
            }
        }

        public static void ScrollToCell(AutomationElement scrollViewer, int key)
        {
            var sp = (ScrollPattern)scrollViewer.GetCurrentPattern(ScrollPattern.Pattern);

            int row = (key - 1) / 20;
            int col = (key - 1) % 20;

            double visibleRows = 20.0 * sp.Current.VerticalViewSize / 100.0;

            double visibleCols = 20.0 * sp.Current.HorizontalViewSize / 100.0;

            double firstRow = Math.Max(0, Math.Min(row - visibleRows / 2, 20 - visibleRows));

            double firstCol = Math.Max(0, Math.Min(col - visibleCols / 2, 20 - visibleCols));

            double vPercent = (20 - visibleRows) <= 0 ? 0 : firstRow / (20 - visibleRows) * 100;

            double hPercent = (20 - visibleCols) <= 0 ? 0 : firstCol / (20 - visibleCols) * 100;

            sp.SetScrollPercent(hPercent, vPercent);
        }

        public static Point GetCellCenter(AutomationElement scrollViewer, int key)
        {
            if (key < 1 || key > 400)
                throw new ArgumentOutOfRangeException(nameof(key));

            var sp = (ScrollPattern)scrollViewer.GetCurrentPattern(ScrollPattern.Pattern);

            var rect = scrollViewer.Current.BoundingRectangle;

            //-----------------------------------
            // 当前可视区域尺寸
            //-----------------------------------

            double visibleWidth = GridWidth * sp.Current.HorizontalViewSize / 100.0;

            double visibleHeight = GridHeight * sp.Current.VerticalViewSize / 100.0;

            //-----------------------------------
            // 当前滚动偏移
            //-----------------------------------

            double maxOffsetX = Math.Max(0, GridWidth - visibleWidth);

            double maxOffsetY = Math.Max(0, GridHeight - visibleHeight);

            double offsetX = maxOffsetX * sp.Current.HorizontalScrollPercent / 100.0;

            double offsetY = maxOffsetY * sp.Current.VerticalScrollPercent / 100.0;

            //-----------------------------------
            // 格子行列
            //-----------------------------------

            int row = (key - 1) / 20;
            int col = (key - 1) % 20;

            //-----------------------------------
            // 格子中心在Grid中的坐标
            //-----------------------------------

            double gridX = col * CellWidth + CellWidth / 2.0;

            double gridY = row * CellHeight + CellHeight / 2.0;

            //-----------------------------------
            // 转换为屏幕坐标
            //-----------------------------------

            double screenX = rect.Left + (gridX - offsetX);

            double screenY = rect.Top + (gridY - offsetY);

            return new Point((int)Math.Round(screenX), (int)Math.Round(screenY));
        }

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(
             int X,
            int Y
        );
    }
}
