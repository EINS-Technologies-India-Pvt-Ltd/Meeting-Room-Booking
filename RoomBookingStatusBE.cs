using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class RoomBookingStatusBE
    {
        public long BookingId { get; set; }
        public long RoomId { get; set; }
        public string BookFor { get; set; }
        public string DayType { get; set; }
        public DateTime BookingDateFrom { get; set; }
        public DateTime BookingDateTo { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string BookingEvent { get; set; }
        public int Capacity { get; set; }
        public string Resources { get; set; }
        public long BookedBy { get; set; }
        public bool CanteenStatus { get; set; }
        public bool ITStatus { get; set; }
        public bool Reminder { get; set; }
        public string ReminderTime { get; set; }
        public string ReminderFor { get; set; }
        public bool BookingStatus { get; set; }
        public string InvitedUsers { get; set; }
        public long AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public long LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string ApprovalStatus { get; set; }
        public string BookinRequestStatus { get; set; }
    }
    public class RoomBookingDataBE
    {
        public long BookingId { get; set; }
        public string RoomName { get; set; }
        public long RoomId { get; set; }
        public string BookFor { get; set; }
      
        public DateTime BookingDateFrom { get; set; }
        public DateTime BookingDateTo { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string BookingEvent { get; set; }
        public long Capacity { get; set; }
        public long ResourceId { get; set; }
        public string Resources { get; set; }
        public string ResourcesName { get; set; }
        public byte[] image { get; set; }
        public long BookedBy { get; set; }
        public string UserName{ get; set; }

   
        public string ReminderFor { get; set; }
        public bool BookingStatus { get; set; }
        public string InvitedUsers { get; set; }
        public long AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public long LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string ApprovalStatus { get; set; }
        public string BookinRequestStatus { get; set; }
    }
}
