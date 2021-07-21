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
            if (fromHistory !=null)
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
    }
}
