using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CultivationHouseTools.actions
{
    internal class AutoSignIn
    {
        private CancellationTokenSource _cts = null;
        private Task _task;

        private MainWindow _form;

        public AutoSignIn(MainWindow form)
        {
            _form = form;
        }

        public void run()
        {
            if (_cts != null)
            {
                Common.addMessage(_form.dailyMessage, "自动签到已开始，请先停止");
            }




            Common.addMessage(_form.dailyMessage, "自动签到已开始");
        }

        public void stop()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
                Common.addMessage(_form.dailyMessage, "自动签到已停止");
            }
        }
    }
}
