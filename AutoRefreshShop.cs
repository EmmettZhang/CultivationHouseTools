using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using CultivationHouseTools.lib;
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


        public void refreshStart()
        {
            AutomationElement shopWindow = Common.getWindow("幸运商店");

            // 获取商品列表
            Common.addMessage(_form.message, "本次刷新");
            List<Goods> list = new List<Goods>();
            for (int i = 1; i <= 9; i++)
            {
                AutomationElement label = Common.getElById(shopWindow, $"Label_{i}");
                Goods goods = new Goods(label.Current.Name, i, 0);

                int weight = 0;
                if (Dics.weightMap.TryGetValue(goods.name, out weight))
                {
                    // 直接获取权重
                    goods.weight = weight;
                }
                else
                {
                    (string key, int currentWeight) = Dics.weightRules.Find(x => goods.name.Contains(x.Key));
                    if (key != null)
                    {
                        switch (key)
                        {
                            case "修为":
                            case "灵币":
                            case "仙币":
                            case "精力":
                            case "体力":
                            case "幸运点":
                            case "福袋":
                            case "属性":
                                Match m = Regex.Match(goods.name, @"\d+");
                                int count = int.Parse(m.Value);
                                goods.weight = currentWeight * count;
                                break;
                            case "背景":
                            case "头像":
                                Match m2 = Regex.Match(goods.name, @"[一二三]");
                                int count2 = Dics.levelMap[m2.Value];
                                goods.weight = currentWeight * count2;
                                break;
                            case "体力丹":
                            case "精力丹":
                                Match m3 = Regex.Match(goods.name, @"[下中上精]品");
                                int count3 = Dics.gradeMap[m3.Value];
                                goods.weight = currentWeight * count3;
                                break;
                            default:
                                goods.weight = currentWeight;
                                break;
                        }
                    }
                }
                
                Common.addMessage(_form.message, $"{goods.ToString()}");
                list.Add(goods);
            }

            var best = list.OrderByDescending(x => x.weight).FirstOrDefault();
           
            Common.addMessage(_form.message, $"选中{best.ToString()}");

            Common.setCombo(shopWindow, $"{best.index} 号");
            Thread.Sleep(100);

            Common.clickButton(shopWindow, "领取物品");
        }
    }
}
