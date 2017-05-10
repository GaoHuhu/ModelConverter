using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ModelConverter
{
    public class OtherPG
    {
        [Resume("Name")]
        public string OtherUserdefTitle1 { get; set; }
        [Resume("Description")]
        public string OtherDesc1 { get; set; }

        [ResumeTime(true,"Year,Month")]
        public long StartTime { get; set; }

        //public static Other ChangeType(OtherPG pg)
        //{
        //    Other other = new Other();
        //    other.SubRowId = pg.StartTime;
        //    other.Name = pg.OtherUserdefTitle1;
        //    other.Description = pg.OtherDesc1;
        //    //other.StartMonth = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(pg.StartTime).Month.ToString();
        //    //other.StartYear = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(pg.StartTime).Year.ToString();
        //    return other;
        //}
    }
    public class Other
    {
        public string SubRowId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //XmlDocument xml = new XmlDocument();
            //xml.Load(@"C:\Users\Lenovo\Desktop\1.xml");
            //XmlNode node = xml.SelectSingleNode("//ASSISTANT_ACCESSORY");
            //node.Attributes["MaxRowID"].Value = "0";
            //XElement root = XElement.Parse(xml.InnerXml);
            //root.Element("ASSISTANT_ACCESSORY").SetAttributeValue("MaxRowID", "0");

            ////XmlDocument xml1 = new XmlDocument();
            ////xml1.InnerXml = root.;
            //root.Save(Guid.NewGuid().ToString() +" xe "+ ".xml");
            //xml.Save(Guid.NewGuid().ToString() + " xd " + ".xml");
            OtherPG opg = new OtherPG();
            
            opg.OtherDesc1 = "test";
            opg.OtherUserdefTitle1 = "22233fsdfsdfs";
            opg.StartTime = 1493710846;
            //opg.StartTime = "1";

            Stopwatch stop = new Stopwatch();
            stop.Start();
            ModelService service = new ModelService();
            Other type = service.BuildModel<Other>(opg);
            stop.Stop();

            Console.WriteLine(stop.Elapsed);

            //stop.Reset();
            //stop.Start();
            //OtherPG.ChangeType(opg);
            //stop.Stop();

            //Console.WriteLine(stop.Elapsed);

            Console.WriteLine(type.SubRowId);
            Console.WriteLine(type.Name);
            Console.WriteLine(type.Description);

            Console.ReadKey();
        }
    }
}
