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
    public class ResumeTimeAttribute:Attribute
    {
        private string _fields;
        private bool isTime;
        public string Fields
        {
            get
            {
                return _fields;
            }
        }
        public bool IsTime
        {
            get
            {
                return isTime;
            }
        }
        public ResumeTimeAttribute(bool isTime, string fields)
        {
            this.isTime = isTime;
            this._fields = fields;
        }
    }
}
