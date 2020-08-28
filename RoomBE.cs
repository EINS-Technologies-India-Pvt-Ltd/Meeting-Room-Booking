using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class RoomBE
    {
        public class RoomMaster
        {
            public long RoomID { get; set; }
            public string RoomManualId { get; set; }
            public long RoomTypeId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public long Capacity { get; set; }
            public long Company { get; set; }
            public long Location { get; set; }
            public byte[] RoomImage { get; set; }
            public bool BookThroughModerator { get; set; }
            public string Resources { get; set; }
            public bool Status { get; set; }
            public long? AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public string BuildingName { get; set; }
            public string Floor { get; set; }
            public string StreetName { get; set; }
            public string City { get; set; }
            public long CountryId { get; set; }
            public long StateId { get; set; }
            public string PinCode { get; set; }
            public string EmpIds { get; set; }
            public string ApprovalStatus { get; set; }
        }
      
        public class RoomMasterForView : RoomMaster
        {
            public string RoomTypeName { get; set; }
            public string CompanyName { get; set; }
            public string LocationName { get; set; }
            public string CountryName { get; set; }
            public string StateName { get; set; }
            public string FullAddress { get; set; }
        }
        public class RoomBooking
        {
            public long BookingID { get; set; }
            public long? RoomID { get; set; }
            public string RoomName { get; set; }
            public string DayType { get; set; }
            public long BookForId { get; set; }
            public string BookFor { get; set; }
            public string MobileNo { get; set; }
            public string EmailID { get; set; }
            public DateTime? FromDate { get; set; }
            public string FromTime { get; set; }
            public DateTime? ToDate { get; set; }
            public string ToTime { get; set; }
            public string BookingEvent { get; set; }
            public long Capacity { get; set; }
            public string Resources { get; set; }
            public long? BookedBy { get; set; }
            public bool? CanteenStatus { get; set; }
            public bool? ITStatus { get; set; }
            public bool? Reminder { get; set; }
            public string ReminderTime { get; set; }
            public string ReminderFor { get; set; }
            public bool? BookingStatus { get; set; }
            public string InvitedUsers { get; set; }
            public string ApprovedStatus { get; set; }
            public string BookingRequestStatus { get; set; }
            public long AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }
        public class RoomBookingForAvailabilty : RoomBooking
        {
            public byte[] RoomImage { get; set; }
            public bool BookThroughModerator { get; set; }
            public string RoomDescription { get; set; }
            public string RoomTypeName { get; set; }
            public string CompanyName { get; set; }
            public string LocationName { get; set; }
            public string CountryName { get; set; }
            public string StateName { get; set; }
            public string FullAddress { get; set; }
        }
        public class Canteen
        {
            public long ID { get; set; }
            public long BookingID { get; set; }
            public string Tea { get; set; }
            public string Coffee { get; set; }
            public string Snacks { get; set; }
            public int? TeaQuantity { get; set; }
            public int? CoffeeQuantity { get; set; }
            public int? SnacksQuantity { get; set; }
            public string AadOns { get; set; }
            public string BookRequest { get; set; }
            public bool? BookingStatus { get; set; }
            public bool? RequestStatus { get; set; }
        }

        public class IT : Canteen
        {
            public long UserID { get; set; }
            public string HardwareRequest { get; set; }
        }

        public class GuestMaster : Canteen
        {
            public string GuestName { get; set; }
            public string GuestCompany { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
        }

        public class RoomTypeMaster
        {
            public long RoomTypeId { get; set; }
            public string RoomTypeName { get; set; }
            public string RoomTypeDescription { get; set; }
            public bool? Status { get; set; }
        }
        public class RoomNameMaster
        {
            public long RoomID { get; set; }
            public string Name { get; set; }
            public bool Status { get; set; }
        }
    }
}
