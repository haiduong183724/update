using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCors.Entity
{
    public class Department :MISAEntity
    {
        public Guid DepartmentId { get; set; } = new Guid();
        public string DepartmentName { get; set; }
        public string Decription { get; set; }
    }
}
