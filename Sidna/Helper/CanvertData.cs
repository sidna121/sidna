using Sidna.Model;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async void TextTodHistory(Int64 i,string path)
        {
            try
            {
                List<History> list = new List<History>();

                _label.Text = "convert data"; await Task.Delay(10);
                using (var streamReader = File.OpenText(path))
                {
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

                _label.Text = "histiry Type";await Task.Delay(100);
                Normalize.histiryType(ref list);

                _label.Text = "Step"; await Task.Delay(100);
                List<History> step = Normalize.histiryType(null, list, 0.001);

                _label.Text = "Step => list"; await Task.Delay(100);
                foreach (var history in step)
                {
                    var h = list.First(x => x.ID == history.ID);
                    h.Step = true;
                }


                _label.Text = "slope List"; await Task.Delay(100);
                List<History> slopeList = Normalize.histiryType(null, list, 0.002);

                _label.Text = "slope ..."; await Task.Delay(100);
                Slope.getSlope(slopeList);

                _label.Text = "slopeList => list"; await Task.Delay(100);

                foreach (var history in slopeList)
                {
                    var h = list.First(x => x.ID == history.ID);
                    h.Slope = history.Slope;
                    //list[(int)history.ID].Slope = history.Slope;
                    //var t = list.FirstOrDefault(x => x.ID > history.ID);
                    //if (t != null)
                    //t.Slope = history.Slope;
                }

                foreach (var history in list)
                {
                    List<SqlParameter> sqlParameter = new List<SqlParameter> {
                            new SqlParameter("@ID", history.ID),
                            new SqlParameter("@Date", history.Date.ToString("yyyy-MM-dd hh:mm:ss")),
                            new SqlParameter("@Str", history.Str),
                            new SqlParameter("@End", history.End),
                            new SqlParameter("@Hig", history.Hig),
                            new SqlParameter("@Low", history.Low),
                            new SqlParameter("@Avg", history.Average),
                            new SqlParameter("@Type", history.Type),
                            new SqlParameter("@Step", history.Step),
                            new SqlParameter("@Slope", Math.Round(history.Slope)),
                        };

                    await dbo.spWrite("AddHistory", sqlParameter);

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
