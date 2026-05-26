using CultivationHouseTools.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CultivationHouseTools
{
    public partial class DailySetWindow : Form
    {
        private MainWindow _form;
        public DailySetWindow(MainWindow form)
        {
            InitializeComponent();
            _form = form;
            
            boss.Text = DailySet.boss;
            luckyCount.Text = DailySet.luckyCount;
            happyBag.Text = DailySet.happyBag;
            monday.Checked = DailySet.monday;
            tuesday.Checked = DailySet.tuesday;
            wednesday.Checked = DailySet.wednesday;
            thursday.Checked = DailySet.thursday;
            friday.Checked = DailySet.friday;
        }

        private void setConfirm_Click(object sender, EventArgs e)
        {
            
            DailySet.monday = monday.Checked;
            DailySet.tuesday = tuesday.Checked;
            DailySet.wednesday = wednesday.Checked;
            DailySet.thursday = thursday.Checked;
            DailySet.friday = friday.Checked;
            DailySet.boss = boss.Text;
            DailySet.luckyCount = luckyCount.Text;
            DailySet.happyBag = happyBag.Text;
            Common.addMessage(_form.dailyMessage, $"已设置{DailySet.ToString()}");
            this.Close();
        }
    }
}
