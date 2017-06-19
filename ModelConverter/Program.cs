using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ModelConverter
{
    public class OtherPG
    {
        //[Resume("Name")]
        public string OtherUserdefTitle1 { get; set; }
        //[Resume("Description")]
        public string OtherDesc1 { get; set; }

        //[ResumeTime(true,"Year,Month")]
        //public long StartTime { get; set; }


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
        [Resume("OtherUserdefTitle1")]
        public string Name { get; set; }
        [Resume("OtherDesc1")]
        public string Description { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
    }

    class Program
    {
        public static Type BuildDynamicTypeWithProperties()
        {
            AppDomain myDomain = Thread.GetDomain();
            AssemblyName myAsmName = new AssemblyName();
            myAsmName.Name = "MyDynamicAssembly";

            // To generate a persistable assembly, specify AssemblyBuilderAccess.RunAndSave.
            AssemblyBuilder myAsmBuilder = myDomain.DefineDynamicAssembly(myAsmName,
                                                            AssemblyBuilderAccess.RunAndSave);
            // Generate a persistable single-module assembly.
            ModuleBuilder myModBuilder =
                myAsmBuilder.DefineDynamicModule(myAsmName.Name, myAsmName.Name + ".dll");

            TypeBuilder myTypeBuilder = myModBuilder.DefineType("CustomerData",
                                                            TypeAttributes.Public);

            FieldBuilder customerNameBldr = myTypeBuilder.DefineField("customerName",
                                                            typeof(string),
                                                            FieldAttributes.Private);

            // The last argument of DefineProperty is null, because the
            // property has no parameters. (If you don't specify null, you must
            // specify an array of Type objects. For a parameterless property,
            // use an array with no elements: new Type[] {})
            PropertyBuilder custNamePropBldr = myTypeBuilder.DefineProperty("CustomerName",
                                                             PropertyAttributes.HasDefault,
                                                             typeof(string),
                                                             null);

            // The property set and property get methods require a special
            // set of attributes.
            MethodAttributes getSetAttr =
                MethodAttributes.Public | MethodAttributes.SpecialName |
                    MethodAttributes.HideBySig;

            // Define the "get" accessor method for CustomerName.
            MethodBuilder custNameGetPropMthdBldr =
                myTypeBuilder.DefineMethod("get_CustomerName",
                                           getSetAttr,
                                           typeof(string),
                                           Type.EmptyTypes);

            ILGenerator custNameGetIL = custNameGetPropMthdBldr.GetILGenerator();

            custNameGetIL.Emit(OpCodes.Ldarg_0);
            custNameGetIL.Emit(OpCodes.Ldfld, customerNameBldr);
            custNameGetIL.Emit(OpCodes.Ret);

            // Define the "set" accessor method for CustomerName.
            MethodBuilder custNameSetPropMthdBldr =
                myTypeBuilder.DefineMethod("set_CustomerName",
                                           getSetAttr,
                                           null,
                                           new Type[] { typeof(string) });

            ILGenerator custNameSetIL = custNameSetPropMthdBldr.GetILGenerator();

            custNameSetIL.Emit(OpCodes.Ldarg_0);
            custNameSetIL.Emit(OpCodes.Ldarg_1);
            custNameSetIL.Emit(OpCodes.Stfld, customerNameBldr);
            custNameSetIL.Emit(OpCodes.Ret);

            // Last, we must map the two methods created above to our PropertyBuilder to 
            // their corresponding behaviors, "get" and "set" respectively. 
            custNamePropBldr.SetGetMethod(custNameGetPropMthdBldr);
            custNamePropBldr.SetSetMethod(custNameSetPropMthdBldr);


            Type retval = myTypeBuilder.CreateType();

            // Save the assembly so it can be examined with Ildasm.exe,
            // or referenced by a test program.
            myAsmBuilder.Save(myAsmName.Name + ".dll");
            return retval;
        }
        static void Main(string[] args)
        {
            //Type custDataType = BuildDynamicTypeWithProperties();

            //PropertyInfo[] custDataPropInfo = custDataType.GetProperties();
            //foreach (PropertyInfo pInfo in custDataPropInfo)
            //{
            //    Console.WriteLine("Property '{0}' created!", pInfo.ToString());
            //}

            //Console.WriteLine("---");
            //// Note that when invoking a property, you need to use the proper BindingFlags -
            //// BindingFlags.SetProperty when you invoke the "set" behavior, and 
            //// BindingFlags.GetProperty when you invoke the "get" behavior. Also note that
            //// we invoke them based on the name we gave the property, as expected, and not
            //// the name of the methods we bound to the specific property behaviors.

            //object custData = Activator.CreateInstance(custDataType);
            //custDataType.InvokeMember("CustomerName", BindingFlags.SetProperty,
            //                              null, custData, new object[] { "Joe User" });

            //Console.WriteLine("The customerName field of instance custData has been set to '{0}'.",
            //                   custDataType.InvokeMember("CustomerName", BindingFlags.GetProperty,
            //                                              null, custData, new object[] { }));
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
            //opg.StartTime = 1493710846;
            //opg.StartTime = "1";

            //Stopwatch stop = new Stopwatch();
            //stop.Start();
            //ModelService service = new ModelService();
            //Other type = service.BuildModel<Other>(opg);
            //stop.Stop();

            //Console.WriteLine(stop.Elapsed);

            //stop.Reset();
            //stop.Start();
            //OtherPG.ChangeType(opg);
            //stop.Stop();

            //Console.WriteLine(stop.Elapsed);

            //Other other = new Other();

            //other.Name = "test";
            //other.Description = "22233fsdfsdfs";
            //opg.StartTime = 1493710846;
            //opg.StartTime = "1";

            //Stopwatch stop = new Stopwatch();
            //stop.Start();
            ModelService service = new ModelService();
            //OtherPG type = service.BuildModel<OtherPG>(other);
            //stop.Stop();

            //Console.WriteLine(stop.Elapsed);

            //Console.WriteLine(type.OtherDesc1);
            //Console.WriteLine(type.OtherUserdefTitle1);

            Dictionary<string, string> maps = new Dictionary<string, string>();
            maps.Add("Name", "OtherUserdefTitle1");
            maps.Add("Description", "OtherDesc1");

            Other o = new Other();
            o=service.BuildModel<Other, OtherPG>(o, opg, maps);

            Console.ReadKey();
        }
    }
}
