using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter
{
    public class OtherPG
    {
        [Resume("otherTitle")]
        public string OtherUserdefTitle1 { get; set; }
        [Resume("otherDesc")]
        public string OtherDesc1 { get; set; }

        [ResumeTime(true, "StartYear,StartMonth")]
        public long StartTime { get; set; }

        public static Other ChangeType(OtherPG pg)
        {
            Other other = new Other();
            other.otherDesc = pg.OtherDesc1;
            other.otherTitle = pg.OtherUserdefTitle1;
            other.otherUserdefTitle = pg.OtherUserdefTitle1;
            other.StartMonth = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(pg.StartTime).Month.ToString();
            other.StartYear = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(pg.StartTime).Year.ToString();
            return other;
        }
    }
    public class Other
    {
        public string otherTitle { get; set; }
        public string otherUserdefTitle { get; set; }
        public string otherDesc { get; set; }
        public string StartYear { get; set; }
        public string StartMonth { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OtherPG opg = new OtherPG();
            opg.OtherDesc1 = "test";
            opg.OtherUserdefTitle1 = "22233fsdfsdfs";
            opg.StartTime = 1493710846;

            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Start();
            ModelService service = new ModelService();
            Other type = service.Build<Other>(opg);
            stop.Stop();

            Console.WriteLine(stop.Elapsed);

            stop.Reset();
            stop.Start();
            OtherPG.ChangeType(opg);
            stop.Stop();

            Console.WriteLine(stop.Elapsed);

            Console.WriteLine(type.otherDesc);
            Console.WriteLine(type.otherTitle);
            Console.WriteLine(type.otherUserdefTitle);
            Console.WriteLine(type.StartMonth);
            Console.WriteLine(type.StartYear);

            Console.ReadKey();
        }
    }
}
