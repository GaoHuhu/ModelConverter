using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter
{
    [AttributeUsage(AttributeTargets.Class |
AttributeTargets.Constructor |
AttributeTargets.Field |
AttributeTargets.Method |
AttributeTargets.Property,
AllowMultiple = true)]
    public class ResumeAttribute:Attribute
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public ResumeAttribute(string name)
        {
            this._name = name;
        }
    }
}
