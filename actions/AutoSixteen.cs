using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace CultivationHouseTools.actions
{
    internal class AutoSixteen
    {
        private CancellationTokenSource _cts;
        private Task _task;

        private MainWindow _form;
        private static List<TimeSpan> times = new List<TimeSpan>() { new TimeSpan(16, 0, 0) };

        public AutoSixteen(MainWindow form)
        {
            _form = form;
        }

        public void run()
        {
            if (_cts != null)
            {
                Common.addMessage(_form.dailyMessage, "自动Boss已开始，请先停止");
                return;
            }

            _cts = new CancellationTokenSource();

            _task = Task.Run(() => RunSchedule(_cts.Token));

            Common.addMessage(_form.dailyMessage, "自动Boss已开始");
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

                Common.addMessage(_form.dailyMessage, "自动Boss下次执行：" + next.Value);

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
            AutomationElement mainWindow = Common.getWindow(_form.title.Text.Trim());
            if (mainWindow != null)
            {
                // 自动获取boss结果
                Common.changeTab(mainWindow, "BOSS", 0);
                Common.clickButton(mainWindow, "获取结果");

                // 如果是周五，自动获取门派分成
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                    Common.changeTab(mainWindow, "门派", 0);
                    Common.changeTab(mainWindow, "分 成", 1);
                    Common.clickButton(mainWindow, "查看本周分成");
                    Thread.Sleep(500);
                    Common.clickButton(mainWindow, "领取我的本周分成");
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
                _cts.Cancel();
                _cts = null;
                Common.addMessage(_form.dailyMessage, "自动Boss已停止");
            }
        }
    }
}
