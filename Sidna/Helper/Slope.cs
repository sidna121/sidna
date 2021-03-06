using Sidna.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidna.Helper
{
    public class Slope
    {
        DboConnection dbo = new DboConnection();
        //public async Task<double> getSlope(History Type, string date )
        //{
        //    double slope = 0;

        //    HistoryType t = HistoryType.نزولی;
        //    if(Type == HistoryType.نزولی)
        //        t = HistoryType.صعودی;
        //    string query = "SELECT TOP 1 * FROM [SidnaDB].[dbo].[History] " +
        //        $"where [Date] < '{date}' AND Type = {(byte)t}" +
        //        "order by date desc";
        //    var result = await dbo.Read<History>(query);
        //    if(result.Data.Any())
        //    {

        //    }

        //    return slope;
        //}
        public static History getSlope(History history, History fromHistory, List<History> list)
        {

            History slope = new History();
            if (fromHistory != null &&
                history.Type != HistoryType.Unknown)
            {
                HistoryType type = history.Type == HistoryType.نزولی ? HistoryType.صعودی : HistoryType.نزولی;
                var result = list.LastOrDefault(x => x.Type == type);
                if (result != null && result.Slope == 0)
                {
                    var i = Math.Round(history.Average - result.Average, 4) * 100;
                    var i2 = (history.Date - result.Date).TotalMinutes;
                    slope.Slope = i2 / i;
                    slope.ID = result.ID;
                }
            }

            return slope;
        }
        public static void getSlope(List<History> SlpeList, ref List<History> list)
        {
            foreach (var history in SlpeList)
            {
                var h1 = SlpeList.FirstOrDefault(x => x.ID > history.ID);
                if(h1 != null)
                {
                    var s1 = list.First(x => x.ID == h1.ID);
                    var s2 = list.First(x => x.ID == history.ID);
                    var i = Math.Round(s1.Str - s2.Str, 4) * 100;
                    var i2 = (s1.Date - s2.Date).TotalMinutes;
                    s2.Slope = i2 / i;
                }
            }
        }
        //public static void getSlope(History slope, List<History> list)
        //{
        //    double end = 0;
        //    if(slope.)
        //    var end = slope.Hig
        //}
    }
}