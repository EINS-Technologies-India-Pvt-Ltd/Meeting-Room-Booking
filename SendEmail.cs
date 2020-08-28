using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class SendEmail
    {
        public int bE_Id { get; set; }
        public int bE_From { get; set; }
        public string bE_To { get; set; }
        public string bE_EmailStatus { get; set; }
        public int bE_IsScheduled { get; set; }
        public long AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public long LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string bE_EmailBody { get; set; }
        public string bE_Subject { get; set; }
        public int OrgId { get; set; }
        public int LocId { get; set; }
        public string bE_Priority { get; set; }
        public DateTime bE_BroadcastDate { get; set; }
        public string bE_Isdeleted { get; set; }
        public int bE_ScheduleId { get; set; }
        public string bE_ContactType { get; set; }
        public long AppointmentId { get; set; }
        public string bE_VisitorData { get; set; }

    }
}
