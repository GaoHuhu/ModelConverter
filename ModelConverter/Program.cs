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

            ModelService service = new ModelService();
            Other type = service.Build<Other>(opg);

            Console.WriteLine(type.otherDesc);
            Console.WriteLine(type.otherTitle);
            Console.WriteLine(type.otherUserdefTitle);
            Console.WriteLine(type.StartMonth);
            Console.WriteLine(type.StartYear);

            Console.ReadKey();
        }
    }
}
