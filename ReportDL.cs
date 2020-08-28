using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;
using RBDL;
using System.Data;
using System.Data.SqlClient;
using RBBE;
using RBDL;

namespace RBDL
{
    public class ReportDL
    {
        CommonDL _ObjCommonDL = new CommonDL();

        public DataTable GetRoomBookingReportDetails(List<long> BookingID)
        {
            //DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName," +
            //                                                      "[BookFor] as BookingFor,[DayType],[Event],[FromDate],[ToDate],[FromTime],[ToTime],RM.[Capacity]," +
            //                                                      "[ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName]," +
            //                                                      "[GuestCompany],GM.[EmailId],[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB " +
            //                                                      "JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON RB.[RoomId] = RM.[RoomId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId " +
            //                                                      "WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName, [BookFor] as BookingFor,[DayType],[Event],RB.[FromDate],RB.[ToDate],[FromTime],[ToTime],RM.[Capacity], [ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName], [GuestCompany],GM.[EmailId],GM.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB  JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON (RM.[Status]=1 and RB.[RoomId] = RM.[RoomId])   LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ") and RB.[Status]=1");

            return db;
        }
        public DataTable getBookingstatusgraphicalData()
        {
            //DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName,[ApprovalStatus] as ApprovalStatus," +
            //                                                      "[BookFor] as BookingFor,[DayType],[Event],RB.[FromDate] as BookingDateFrom,RB.[ToDate]as BookingDateTo,[FromTime],[ToTime],RM.[Capacity]," +
            //                                                      "[ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName]," +
            //                                                      "[GuestCompany],GM.[EmailId],RB.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON RB.[RoomId] = RM.[RoomId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId ");
            // "WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");

            // DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName, [BookFor] as BookingFor,[DayType],[Event],RB.[FromDate],RB.[ToDate],[FromTime],[ToTime],RM.[Capacity], [ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName], [GuestCompany],GM.[EmailId],GM.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB  JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON RB.[RoomId] = RM.[RoomId]   LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT  [BookingId],[Name] as RoomName,[BookFor] ,[MobileNo] ,[EmailID] ,[DayType] ,[Event]"
     + " ,[FromDate] as BookingDateFrom,[ToDate] as BookingDateTo,[ApprovalStatus] FROM [EINS_RBMS].[dbo].[RoomBooking] rb left join "
     + "[EINS_RBMS].[dbo].[RoomMaster] rm on (rm.[Status]=1 and  rm.[RoomId]=rb.[RoomId]) where  rb.[Status]=1");
            return db;
        }

        public DataTable getBookingstatusgraphicalDataonrbDate(DateTime FromDate, DateTime toDate)
        {
            //DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName,[ApprovalStatus] as ApprovalStatus," +
            //                                                      "[BookFor] as BookingFor,[DayType],[Event],RB.[FromDate] as BookingDateFrom,RB.[ToDate]as BookingDateTo,[FromTime],[ToTime],RM.[Capacity]," +
            //                                                      "[ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName]," +
            //                                                      "[GuestCompany],GM.[EmailId],RB.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON (RM.[Status]=1 and RB.[RoomId] = RM.[RoomId] )" +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId where CONVERT(DATE,RB.[FromDate])>=CONVERT(DATE,'" + FromDate + "')  AND CONVERT(DATE,RB.[ToDate])<=CONVERT(DATE,'" + toDate + "') and RB.[Status]=1 ");
           
            // "WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");
            // DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName, [BookFor] as BookingFor,[DayType],[Event],RB.[FromDate],RB.[ToDate],[FromTime],[ToTime],RM.[Capacity], [ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName], [GuestCompany],GM.[EmailId],GM.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB  JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON RB.[RoomId] = RM.[RoomId]   LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT  [BookingId],[Name] as RoomName,[BookFor] ,[MobileNo] ,[EmailID] ,[DayType] ,[Event]"
  + " ,[FromDate] as BookingDateFrom,[ToDate] as BookingDateTo,[ApprovalStatus] FROM [EINS_RBMS].[dbo].[RoomBooking] rb left join "
  + "[EINS_RBMS].[dbo].[RoomMaster] rm on (rm.[Status]=1 and  rm.[RoomId]=rb.[RoomId]) where  rb.[Status]=1 and CONVERT(DATE,rb.[FromDate])>=CONVERT(DATE,'" + FromDate + "')  AND CONVERT(DATE,rb.[ToDate])<=CONVERT(DATE,'" + toDate + "') ");
            return db;
        }

        public DataTable getBookingstatusgraphicalDataonrbMonth(int Month, int year)
        {
            //DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName,[ApprovalStatus] as ApprovalStatus," +
            //                                                      "[BookFor] as BookingFor,[DayType],[Event],RB.[FromDate] as BookingDateFrom,RB.[ToDate]as BookingDateTo,[FromTime],[ToTime],RM.[Capacity]," +
            //                                                      "[ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName]," +
            //                                                      "[GuestCompany],GM.[EmailId],RB.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON ( RM.[Status]=1 and RB.[RoomId] = RM.[RoomId]) " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId where MONTH(RB.[FromDate])='" + Month + "' AND YEAR(RB.[ToDate])='" + year + "' and RB.[Status]=1");
            
            // "WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");
            // DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName, [BookFor] as BookingFor,[DayType],[Event],RB.[FromDate],RB.[ToDate],[FromTime],[ToTime],RM.[Capacity], [ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName], [GuestCompany],GM.[EmailId],GM.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB  JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON RB.[RoomId] = RM.[RoomId]   LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT  [BookingId],[Name] as RoomName,[BookFor] ,[MobileNo] ,[EmailID] ,[DayType] ,[Event]"
  + " ,[FromDate] as BookingDateFrom,[ToDate] as BookingDateTo,[ApprovalStatus] FROM [EINS_RBMS].[dbo].[RoomBooking] rb left join "
  + "[EINS_RBMS].[dbo].[RoomMaster] rm on (rm.[Status]=1 and  rm.[RoomId]=rb.[RoomId]) where  rb.[Status]=1 and MONTH(RB.[FromDate])='" + Month + "' AND YEAR(RB.[ToDate])='" + year + "'");
            return db;
        }
        public DataTable getBookingstatusgraphicalDataOnRoomName(string str)
        {
            //DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName,[ApprovalStatus] as ApprovalStatus," +
            //                                                      "[BookFor] as BookingFor,[DayType],[Event],RB.[FromDate] as BookingDateFrom,RB.[ToDate]as BookingDateTo,[FromTime],[ToTime],RM.[Capacity]," +
            //                                                      "[ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName]," +
            //                                                      "[GuestCompany],GM.[EmailId],RB.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON (RM.[Status]=1 and RB.[RoomId] = RM.[RoomId] )" +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId where RM.[Name] like '%" + str + "%' and RB.[Status]=1 ");
            
            // "WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");
            // DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName, [BookFor] as BookingFor,[DayType],[Event],RB.[FromDate],RB.[ToDate],[FromTime],[ToTime],RM.[Capacity], [ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName], [GuestCompany],GM.[EmailId],GM.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB  JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON RB.[RoomId] = RM.[RoomId]   LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT  [BookingId],[Name] as RoomName,[BookFor] ,[MobileNo] ,[EmailID] ,[DayType] ,[Event]"
+ " ,[FromDate]as BookingDateFrom,[ToDate] as BookingDateTo,[ApprovalStatus] FROM [EINS_RBMS].[dbo].[RoomBooking] rb left join "
+ "[EINS_RBMS].[dbo].[RoomMaster] rm on (rm.[Status]=1 and  rm.[RoomId]=rb.[RoomId]) where  rb.[Status]=1 and rm.[Name] like '%" + str + "%'");
            return db;
        }

        public DataTable getBookingstatusgraphicalDataOnStatus(string status)
        {
            //DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName,[ApprovalStatus] as ApprovalStatus," +
            //                                                      "[BookFor] as BookingFor,[DayType],[Event],RB.[FromDate] as BookingDateFrom,RB.[ToDate]as BookingDateTo,[FromTime],[ToTime],RM.[Capacity]," +
            //                                                      "[ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName]," +
            //                                                      "[GuestCompany],GM.[EmailId],RB.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON (RM.[Status]=1 and RB.[RoomId] = RM.[RoomId] )" +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID " +
            //                                                      "LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId where [ApprovalStatus] = '" + status + "'and RB.[Status]=1 ");
           
            // "WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");
            // DataTable db = _ObjCommonDL.executeSelectQuery("SELECT DISTINCT RB.[BookingId],UM.[Name] as ITName,[HardwareRequest] as Hardware,RM.[Name] as RoomName, [BookFor] as BookingFor,[DayType],[Event],RB.[FromDate],RB.[ToDate],[FromTime],[ToTime],RM.[Capacity], [ReminderFor],[TeaQuantity],[Tea],[CofeeQuantity],[Cofee],[Snacks],[SnackQuantity],[AdOns],[GuestName], [GuestCompany],GM.[EmailId],GM.[MobileNo],[GuestId] FROM [EINS_RBMS].[dbo].[RoomBooking] RB  JOIN [EINS_RBMS].[dbo].[RoomMaster] RM ON RB.[RoomId] = RM.[RoomId]   LEFT JOIN [EINS_RBMS].[dbo].[CanteenRequest] CR ON RB.[BookingId] = CR.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[IT_Request] IT ON RB.[BookingId] = IT.[BookingId] LEFT JOIN [EINS_RBMS].[dbo].[UserMaster] UM ON IT.[UserID] = UM.ID LEFT JOIN [EINS_RBMS].[dbo].[GuestMaster] GM on GM.BookingId=RB.BookingId WHERE RB.[BookingId]  in(" + string.Join(",", BookingID) + ")");
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT  [BookingId],[Name] as RoomName,[BookFor] ,[MobileNo] ,[EmailID] ,[DayType] ,[Event]"
  + " ,[FromDate] as BookingDateFrom,[ToDate] as BookingDateTo,[ApprovalStatus] FROM [EINS_RBMS].[dbo].[RoomBooking] rb left join "
  + "[EINS_RBMS].[dbo].[RoomMaster] rm on (rm.[Status]=1 and  rm.[RoomId]=rb.[RoomId]) where  rb.[Status]=1 and [ApprovalStatus] = '" + status + "'");
            return db;
        }
    }
}
