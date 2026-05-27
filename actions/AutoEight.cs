using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using CultivationHouseTools.lib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CultivationHouseTools.actions
{
    internal class AutoEight
    {
        private CancellationTokenSource _cts = null;
        private Task _task;

        private MainWindow _form;
        private static List<TimeSpan> times = new List<TimeSpan>() { new TimeSpan(8, 0, 0) };

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

                foreach (var t in times)
                {
                    DateTime dt = now.Date.Add(t);

                    if (dt > now)
                    {
                        next = dt;
                        break;
                    }
                }

                if (next == null)
                {
                    next = now.Date.AddDays(1).Add(times[0]);
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
         * 每日八点自动签到、葫芦签到、播撒灵露、门派演武、报名boss、购买金币精力和金币福袋、购买仙币幸运点和福袋、购买每日兑换。
         * 每日八点十分、十三点十分自动收割并播种门派后山。
         * 每日十六点自动获取boss结果。
         * 每周一八点自动收获葫芦。
         * 
         */
        public void DoWork()
        {
            AutomationElement mainWindow = Common.getWindow(_form.title.Text.Trim());
            if (mainWindow != null)
            {
                // 签到弹窗
                Common.clickButton(mainWindow, "签到");
                Thread.Sleep(500); // 等待弹窗打开
                AutomationElement signWindow = Common.getWindow("签到");
                Common.clickButton(signWindow, "点击签到");
                Common.clickButton(signWindow, "每日签到福利");
                Common.clickButtonById(signWindow, "Close");
                Thread.Sleep(500); // 等待弹窗打开

                // 葫芦
                Common.clickButton(mainWindow, "仙玉商店");
                Thread.Sleep(500); // 等待弹窗打开
                AutomationElement jadeWindow = Common.getWindow("仙玉商店");
                Common.clickButton(jadeWindow, "宝葫芦玩法");
                Thread.Sleep(500); // 等待弹窗打开
                AutomationElement huluWindow = Common.getWindow("宝葫芦");
                Common.clickButton(huluWindow, "每日签到");
                // 如果是周一
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    Common.clickButton(huluWindow, "收获葫芦");
                }
                Common.clickButton(huluWindow, "播撒全部灵露");
                Common.clickButtonById(huluWindow, "Close");
                Common.clickButtonById(jadeWindow, "Close");
                Thread.Sleep(500);

                // 门派演武
                Common.changeTab(mainWindow, "门派", 0);
                Common.changeTab(mainWindow, "建 筑", 1);
                Common.clickButton(mainWindow, "刷新数据");
                Thread.Sleep(500);
                Common.clickButton(mainWindow, "演武");
                Thread.Sleep(500);

                // 报名Boss
                Common.changeTab(mainWindow, "BOSS", 0);
                int bossIndex = Dics.bossMap[DailySet.boss];
                Common.clickButton(mainWindow, "报名", bossIndex);
                Thread.Sleep(500);

                // 购买金币精力和金币福袋
                Common.changeTab(mainWindow, "兑换", 0);
                Common.changeTab(mainWindow, "金币兑换", 1);
                for (int i = 0; i < 10; i++)
                {
                    if (i < 3)
                    {
                        Common.clickButton(mainWindow, "点击兑换10精力（7000金币）");
                    }
                    if (i >= 3)
                    {
                        Common.clickButton(mainWindow, "点击兑换1精力（700金币）");
                    }
                    Common.clickButton(mainWindow, "点击兑换1福袋（800金币）");
                    Thread.Sleep(500);
                }

                // 购买仙币兑换
                if (DailySet.luckyCount == "是" || DailySet.happyBag == "是")
                {
                    Common.changeTab(mainWindow, "仙币兑换", 1);
                    for (int i = 0; i < 10; i++)
                    {
                        if (i < 5 && DailySet.luckyCount == "是")
                        {
                            Common.clickButton(mainWindow, "兑换10幸运点（1600仙币）");
                        }
                        if (DailySet.happyBag == "是")
                        {
                            Common.clickButton(mainWindow, "点击兑换1福袋（400仙币）");
                        }
                        Thread.Sleep(500);
                    }
                }

                // 购买每日兑换，仅周一至周五进行每日兑换
                // 周一：3W仙币兑换200幸运点
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && DailySet.monday)
                {
                    Common.changeTab(mainWindow, "每日兑换", 1);
                    Common.clickButton(mainWindow, "周一：3W仙币兑换200幸运点");
                }
                // 周二：3W仙币兑换100福袋
                if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && DailySet.tuesday)
                {
                    Common.changeTab(mainWindow, "每日兑换", 1);
                    Common.clickButton(mainWindow, "周二：3W仙币兑换100福袋");
                }
                // 周三：3W仙币兑换1000修为
                if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && DailySet.wednesday)
                {
                    Common.changeTab(mainWindow, "每日兑换", 1);
                    Common.clickButton(mainWindow, "周三：3W仙币兑换1000修为");
                }
                // 周四：3W仙币兑换1个随机灵宝碎片
                if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && DailySet.thursday)
                {
                    Common.changeTab(mainWindow, "每日兑换", 1);
                    Common.clickButton(mainWindow, "周四：3W仙币兑换1个随机灵宝碎片");
                }
                // 周五：3W仙币兑换兑20宝箱积分
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday && DailySet.friday)
                {
                    Common.changeTab(mainWindow, "每日兑换", 1);
                    Common.clickButton(mainWindow, "周五：3W仙币兑换20宝箱积分");
                }
            }
            else
            {
                Common.addMessage(_form.dailyMessage, "未找到修仙小屋窗口，请确保游戏正在运行并且窗口标题正确");
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
