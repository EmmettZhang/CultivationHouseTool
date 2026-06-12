using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CultivationHouseTools.lib
{
    public static class UnknownLib
    {

        public static Dictionary<int, List<int>> RemainMap = new Dictionary<int, List<int>>();

        private static CancellationTokenSource _cts;
        private static Task _task;
        private static Random _rnd = new Random();

        private static List<TimeSpan> times = new List<TimeSpan>() { new TimeSpan(9, 29, 00), new TimeSpan(15, 29, 00), new TimeSpan(21, 29, 00) };


        public static void run()
        {
            DoWork();
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

                try
                {
                    DoWork();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        public static List<int> shuffle()
        {
            List<int> list = Enumerable.Range(0, 400).ToList();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rnd.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }

            return list;
        }

        public static void DoWork()
        {
            for (int i = 0; i <= 9; i++)
            {
                RemainMap[i] = shuffle();
            }
        }
    }
}
