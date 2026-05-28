using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CultivationHouseTools.lib
{
    public static class UnknownLib
    {
        public static HashSet<string> clickedSet = new HashSet<string>();

        private static CancellationTokenSource _cts;
        private static Task _task;

        private static List<TimeSpan> times = new List<TimeSpan>() { new TimeSpan(9, 29, 00), new TimeSpan(15, 29, 00), new TimeSpan(21, 29, 00) };


        public static void run()
        {
            _cts = new CancellationTokenSource();

            _task = Task.Run(() => RunSchedule(_cts.Token));
        }

        private async static Task RunSchedule(CancellationToken token)
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

                await Task.Delay(wait, token);

                DoWork();
            }
        }

        public static void DoWork()
        {
            clickedSet.Clear();

        }
    }
}
