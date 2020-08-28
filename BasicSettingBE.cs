using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class BasicSettingBE
    {
        public class BasicBe
        {
            public int BasicSettingID { get; set; }
            public string Activate_Moderator { get; set; }
            public string Canteen_EmailId { get; set; }
            public string CanteenPersonName { get; set; }
            public bool SettingStatus { get; set; }
            public long AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }
    }
}
