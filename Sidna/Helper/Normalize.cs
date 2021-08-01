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
            List<History> returnList = new List<History>();
            foreach (var history in list)
            {
                if (fromHistory != null)
                {
                    if(fromHistory.Type == HistoryType.صعودی)
                    {
                        if (history.Type == HistoryType.نزولی
                            && fromHistory.End > (history.End + Range))
                        {
                            fromHistory = history;
                            int i7 = list.FindIndex(x => x.ID == history.ID);
                            for (int i = list.FindIndex(x => x.ID == history.ID); i >= 0; i--)
                                if (list[i].Type == fromHistory.Type && list[i].Str > fromHistory.Str)
                                    fromHistory = list[i];
                            returnList.Add(fromHistory);
                        }
                    }
                    else if(fromHistory.Type == HistoryType.نزولی)
                    {
                        if (history.Type == HistoryType.صعودی
                            && fromHistory.End < (history.End - Range))
                        {
                            fromHistory = history;
                            for (int i = list.FindIndex(x => x.ID == history.ID); i >= 0; i--)
                                if (list[i].Type == fromHistory.Type &&  list[i].Str < fromHistory.Str)
                                    fromHistory = list[i];
                            returnList.Add(fromHistory);
                        }
                    }
                    else
                        fromHistory = history;
                }
                else
                    fromHistory = history;
            }
            return returnList;
        }
    }
}
