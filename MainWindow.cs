using CultivationHouseTools.actions;
using CultivationHouseTools.lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CultivationHouseTools
{
    public partial class MainWindow : Form
    {
        private CancellationTokenSource _tokenSource;
        private CancellationTokenSource _unknownTokenSource;

        private AutoEight _autoSignIn;
        private AutoHarvest _autoHarvest;
        private AutoSixteen _autoBoss;
        private AutoTwelve _autoTwelve;
        private AutoRefreshShop _autoRefreshShop;
        private AutoUnknown _autoUnknown;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DailySet.dailyStatus = "自动日常已停止";
            dailyStatus.Text = DailySet.dailyStatus;
            dailyStatus.ForeColor = Color.Red;

            DailySet.monday = false;
            DailySet.tuesday = false;
            DailySet.wednesday = false;
            DailySet.thursday = false;
            DailySet.friday = false;

            DailySet.boss = "饕餮";
            DailySet.luckyCount = "是";
            DailySet.happyBag = "否";
            DailySet.attackMethod = "物攻";
            Common.addMessage(dailyMessage, DailySet.print());

            _autoSignIn = new AutoEight(this);
            _autoHarvest = new AutoHarvest(this);
            _autoBoss = new AutoSixteen(this);
            _autoTwelve = new AutoTwelve(this);
            _autoRefreshShop = new AutoRefreshShop(this);
            _autoUnknown = new AutoUnknown(this);
        }

        private async void refresh_ClickAsync(object sender, EventArgs e)
        {
            _autoRefreshShop.run();
        }

        private void stopRefresh_Click(object sender, EventArgs e)
        {
            _autoRefreshShop.stop();
        }

        private async void flip_Click(object sender, EventArgs e)
        {
            Common.addMessage(message, "敬请期待！");
            return;

            if (_tokenSource != null)
            {
                Common.addMessage(message, "当前无法开始翻卡，请先结束其他操作");
                return;
            }

            string s = flipNum.Text.Trim();
            if (int.TryParse(s, out int num))
            {
                AutoFlip flip = new AutoFlip();
                flip.flipStart(num);
            }
            else {
                Common.addMessage(message, "翻卡次数请输入有效的整数");
            }
        }

        private void stopFlip_Click(object sender, EventArgs e)
        {
            Common.addMessage(message, "敬请期待！");
            return;

            _tokenSource?.Cancel();

            _tokenSource = null;

            Common.addMessage(message, "结束翻卡");
        }

        private async void unknownBox_Click(object sender, EventArgs e)
        {
            _autoUnknown.run();
        }

        private void stopUnknownBox_Click(object sender, EventArgs e)
        {

            Common.addMessage(message, "敬请期待！");
            return;

            _unknownTokenSource?.Cancel();

            _unknownTokenSource = null;

            Common.addMessage(message, "结束盲盒");
        }

        private void dailySet_Click(object sender, EventArgs e)
        {
            DailySetWindow dailySet = new DailySetWindow(this);
            dailySet.Show();
        }

        private void dayTask_Click(object sender, EventArgs e)
        {
            if (DailySet.dailyStatus == "自动日常已停止")
            {
                DailySet.dailyStatus = "自动日常已开始";
                dailyStatus.Text = DailySet.dailyStatus;
                dailyStatus.ForeColor = Color.Green;

                _autoSignIn.run();
                _autoHarvest.run();
                _autoBoss.run();
                _autoTwelve.run();
            }
            else if (DailySet.dailyStatus == "自动日常已开始")
            {
                DailySet.dailyStatus = "自动日常已停止";
                dailyStatus.Text = DailySet.dailyStatus;
                dailyStatus.ForeColor = Color.Red;

                _autoSignIn.stop();
                _autoHarvest.stop();
                _autoBoss.stop();
                _autoTwelve.stop();
            }
        }
    }
}
