using CultivationHouseTools.lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace CultivationHouseTools.actions
{
    internal class AutoTwelve
    {
        private CancellationTokenSource _cts;
        private Task _task;
        private Random _random = new Random();

        private MainWindow _form;
        private static List<TimeSpan> times = new List<TimeSpan>() { new TimeSpan(12, 0, 5) };

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
            AutomationElement mainWindow = Common.getWindow(_form.title.Text.Trim());
            if (mainWindow != null)
            {
                // 自动获取boss结果
                Common.changeTab(mainWindow, "BOSS", 0);
                Common.clickButton(mainWindow, "真·BOSS烛龙");

                AutomationElement bossWindow = Common.getWindow("真·世界BOSS");
                // 1-3秒随机偏移
                Thread.Sleep(new Random().Next(1000, 3000));

                if (bossWindow != null) {
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
                Common.addMessage(_form.dailyMessage, "自动真Boss已停止");
            }
        }
    }
}
