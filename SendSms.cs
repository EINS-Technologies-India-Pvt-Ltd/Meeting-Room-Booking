using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
   public class SendSms
    {
        public int SentId { get; set; }
        public string ContactNo { get; set; }
        public string Message { get; set; }
        public string Message_Status { get; set; }
        public string FormName { get; set; }
        public long ScheduleId { get; set; }
        public long AppointmentId { get; set; }
        public long AddedBy { get; set; }
    }
}
