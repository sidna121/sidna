using Sidna.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sidna.Helper
{
    public class CanvertData
    {
        Label _label;
        double _length = 0;
        double _count = 0;
        DboConnection dbo = new DboConnection();
        Slope slope = new Slope();

        public CanvertData(Label label)
        {
            _label = label;
        }

        public async void TextTodHistory(){
            try
            {
                List<History> list = new List<History>();
                using (var streamReader = File.OpenText(@"D:\kama\111\EURUSD\1.txt"))
                {
                    Int64 i = 0;
                    var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    _length = lines.Length;

                    foreach (var line in lines)
                    {
                        var obj = line.Split(',');
                        list.Add(new History
                        {
                            ID = i,
                            Date = DateTime.Parse(getDate(obj[1], obj[2])) ,
                            Str = double.Parse(obj[3]),
                            End = double.Parse(obj[4]),
                            Hig = double.Parse(obj[5]),
                            Low = double.Parse(obj[6])
                        });
                        i++;
                    }
                }

                foreach (var history in list)
                {
                    var historyIndex = list.IndexOf(history);
                    var fromHistory = list.FirstOrDefault(x=> x.ID == history.ID - 1);
                   
                    history.Type = Normalize.HistoryAvgNormal(history, fromHistory, 0.001);
                    
                    var historySlope = Slope.getSlope(history, fromHistory, list);
                    if (historySlope.ID > 0)
                        list.First(x => x.ID == historySlope.ID).Slope = historySlope.Slope;

                    List<SqlParameter> sqlParameter = new List<SqlParameter> {
                            new SqlParameter("@ID", history.ID),
                            new SqlParameter("@Date", history.Date.ToString("yyyy-MM-dd hh:mm:ss")),
                            new SqlParameter("@Str", history.Str),
                            new SqlParameter("@End", history.End),
                            new SqlParameter("@Hig", history.Hig),
                            new SqlParameter("@Low", history.Low),
                            new SqlParameter("@Avg", history.Average),
                            new SqlParameter("@Type", history.Type),
                            new SqlParameter("@SlopeID", historySlope.ID),
                            new SqlParameter("@Slope", historySlope.Slope),
                        };

                    await dbo.spWrite("AddHistory", sqlParameter);

                    //if (history.Type != HistoryType.Unknown)
                    //    fromHistory = history;
                    _count++;
                    if (_count % 10 == 0)
                        _label.Text = $"{_count} of {_length} : {Math.Round((_count * 100) / _length, 2) }";
                }
            }
            catch (Exception ex)
            {
                var t = ex.Message;
            }
        }

        string getDate(string date, string watch)
        {
            var dateTime = date.Substring(0, 4)
                           + "-" + date.Substring(4, 2)
                           + "-" + date.Substring(6, 2)
                           + " " + watch.Substring(0, 2)
                           + ":" + watch.Substring(2, 2) + ":00";
            return dateTime;
        }
    }
}
