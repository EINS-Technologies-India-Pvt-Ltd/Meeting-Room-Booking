using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBDL;
using RBBE;
using System.Data.SqlClient;
using System.Data;


namespace RBDL
{
    public class EventLogDL
    {
        CommonDL _ObjCommonDL = new CommonDL();
        EINS_RBMSEntities DB = new EINS_RBMSEntities();
        public List<ReportBE.SectionBE> getSectionData()
        {
            var result = from section in DB.SectionMasters
                         where section.Status == true
                         select new ReportBE.SectionBE
                         {
                             SectionID = section.SectionID,
                             SectionName = section.SectionName,
                             Status = section.Status.Value,
                             SectionHeadId = section.SectionHeadId,
                             SectionHead = section.SectionHead,
                             EmailSmsStatus = section.EmailSmsStatus.Value
                         };
            return result.ToList();
        }

        //public List<ReportBE.EventLogBE> getALLeventLogData()
        //{
        //    var result = from eventlog in DB.EventLogs
        //                 join user in DB.UserMasters on eventlog.UserID equals user.ID
        //                 where user.Status == true
        //                 orderby eventlog.SerialNo descending
        //                 select new ReportBE.EventLogBE
        //                 {
        //                     SerialNo = eventlog.SerialNo,
        //                     UserID = eventlog.UserID.Value,
        //                     MachineName = eventlog.MachineName,
        //                     Date = eventlog.EventDateTime.Value,
        //                     FormName = eventlog.FormName,
        //                     Action = eventlog.Action,
        //                     Result = eventlog.Result,
        //                     Details = eventlog.Details,
        //                     BrowserName = eventlog.BrowserName,

        //                     ID = user.ID,
        //                     EmployeeManualID = user.EmployeeManualID,
        //                     EmployeeTypeId = user.EmployeeTypeId.Value,
        //                     Name = user.Name,
        //                 };
        //    return result.ToList();
        //}
        //public List<ReportBE.EventLogBE> getALLeventLogData(long userID)
        //{
        //    var result = from eventlog in DB.EventLogs
        //                 join user in DB.UserMasters on eventlog.UserID equals user.ID
        //                 where user.Status == true && eventlog.UserID==userID
        //                 orderby eventlog.SerialNo descending
        //                 select new ReportBE.EventLogBE
        //                 {
        //                     SerialNo = eventlog.SerialNo,
        //                     UserID = eventlog.UserID.Value,
        //                     MachineName = eventlog.MachineName,
        //                     EventDateTime = eventlog.EventDateTime.Value,
        //                     FormName = eventlog.FormName,
        //                     Action = eventlog.Action,
        //                     Result = eventlog.Result,
        //                     Details = eventlog.Details,
        //                     BrowserName = eventlog.BrowserName,

        //                     ID = user.ID,
        //                     EmployeeManualID = user.EmployeeManualID,
        //                     EmployeeTypeId = user.EmployeeTypeId.Value,
        //                     Name = user.Name,
        //                 };
        //    return result.ToList();
        //}
        //public List<ReportBE.EventLogBE> getALLeventLogDataOnRadioDateOnchkUser(DateTime fromDate,DateTime ToDate,long userID)
        //{
        //    var result = from eventlog in DB.EventLogs
        //                 join user in DB.UserMasters on eventlog.UserID equals user.ID
                       
        //                 where user.Status == true && (eventlog.EventDateTime.Value >= fromDate && eventlog.EventDateTime.Value <= ToDate) && eventlog.UserID == userID
        //                 orderby eventlog.SerialNo descending
        //                 select new ReportBE.EventLogBE
        //                 {
        //                     SerialNo = eventlog.SerialNo,
        //                     UserID = eventlog.UserID.Value,
        //                     MachineName = eventlog.MachineName,
        //                     EventDateTime = eventlog.EventDateTime.Value,
        //                     FormName = eventlog.FormName,
        //                     Action = eventlog.Action,
        //                     Result = eventlog.Result,
        //                     Details = eventlog.Details,
        //                     BrowserName = eventlog.BrowserName,

        //                     ID = user.ID,
        //                     EmployeeManualID = user.EmployeeManualID,
        //                     EmployeeTypeId = user.EmployeeTypeId.Value,
        //                     Name = user.Name,
        //                 };
        //    return result.ToList();
        //}

        //public List<ReportBE.EventLogBE> getALLeventLogDataOnRadioDate(DateTime fromDate, DateTime ToDate)
        //{
        //    var result = from eventlog in DB.EventLogs
        //                 join user in DB.UserMasters on eventlog.UserID equals user.ID
        //                 where user.Status == true && (eventlog.EventDateTime.Value >= fromDate && eventlog.EventDateTime.Value <= ToDate)
        //                 orderby eventlog.SerialNo descending
        //                 select new ReportBE.EventLogBE
        //                 {
        //                     SerialNo = eventlog.SerialNo,
        //                     UserID = eventlog.UserID.Value,
        //                     MachineName = eventlog.MachineName,
        //                     EventDateTime = eventlog.EventDateTime.Value,
        //                     FormName = eventlog.FormName,
        //                     Action = eventlog.Action,
        //                     Result = eventlog.Result,
        //                     Details = eventlog.Details,
        //                     BrowserName = eventlog.BrowserName,

        //                     ID = user.ID,
        //                     EmployeeManualID = user.EmployeeManualID,
        //                     EmployeeTypeId = user.EmployeeTypeId.Value,
        //                     Name = user.Name,
        //                 };
        //    return result.ToList();
        //}

        //public List<ReportBE.EventLogBE> getALLeventLogDataOnRadioMonth(int Month,int YEAR)
        //{
        //    var result = from eventlog in DB.EventLogs
        //                 join user in DB.UserMasters on eventlog.UserID equals user.ID
        //                 where user.Status == true && (eventlog.EventDateTime.Value.Month == Month && eventlog.EventDateTime.Value.Year == YEAR)
        //                 orderby eventlog.SerialNo descending
        //                 select new ReportBE.EventLogBE
        //                 {
        //                     SerialNo = eventlog.SerialNo,
        //                     UserID = eventlog.UserID.Value,
        //                     MachineName = eventlog.MachineName,
        //                     EventDateTime = eventlog.EventDateTime.Value,
        //                     FormName = eventlog.FormName,
        //                     Action = eventlog.Action,
        //                     Result = eventlog.Result,
        //                     Details = eventlog.Details,
        //                     BrowserName = eventlog.BrowserName,

        //                     ID = user.ID,
        //                     EmployeeManualID = user.EmployeeManualID,
        //                     EmployeeTypeId = user.EmployeeTypeId.Value,
        //                     Name = user.Name,
        //                 };
        //    return result.ToList();
        //}

        //public List<ReportBE.EventLogBE> getALLeventLogDataOnRadioMonthOnchkUser(int Month, int YEAR, long userID)
        //{
        //    var result = from eventlog in DB.EventLogs
        //                 join user in DB.UserMasters on eventlog.UserID equals user.ID
        //                 where user.Status == true && (eventlog.EventDateTime.Value.Month == Month && eventlog.EventDateTime.Value.Year == YEAR) && eventlog.UserID==userID
        //                 orderby eventlog.SerialNo descending
        //                 select new ReportBE.EventLogBE
        //                 {
        //                     SerialNo = eventlog.SerialNo,
        //                     UserID = eventlog.UserID.Value,
        //                     MachineName = eventlog.MachineName,
        //                     EventDateTime = eventlog.EventDateTime.Value,
        //                     FormName = eventlog.FormName,
        //                     Action = eventlog.Action,
        //                     Result = eventlog.Result,
        //                     Details = eventlog.Details,
        //                     BrowserName = eventlog.BrowserName,

        //                     ID = user.ID,
        //                     EmployeeManualID = user.EmployeeManualID,
        //                     EmployeeTypeId = user.EmployeeTypeId.Value,
        //                     Name = user.Name,
        //                 };
        //    return result.ToList();
        //}

        //public List<ReportBE.EventLogBE> getALLeventLogDataOnRadioMonthOnchkTimeOnChkUser(int Month, int YEAR,long userID, DateTime fromTime,DateTime ToTime)
        //{
        //    var result = from eventlog in DB.EventLogs
        //                 join user in DB.UserMasters on eventlog.UserID equals user.ID
        //                 where user.Status == true && (eventlog.EventDateTime.Value.Month == Month && eventlog.EventDateTime.Value.Year == YEAR) && eventlog.UserID == userID
        //                 && (eventlog.EventDateTime.Value.TimeOfDay.Hours >= fromTime.TimeOfDay.Hours && eventlog.EventDateTime.Value.TimeOfDay.Minutes >= fromTime.TimeOfDay.Minutes && eventlog.EventDateTime.Value.TimeOfDay.Hours <= ToTime.TimeOfDay.Hours && eventlog.EventDateTime.Value.TimeOfDay.Minutes <= ToTime.TimeOfDay.Minutes)
        //                 orderby eventlog.SerialNo descending
        //                 select new ReportBE.EventLogBE
        //                 {
        //                     SerialNo = eventlog.SerialNo,
        //                     UserID = eventlog.UserID.Value,
        //                     MachineName = eventlog.MachineName,
        //                     EventDateTime = eventlog.EventDateTime.Value,
        //                     FormName = eventlog.FormName,
        //                     Action = eventlog.Action,
        //                     Result = eventlog.Result,
        //                     Details = eventlog.Details,
        //                     BrowserName = eventlog.BrowserName,

        //                     ID = user.ID,
        //                     EmployeeManualID = user.EmployeeManualID,
        //                     EmployeeTypeId = user.EmployeeTypeId.Value,
        //                     Name = user.Name,
        //                 };
        //    return result.ToList();
        //}

        public DataTable getALLeventLogDataOnRadioUserDateOnchkTime(DateTime fromDate, DateTime ToDate, long userID,long companyID,long LocationId,DateTime fromTime,DateTime ToTime )
        {
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT e.[SerialNo],e.[UserID] ,u.ID, u.EmployeeManualID, u.EmployeeTypeId ,u.Name,l.[LocationName],c.[CompanyName],e.[MachineName],CAST(e.[EventDateTime] as Date) as Date ,CONVERT(varchar, CAST(e.EventDateTime as time(0)), 100) as Time ,e.[FormName],e.[Action],e.[Result] ,e.[Details] ,e.[BrowserName] FROM [EINS_RBMS].[dbo].[EventLog] e join [EINS_RBMS].[dbo].[UserMaster] u on(u.Status=1 and e.[UserID]=u.[ID]) join [EINS_RBMS].[dbo].[CompanyMaster] c on(c.Status=1 and u.[Company]=c.[CompanyID] ) join [EINS_RBMS].[dbo].[LocationMaster] L on (l.LocationStatus=1 and u.[Location]=l.[LocationId]) Where e.UserID=" + userID + " and u.[Company]=" + companyID + " and u.[Location]=" + LocationId + " and CAST(e.EventDateTime as Date)>='" + fromDate + "' and CAST(e.EventDateTime as Date)<='" + ToDate + "' and CAST(e.EventDateTime as time(0)) >='" + fromTime + "' and CAST(e.EventDateTime as time(0)) <='" + ToTime + "'order by e.[SerialNo] desc");

            return db;
        }
        public DataTable getALLeventLogData()
        {
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT e.[SerialNo],e.[UserID] ,u.ID, u.EmployeeManualID, u.EmployeeTypeId ,u.Name,l.[LocationName],c.[CompanyName],e.[MachineName],CAST(e.[EventDateTime] as Date) as Date ,CONVERT(varchar, CAST(e.EventDateTime as time(0)), 100) as Time ,e.[FormName],e.[Action],e.[Result] ,e.[Details] ,e.[BrowserName] FROM [EINS_RBMS].[dbo].[EventLog] e join [EINS_RBMS].[dbo].[UserMaster] u on(u.Status=1 and e.[UserID]=u.[ID]) join [EINS_RBMS].[dbo].[CompanyMaster] c on(c.Status=1 and u.[Company]=c.[CompanyID] ) join [EINS_RBMS].[dbo].[LocationMaster] L on (l.LocationStatus=1 and u.[Location]=l.[LocationId]) order by e.[SerialNo] desc ");
            return db;
        }
        public DataTable getALLeventLogDataDateOnchkTime(DateTime fromDate, DateTime ToDate, long companyID, long LocationId, DateTime fromTime, DateTime ToTime)
        {
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT e.[SerialNo],e.[UserID] ,u.ID, u.EmployeeManualID, u.EmployeeTypeId ,u.Name,l.[LocationName],c.[CompanyName],e.[MachineName],CAST(e.[EventDateTime] as Date) as Date ,CONVERT(varchar, CAST(e.EventDateTime as time(0)), 100) as Time ,e.[FormName],e.[Action],e.[Result] ,e.[Details] ,e.[BrowserName] FROM [EINS_RBMS].[dbo].[EventLog] e join [EINS_RBMS].[dbo].[UserMaster] u on(u.Status=1 and e.[UserID]=u.[ID]) join [EINS_RBMS].[dbo].[CompanyMaster] c on(c.Status=1 and u.[Company]=c.[CompanyID] ) join [EINS_RBMS].[dbo].[LocationMaster] L on (l.LocationStatus=1 and u.[Location]=l.[LocationId]) Where u.[Company]=" + companyID + " and u.[Location]=" + LocationId + " and CAST(e.EventDateTime as Date)>='" + fromDate + "' and CAST(e.EventDateTime as Date)<='" + ToDate + "' and CAST(e.EventDateTime as time(0)) >='" + fromTime + "' and CAST(e.EventDateTime as time(0)) <='" + ToTime + "'order by e.[SerialNo] desc");

            return db;
        }
        public DataTable getALLeventLogDataDate(DateTime fromDate, DateTime ToDate, long companyID, long LocationId)
        {
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT e.[SerialNo],e.[UserID] ,u.ID, u.EmployeeManualID, u.EmployeeTypeId ,u.Name,l.[LocationName],c.[CompanyName],e.[MachineName],CAST(e.[EventDateTime] as Date) as Date ,CONVERT(varchar, CAST(e.EventDateTime as time(0)), 100) as Time ,e.[FormName],e.[Action],e.[Result] ,e.[Details] ,e.[BrowserName] FROM [EINS_RBMS].[dbo].[EventLog] e join [EINS_RBMS].[dbo].[UserMaster] u on(u.Status=1 and e.[UserID]=u.[ID]) join [EINS_RBMS].[dbo].[CompanyMaster] c on(c.Status=1 and u.[Company]=c.[CompanyID] ) join [EINS_RBMS].[dbo].[LocationMaster] L on (l.LocationStatus=1 and u.[Location]=l.[LocationId]) Where u.[Company]=" + companyID + " and u.[Location]=" + LocationId + " and CAST(e.EventDateTime as Date)>='" + fromDate + "' and CAST(e.EventDateTime as Date)<='" + ToDate + "' order by e.[SerialNo] desc");

            return db;
        }
        public DataTable getALLeventLogDataOnRadioUserDate(DateTime fromDate, DateTime ToDate, long userID, long companyID, long LocationId)
        {
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT e.[SerialNo],e.[UserID] ,u.ID, u.EmployeeManualID, u.EmployeeTypeId ,u.Name,l.[LocationName],c.[CompanyName],e.[MachineName],CAST(e.[EventDateTime] as Date) as Date ,CONVERT(varchar, CAST(e.EventDateTime as time(0)), 100) as Time ,e.[FormName],e.[Action],e.[Result] ,e.[Details] ,e.[BrowserName] FROM [EINS_RBMS].[dbo].[EventLog] e join [EINS_RBMS].[dbo].[UserMaster] u on(u.Status=1 and e.[UserID]=u.[ID]) join [EINS_RBMS].[dbo].[CompanyMaster] c on(c.Status=1 and u.[Company]=c.[CompanyID] ) join [EINS_RBMS].[dbo].[LocationMaster] L on (l.LocationStatus=1 and u.[Location]=l.[LocationId]) Where e.UserID=" + userID + " and u.[Company]=" + companyID + " and u.[Location]=" + LocationId + " and CAST(e.EventDateTime as Date)>='" + fromDate + "' and CAST(e.EventDateTime as Date)<='" + ToDate + "' order by e.[SerialNo] desc");

            return db;
        }
        public DataTable getALLeventLogDataOnRadioEmpIDDateOnchkTime(DateTime fromDate, DateTime ToDate, string EmpManualID, long companyID, long LocationId, DateTime fromTime, DateTime ToTime)
        {
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT e.[SerialNo],e.[UserID] ,u.ID, u.EmployeeManualID, u.EmployeeTypeId ,u.Name,l.[LocationName],c.[CompanyName],e.[MachineName],CAST(e.[EventDateTime] as Date) as Date ,CONVERT(varchar, CAST(e.EventDateTime as time(0)), 100) as Time ,e.[FormName],e.[Action],e.[Result] ,e.[Details] ,e.[BrowserName] FROM [EINS_RBMS].[dbo].[EventLog] e join [EINS_RBMS].[dbo].[UserMaster] u on(u.Status=1 and e.[UserID]=u.[ID]) join [EINS_RBMS].[dbo].[CompanyMaster] c on(c.Status=1 and u.[Company]=c.[CompanyID] ) join [EINS_RBMS].[dbo].[LocationMaster] L on (l.LocationStatus=1 and u.[Location]=l.[LocationId]) Where u.EmployeeManualID='" + EmpManualID + "' and u.[Company]=" + companyID + " and u.[Location]=" + LocationId + " and CAST(e.EventDateTime as Date)>='" + fromDate + "' and CAST(e.EventDateTime as Date)<='" + ToDate + "' and CAST(e.EventDateTime as time(0)) >='" + fromTime + "' and CAST(e.EventDateTime as time(0)) <='" + ToTime + "'order by e.[SerialNo] desc");

            return db;
        }
        public DataTable getALLeventLogDataOnRadioEmpIDDate(DateTime fromDate, DateTime ToDate, string EmpManualID, long companyID, long LocationId  )
        {
            DataTable db = _ObjCommonDL.executeSelectQuery("SELECT e.[SerialNo],e.[UserID] ,u.ID, u.EmployeeManualID, u.EmployeeTypeId ,u.Name,l.[LocationName],c.[CompanyName],e.[MachineName],CAST(e.[EventDateTime] as Date) as Date ,CONVERT(varchar, CAST(e.EventDateTime as time(0)), 100) as Time ,e.[FormName],e.[Action],e.[Result] ,e.[Details] ,e.[BrowserName] FROM [EINS_RBMS].[dbo].[EventLog] e join [EINS_RBMS].[dbo].[UserMaster] u on(u.Status=1 and e.[UserID]=u.[ID]) join [EINS_RBMS].[dbo].[CompanyMaster] c on(c.Status=1 and u.[Company]=c.[CompanyID] ) join [EINS_RBMS].[dbo].[LocationMaster] L on (l.LocationStatus=1 and u.[Location]=l.[LocationId]) Where u.EmployeeManualID='" + EmpManualID + "' and u.[Company]=" + companyID + " and u.[Location]=" + LocationId + " and CAST(e.EventDateTime as Date)>='" + fromDate + "' and CAST(e.EventDateTime as Date)<='" + ToDate + "' order by e.[SerialNo] desc");

            return db;
        }
    }
}
