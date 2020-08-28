using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class FeedbackBE
    {
        public class FeedbackSaveBE
        {
            public long FeedbackID { get; set; }
            public long userid { get; set; }
            public string question { get; set; }
            public string Seating_Arrangement { get; set; }
            public string Canteen_Facility  { get; set; }
            public string IT_Facility  { get; set; }
            public string Room_Environment { get; set; }
            public string Meeting_Your_Needs { get; set; }
            public string comment { get; set; }
            public DateTime? MeetingDate { get; set; }
            public int RoomId { get; set; }
            public long AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }
        public class feedbackReportBE
        {
            public long? FeedbackID { get; set; }
            public long? UserId { get; set; }
            public string userName { get; set; }
            public string MobileNo { get; set; }
            public string Email { get; set; }
            public string RoomName { get; set; }
            public DateTime? MeetingDate { get; set; }
            public string LocationName { get; set; }
            public string CompanyName { get; set; }
            public string UserType { get; set; }
            public string Seating_Arrangement { get; set; }
            public string question { get; set; }
            public string Canteen_Facility { get; set; }
            public string IT_Facility { get; set; }
            public string Room_Environment { get; set; }
            public string Meeting_Your_Needs { get; set; }
        }
        public class feedbackReportCRBE
        {
            public long? FeedbackID { get; set; }
            public long? UserId { get; set; }
            public string userName { get; set; }
            public string MobileNo { get; set; }
            public string Email { get; set; }
            public string RoomName { get; set; }
            public DateTime MeetingDate { get; set; }
            public string LocationName { get; set; }
            public string CompanyName { get; set; }
            public string UserType { get; set; }
            public string Seating_Arrangement { get; set; }
            public string question { get; set; }
            public string Canteen_Facility { get; set; }
            public string IT_Facility { get; set; }
            public string Room_Environment { get; set; }
            public string Meeting_Your_Needs { get; set; }
        }
    }
}
