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
                double d1 = Math.Round(history.Average, Range.ToString().Length);
                double d2 = Math.Round(fromHistory.Average, Range.ToString().Length);

                if(d1 != (d2 + Range) && d1 != (d2 - Range))
                {
                    if (history.Average > fromHistory.Average)
                        history.Type = HistoryType.صعودی;
                    else if(history.Average < fromHistory.Average)
                        history.Type = HistoryType.نزولی;
                }
            }
            return history.Type;
        }
    }
}
