using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class CommonBE
    {
        public long AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public long LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }


        public class CountryMasterBE
        {
            public long CountryId { get; set; }
            public string Country { get; set; }
        }
       
        public class StatesMasterBE
        {
            public long ID { get; set; }
            public string States { get; set; }
            public long? CountryId { get; set; }
        }
      
    }
}
