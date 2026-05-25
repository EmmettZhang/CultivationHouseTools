using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CultivationHouseTools
{
    internal class AutoRefreshShop
    {
        private Form1 _form;

        public AutoRefreshShop(Form1 form)
        {
            _form = form;
        }

        private readonly List<(string Key, int Weight)> weightRules =
            new List<(string, int)>
            {
                ("称号",19999997),
                ("背景",9999999),
                ("头像",9999998),
                ("无级别",999999),
                ("保级丹",999998),
                ("重置",999997),
                ("补偿丹",999996),
                ("精品精力",19999),
                ("上限",19998),
                ("上品精力",1999),
                ("修为",900),
                ("福袋",400),
                ("幸运点",199),
                ("精力丹",199),
                ("精力",199),
                ("体力丹",98),
                ("体力",98),
                ("抹痕丹",20),
                ("灵币",5),
                ("仙币",5),
                ("金币",2)
            };
        private static readonly Dictionary<string, int> map = new Dictionary<string, int>()
            {
                {"一",1},
                {"二",2},
                {"三",3}
            };
        //private static readonly Dictionary<int, string> selecterMap = new Dictionary<int, string>()
        //    {
        //        {1,"1 号"},
        //        {2,"2 号"},
        //        {3,"3 号"},
        //        {4,"4 号"},
        //        {5,"5 号"},
        //        {6,"6 号"},
        //        {7,"7 号"},
        //        {8,"8 号"},
        //        {9,"9 号"}
        //    };

        public void refreshStart()
        {
            AutomationElement shopWindow = Common.getWindow("幸运商店");

            // 获取商品列表
            Common.addMessage(_form.message, "本次刷新");
            List<Goods> list = new List<Goods>();
            for (int i = 1; i <= 9; i++)
            {
                AutomationElement label = Common.getElById(shopWindow, $"Label_{i}");
                Goods goods = new Goods();
                goods.name = label.Current.Name;
                goods.index = i;

                foreach (var rule in weightRules)
                {
                    if (goods.name.Contains(rule.Key))
                    {
                        goods.weight = rule.Weight;
                        if (rule.Key == "修为" || rule.Key == "灵币" || rule.Key == "仙币" ||
                            rule.Key == "精力" || rule.Key == "体力" || rule.Key == "幸运点" || rule.Key == "福袋")
                        {
                            Match m = Regex.Match(goods.name, @"\d+");
                            int count = int.Parse(m.Value);
                            goods.weight = goods.weight * count;
                        }
                        if (rule.Key == "背景" || rule.Key == "头像")
                        {
                            Match m = Regex.Match(goods.name, @"[一二三]");
                            int count = map[m.Value];
                            goods.weight = goods.weight * count;
                        }
                        break;
                    }
                }
                Common.addMessage(_form.message, $"{goods.ToString()}");
                list.Add(goods);
            }

            var best = list.OrderByDescending(x => x.weight).FirstOrDefault();
           
            Common.addMessage(_form.message, $"选中{best.ToString()}");

            Common.setCombo(shopWindow, $"{best.index} 号");
            Thread.Sleep(200);

            Common.clickButton(shopWindow, "领取物品");
        }
    }
}
