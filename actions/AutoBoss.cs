using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CultivationHouseTools.actions
{
    internal class AutoBoss
    {
        private CancellationTokenSource _cts;
        private Task _task;

        private MainWindow _form;

        public AutoBoss(MainWindow form)
        {
            _form = form;
        }

        public void run()
        {
            if (_cts != null)
            {
                Common.addMessage(_form.dailyMessage, "自动Boss已开始，请先停止");
            }
            Common.addMessage(_form.dailyMessage, "自动Boss已开始");
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
