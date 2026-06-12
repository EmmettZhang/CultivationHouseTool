using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultivationHouseTools.lib
{
    public static class DailySet
    {
        public static string dailyStatus;

        public static bool monday;
        public static bool tuesday;
        public static bool wednesday;
        public static bool thursday;
        public static bool friday;

        public static string boss;

        public static string luckyCount;

        public static string happyBag;

        public static string attackMethod;
        public static String print()
        {
            return $"每日兑换：周一：{(monday ? '是' : '否')}，周二：{(tuesday ? '是' : '否')}，周三：{(wednesday ? '是' : '否')}，周四：{(thursday ? '是' : '否')}，周五：{(friday ? '是' : '否')}，每日Boss：{boss}，幸运点：{luckyCount}，福袋：{happyBag}，真Boss：{attackMethod}";
        }
    }
}
