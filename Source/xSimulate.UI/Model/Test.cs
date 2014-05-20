using System;
using System.Collections.Generic;
using System.Text;

namespace NetFramework.DataAccess.EntityBuilder
{
    public class DataMappingAttribute : Attribute
    {
        public DataMappingAttribute()
        {

        }
        public DataMappingAttribute(string columnName)
        {

        }
    }

    public class ReferencedEntityAttribute : Attribute
    {
        public ReferencedEntityAttribute()
        {

        }
        public ReferencedEntityAttribute(string prefix)
        {

        }
    }
}
