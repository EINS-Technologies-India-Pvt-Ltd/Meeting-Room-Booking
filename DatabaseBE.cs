using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
   public class DatabaseBE
   {
       public int TaskID { get; set; }
       public string TaskName { get; set; }
        public DateTime TimeValue { get; set; }
        public bool IsOverride { get; set; }
        public bool ScheduleType { get; set; }
        public string strScheduleType { get; set; }
        public bool IsMonday { get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsFriday { get; set; }
        public bool IsSaturday { get; set; }
        public bool IsSunday { get; set; }
        public long? AddedBy { get; set; }
        public long? LastModifiedBy { get; set; }
        public string ServerPath { get; set; }
        public bool Status { get; set; }
    }
}
