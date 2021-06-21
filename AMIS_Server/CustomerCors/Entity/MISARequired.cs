using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCors.Entity
{
    [AttributeUsage(AttributeTargets.Property)]
    class MISARequired:Attribute
    {

    }
    public class MISANoDB : Attribute
    {

    }
    public class MISAEntityCode : Attribute
    {

    }
    public class MISAEntityId : Attribute 
    {
       
    }
}
