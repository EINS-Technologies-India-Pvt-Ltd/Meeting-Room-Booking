using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class RoomBookingAppRejBE
    {
        public class RoomBookingBE
        {
            public long BookingId { get; set; }
            public string name { get; set; }
            public long RoomId { get; set; }
            public string RoomName { get; set; }
            public string BookFor { get; set; }
            public string DayType { get; set; }
            public DateTime BookingDateFrom { get; set; }
            public DateTime BookingDateTo { get; set; }
            public string FromTime { get; set; }
            public string ToTime { get; set; }
            public string BookingEvent { get; set; }
            public long Capacity { get; set; }
            public string Resources { get; set; }
            public long BookedBy { get; set; }
            public string BookedByName { get; set; }
            public bool CanteenStatus { get; set; }
            public bool ITStatus { get; set; }
            public bool Reminder { get; set; }
            public string ReminderTime { get; set; }
            public string ReminderFor { get; set; }
            public bool BookingStatus { get; set; }
            public string InvitedUsers { get; set; }
            public long AddedBy { get; set; }
            public DateTime AddedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public string ApprovalStatus { get; set; }
            public string BookinRequestStatus { get; set; }
        }


        public class ITRequestBE
        {
            public long BookingId { get; set; }
            public long? UserID { get; set; }
            public string HardwareRequest { get; set; }
            public bool RequestStatus { get; set; }
            public bool BookingStatus { get; set; }
            public string Book_or_req { get; set; }
        }

        public class GuestMasterBE
        {
            public long GuestId { get; set; }
            public long BookingId { get; set; }
            public string GuestName { get; set; }
            public string GuestCompany { get; set; }
            public string EmailId { get; set; }
            public string MobileNo { get; set; }
            public bool RequestStatus { get; set; }
            public bool BookingStatus { get; set; }
        }

        public class CanteenRequestBE
        {
            public long BookingId { get; set; }
            public int Tea { get; set; }
            public string TeaName { get; set; }
            public int Cofee { get; set; }
            public string CofeeName { get; set; }
            public string SnacksName { get; set; }
            public int Snack { get; set; }
            public string AdOns { get; set; }
            public bool RequestStatus { get; set; }
            public bool BookingStatus { get; set; }
            public string CBook_or_req { get; set; }
        }

        public class RoomMasterBE
        {
            public long RoomID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public long Capacity { get; set; }
            public long? Company { get; set; }
            public long? Location { get; set; }
            public byte[] RoomImage { get; set; }
            public bool? BookThroughModerator { get; set; }
            public bool? Status { get; set; }
            public long AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }

        public class UserMasterBe
        {
            public long? UserId { get; set; }
            public string name { get; set; }
        }

        public class roomType : RoomBookingBE
        {
            public string RoomTypeName { get; set; }
            public string Mobile { get; set; }
            public string EmailID { get; set; }
        }
    }
}
