using Sidna.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidna.Helper
{
    public class Normalize
    {
        public static HistoryType HistoryAvgNormal(History history, History fromHistory, double Range)
        {
            if (fromHistory != null)
            {
                double d1 = Math.Round(history.End, 3);
                double d2 = Math.Round(fromHistory.End, 3);

                if (d1 > d2)
                    history.Type = HistoryType.صعودی;
                else if (d1 < d2)
                    history.Type = HistoryType.نزولی;
                else history.Type = fromHistory.Type;
            }
            return history.Type;
        }
        public static void histiryType(ref List<History> list)
        {
            History fromHistory = null;
            foreach (var history in list)
            {
                if (fromHistory != null)
                {
                    if (history.End > fromHistory.End)
                        history.Type = HistoryType.صعودی;
                    else if (history.End < fromHistory.End)
                        history.Type = HistoryType.نزولی;
                }
                fromHistory = history;
            }
        }
        public static List<History> histiryType(History fromHistory, List < History> list, double Range)
        {
            //     ***  refactor  ***
            List<History> list2 = new List<History>();
            List<History> returnList = new List<History>();

            for (int i = 0; i < list.Count; i++)
            {
                var h = new History { ID = list[i].ID};
                if (i > 0)
                {
                    if (list[i].End > (fromHistory.End + Range) || list[i].End < (fromHistory.End - Range))
                        h.End = list[i].End;
                    else h.End = fromHistory.End;
                }
                else h.End = list[i].End;

                list2.Add(h);

                fromHistory = list[i];
                fromHistory.End = h.End;
            }

            returnList.Add(list[0]);
            foreach (var h in list2)
            {
                if (fromHistory.End != h.End)
                {
                    returnList.Add(h);
                    fromHistory = h;
                }
            }
            return returnList;
        }
    }
}
