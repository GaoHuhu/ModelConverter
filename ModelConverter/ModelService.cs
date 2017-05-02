﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter
{
    public class ModelService
    {
        public T Build<T>(object left) where T : class
        {
            if (left == null)
                return default(T);

            T t = (T)Activator.CreateInstance(typeof(T));

            foreach (var property in left.GetType().GetProperties())
            {
                foreach (var att in property.GetCustomAttributes(true))
                {
                    Type type = att.GetType();

                    if (type.Name.Equals("ResumeAttribute"))
                    {
                        ResumeAttribute attribute = (ResumeAttribute)att;
                        foreach (var attr in t.GetType().GetProperties())
                        {
                            if (attribute.Name.Equals(attr.Name))
                            {
                                t.GetType().GetProperty(attr.Name).SetValue(t, left.GetType().GetProperty(property.Name).GetValue(left));
                            }
                        }
                    }
                    else if (type.Name.Equals("ResumeTimeAttribute"))
                    {
                        ResumeTimeAttribute attribute = (ResumeTimeAttribute)att;
                        foreach (var attr in t.GetType().GetProperties())
                        {
                            if (attribute.IsTime)
                            {
                                string[] fields = attribute.Fields.Split(',');

                                long time = (long)left.GetType().GetProperty(property.Name).GetValue(left);
                                DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(time);
                                t.GetType().GetProperty(fields[0]).SetValue(t, dateTime.Year.ToString());
                                t.GetType().GetProperty(fields[1]).SetValue(t, dateTime.Month.ToString());
                            }
                        }
                    }
                }
            }

            return t;
        }
    }
}