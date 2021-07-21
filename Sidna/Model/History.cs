using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidna.Model
{
    public class History : Model
    {
        public DateTime Date { get; set; }
        public double Str  { get; set; }
        public double End  { get; set; }
        public double Hig  { get; set; }
        public double Low { get; set; }
        public double Avg { get; set; }
        public double Average => (Str + End) / 2;
        public HistoryType Type { get; set; }
        public double Slope { get; set; }
    }
}
