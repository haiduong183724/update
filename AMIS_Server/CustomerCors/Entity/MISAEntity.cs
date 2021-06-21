using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCors.Entity
{
    public class MISAEntity
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set;}
        public DateTime? ModifiedDate { get; set; } = null;
        public string ModifiedBy { get; set; }
    }
}
