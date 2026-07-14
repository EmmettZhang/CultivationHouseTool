using System;
using System.Threading;
using System.Windows.Automation;
using CommonUtils;

namespace CultivationHouseTool.actions
{
    public class AutoOut
    {
        private int timeoutMs = 10000;
        private int interval = 100;
        private int elapsed = 0;
        
        // 自动出库
        public void autoOut(AutomationElement mainWindow)
        {
            Common.changeTab(mainWindow, "娱乐", 0);
            Common.clickButton(mainWindow, "宝库");
            AutomationElement warehouse = null;
            elapsed = 0;
            while (elapsed < timeoutMs)
            {
                warehouse = Common.getWindow("宝库");

                if (warehouse != null)
                    break;

                Thread.Sleep(interval);
                elapsed += interval;
            }
            if (warehouse != null)
            {
                Common.clickButton(warehouse, "拿回所有已到期物品");
                Common.clickButtonById(warehouse, "Close");
                Thread.Sleep(new Random().Next(500, 1000));
            }
        }
    }
}