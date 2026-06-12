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
        private MainWindow _form;
        private CancellationTokenSource _shopTokenSource;
        private int timeoutMs = 10000;
        private int interval = 100;
        private int elapsed = 0;

        public AutoRefreshShop(MainWindow form)
        {
            _form = form;
        }

        public async void run()
        {
            if (_shopTokenSource != null)
            {
                Common.addMessage(_form.message, "当前无法开始购物，请先结束购物");
                return;
            }

            AutomationElement exit = Common.getWindow("幸运商店");
            if (exit != null)
            {
                // 关闭幸运商店窗口，以备重新打开重置状态
                Common.clickButtonById(exit, "Close");
                Thread.Sleep(1000);
            }

            string s = _form.shopNum.Text.Trim();
            if (int.TryParse(s, out int num))
            {
                AutomationElement mainWindow = null;
                elapsed = 0;
                while (elapsed < timeoutMs)
                {
                    mainWindow = Common.getWindow(_form.title.Text.Trim());

                    if (mainWindow != null)
                        break;

                    Thread.Sleep(interval);
                    elapsed += interval;
                }

                if (mainWindow == null)
                {
                    Common.addMessage(_form.message, "未找到修仙小屋窗口，请确保游戏正在运行并且窗口标题正确");
                    return;
                }

                // 打开幸运商店弹窗
                Common.clickButton(mainWindow, "幸运商店");

                AutomationElement shopWindow = null;
                elapsed = 0;
                while (elapsed < timeoutMs)
                {
                    shopWindow = Common.getWindow("幸运商店");

                    if (shopWindow != null)
                        break;

                    Thread.Sleep(interval);
                    elapsed += interval;
                }


                if (shopWindow == null)
                {
                    Common.addMessage(_form.message, "窗口未启动");
                    return;
                }


                Common.clickButtonById(shopWindow, "ShuaXinButton");

                AutomationElement noCount = Common.getElById(shopWindow, "TiShiLabel");
                if (noCount != null && noCount.Current.Name == "你的幸运点不足")
                {
                    Common.addMessage(_form.message, "你的幸运点不足, 停止");
                    return;
                }

                // 领取后自动刷新
                AutomationElement box = Common.getElById(shopWindow, "ziDongShuaXin_Name");
                TogglePattern toggle = box.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;
                bool isChecked = toggle.Current.ToggleState == ToggleState.On;
                if (toggle.Current.ToggleState != ToggleState.On)
                {
                    toggle.Toggle();
                }

                _shopTokenSource = new CancellationTokenSource();
                int count = 0;

                await Task.Run(() =>
                {
                    while (_shopTokenSource != null && !_shopTokenSource.Token.IsCancellationRequested)
                    {
                        Common.addMessage(_form.message, $"{DateTime.Now.ToString()}，第{count + 1}次执行自动化");

                        if (count == num - 1)
                        {
                            toggle.Toggle();
                        }
                        refreshStart();
                        Thread.Sleep(1000);
                        count++;
                        if (count >= num)
                        {
                            _shopTokenSource?.Cancel();
                            _shopTokenSource = null;
                            Common.addMessage(_form.message, $"已完成{num}次购物, 停止");
                        }

                        if (noCount != null && noCount.Current.Name == "你的幸运点不足")
                        {
                            _shopTokenSource?.Cancel();
                            _shopTokenSource = null;

                            Common.addMessage(_form.message, "你的幸运点不足, 停止");
                        }
                    }
                },
                _shopTokenSource.Token
                );
            }
            else
            {
                Common.addMessage(_form.message, "购物次数请输入有效的整数");
            }
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
                if (Dics.titleMap.TryGetValue(goods.name, out weight))
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
                            case "金币":
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

        public void stop()
        {
            _shopTokenSource?.Cancel();
            _shopTokenSource = null;
            Common.addMessage(_form.message, "结束购物");
        }
    }
}
