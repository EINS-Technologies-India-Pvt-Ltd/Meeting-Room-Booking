using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class ReportBE
    {
        public class RoomBookingBE
        {
            public long BookningID { get; set; }
            public string ITName { get; set; }
            public string Hardware { get; set; }
            public string RoomName { get; set; }
            public string BookingFor { get; set; }
            public string DayType { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public string FromTime { get; set; }
            public string ToTime { get; set; }
            public int? Capacity { get; set; }
            public string ReminderFor { get; set; }
            public string Tea { get; set; }
            public int? TeaQuantity { get; set; }
            public string Coffee { get; set; }
            public int? CoffeeQuantity { get; set; }
            public string Snacks { get; set; }
            public int? SnacksQuantity { get; set; }
            public string AddOns { get; set; }
            public long GuestID { get; set; }
            public string GuestName { get; set; }
            public string GuestCompany { get; set; }
            public string GuestEmail { get; set; }
            public string GuestMobile { get; set; }
        }
        public class SectionBE
        {
            public int SectionID { get; set; }
            public string SectionName { get; set; }
            public string SectionHead { get; set; }
            public int SectionHeadId { get; set; }
            public bool Status { get; set; }
            public bool? EmailSmsStatus { get; set; }
        }
        public class EventLogBE
        {
            //from userMAster
            public long ID { get; set; }
            public String EmployeeManualID { get; set; }
            public long EmployeeTypeId { get; set; }
            public String Name { get; set; }

            //from EventLog
            public long SerialNo { get; set; }
            public long UserID { get; set; }
            public String MachineName { get; set; }
            public DateTime Date { get; set; }
            public String FormName { get; set; }
            public String Action { get; set; }
            public String Result { get; set; }
            public String Details { get; set; }
            public String BrowserName { get; set; }
        }
    }
}
