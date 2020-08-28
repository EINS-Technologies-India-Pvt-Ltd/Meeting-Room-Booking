using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;
using System.Data;
using System.Data.SqlClient;

namespace RBDL
{
    public class ClsSendEmail_DL
    {
        CommonDL _objCommonDL = new CommonDL();
        //Get Email Configuration Details
        public DataTable getEmailConfigurationDetails(int iComID, int iLocID)
        {
            DataTable dtMailConfigDetails = new DataTable();
            dtMailConfigDetails = _objCommonDL.executeSelectQuery("Select case when (Select [eC_Id] from [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] where [CompanyId]=" + iComID + " and [LocationId]=" + iLocID + " and [eC_status]='true' and [eC_IsActive]='true') IS NOT NULL then (Select [eC_Id] from [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] where [CompanyId]=" + iComID + " and [LocationId]=" + iLocID + "  and [eC_status]='true' and [eC_IsActive]='true') else (Select [eC_Id] from [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] where [CompanyId]=0 and [LocationId]=0  and [eC_status]='true' and [eC_IsActive]='true') End As [ID] from [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration]");
            return dtMailConfigDetails;
        }
        public DataTable getEmailConfigurationDetails()
        {
            DataTable dtMailConfigDetails = new DataTable();
            dtMailConfigDetails = _objCommonDL.executeSelectQuery("Select case when (Select [eC_Id] from [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] where [eC_status]='true' and [eC_IsActive]='true') IS NOT NULL then (Select [eC_Id] from [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] where [eC_status]='true' and [eC_IsActive]='true') else (Select [eC_Id] from [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] where [eC_status]='true' and [eC_IsActive]='true') End As [ID] from [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration]");
            return dtMailConfigDetails;
        }
        //To send Mail
        public long insertTO_SendEmail(SendEmail _objEmail)
        {
            long returnvalue;
            try
            {
                SqlParameter parambE_From = new SqlParameter("@bE_From", SqlDbType.Int);
                parambE_From.Value = _objEmail.bE_From;

                SqlParameter parambE_To = new SqlParameter("@bE_To", SqlDbType.VarChar);
                parambE_To.Value = _objEmail.bE_To;

                SqlParameter parambE_EmailStatus = new SqlParameter("@bE_EmailStatus", SqlDbType.VarChar);
                parambE_EmailStatus.Value = _objEmail.bE_EmailStatus;

                SqlParameter parambE_IsScheduled = new SqlParameter("@bE_IsScheduled", SqlDbType.Int);
                parambE_IsScheduled.Value = _objEmail.bE_IsScheduled;

                SqlParameter paramAddedBy = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                paramAddedBy.Value = _objEmail.AddedBy;

                SqlParameter parambE_EmailBody = new SqlParameter("@bE_EmailBody", SqlDbType.VarChar);
                parambE_EmailBody.Value = _objEmail.bE_EmailBody;

                SqlParameter parambE_Subject = new SqlParameter("@bE_Subject", SqlDbType.VarChar);
                parambE_Subject.Value = _objEmail.bE_Subject;

                SqlParameter paramOrgId = new SqlParameter("@OrgId", SqlDbType.Int);
                paramOrgId.Value = _objEmail.OrgId;

                SqlParameter paramLocId = new SqlParameter("@LocId", SqlDbType.Int);
                paramLocId.Value = _objEmail.LocId;

                SqlParameter parambE_Priority = new SqlParameter("@bE_Priority", SqlDbType.VarChar);
                parambE_Priority.Value = "Normal";

                SqlParameter parambE_Isdeleted = new SqlParameter("@bE_Isdeleted", SqlDbType.VarChar);
                parambE_Isdeleted.Value = "0";

                SqlParameter parambE_ScheduleId = new SqlParameter("@bE_ScheduleId", SqlDbType.Int);
                parambE_ScheduleId.Value = _objEmail.bE_ScheduleId;

                SqlParameter parambE_ContactType = new SqlParameter("@bE_ContactType", SqlDbType.VarChar);
                parambE_ContactType.Value = _objEmail.bE_ContactType;

                SqlParameter paramAppointmentId = new SqlParameter("@AppointmentId", SqlDbType.BigInt);
                paramAppointmentId.Value = _objEmail.AppointmentId;

                SqlParameter parambE_VisitorData = new SqlParameter("@bE_VisitorData", SqlDbType.VarChar);
                parambE_VisitorData.Value = _objEmail.bE_VisitorData;

                SqlParameter paramSuccess = new SqlParameter("@Success", SqlDbType.Bit);
                paramSuccess.Value = 0;
                paramSuccess.Direction = ParameterDirection.Output;

                SqlParameter[] _parameters = {
                parambE_From,
                parambE_To,
                parambE_EmailStatus,
                parambE_IsScheduled,
                paramAddedBy,
                parambE_EmailBody,
                parambE_Subject,
                paramOrgId,
                paramLocId, 
                parambE_Priority,
                parambE_Isdeleted,
                parambE_ScheduleId,
                parambE_ContactType,
                paramAppointmentId,
                parambE_VisitorData,
                paramSuccess
            };
                returnvalue = _objCommonDL.executeReturnLongNonSelectQuery("[EINS_RBMS].[dbo].[Isp_SendEmail]", _parameters);
                return returnvalue;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataTable dt_SearchShowAll()
        {
            DataTable dt = new DataTable();
            dt = _objCommonDL.executeSelectQuery(" SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 order by [bE_To],bE_Id DESC");
            return dt;
        }

        public DataTable dtSend_EmailDetails(string dtAddedOn)
        {
            DataTable dt = new DataTable();
            dt = _objCommonDL.executeSelectQuery(" SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND CONVERT(DATE,E.[AddedOn])=CONVERT(DATE,'" + dtAddedOn + "') order by [bE_To],bE_Id DESC");
            return dt;
        }

        public DataTable dt_SearchWith_EmailId(string EmailId)
        {
            DataTable dt = new DataTable();
            dt = _objCommonDL.executeSelectQuery(" SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id]  WHERE [bE_Isdeleted]=0 AND bE_To='" + EmailId + "'");
            return dt;
        }
        public DataTable dt_ViewDetails(int Id)
        {
            DataTable dt = new DataTable();
            dt = _objCommonDL.executeSelectQuery(" SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(varchar(15),E.AddedOn,103)AS AddedOn,[eC_UserName],[companyName],locationName,[eC_SMTPServerName],[eC_IsActive], CONVERT(Varchar(20),[bE_BroadcastDate],108) AS  bE_BroadcastDat_TIME,eC_SMTPPort,bE_EmailBody FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E "
                                                 + " INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] "
                                                 + " INNER JOIN [EINS_RBMS].[dbo].[CompanyMaster] C ON C.companyId=EL.CompanyId INNER JOIN [EINS_RBMS].[dbo].[LocationMaster] L ON L.locationId=EL.LocationId WHERE [bE_Isdeleted]=0 AND [bE_Id]='" + Id + "'");
            return dt;
        }

        public int dtDeleteSendEmail(string iRemove)
        {

            int deleteId = _objCommonDL.executeScalarQuery("UPDATE  [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] SET [bE_Isdeleted]=1  WHERE [bE_Id] IN (" + iRemove + ")");
            return deleteId;
        }
        public DataTable getGuest_MAilID(long BookingId)
        {
            DataTable dtVisitorEmailID = new DataTable();
            dtVisitorEmailID = _objCommonDL.executeSelectQuery("SELECT [EmailId],[GuestName],[MobileNo] FROM [EINS_RBMS].[dbo].[GuestMaster] where [BookingStatus]=1 and [BookingId]=" + BookingId);
            return dtVisitorEmailID;
        }
        public DataTable getRoomUser_MAilID(long BookingId)
        {
            DataTable dtUserEmailID = new DataTable();
            dtUserEmailID = _objCommonDL.executeSelectQuery("select EmailID,Event,MobileNo,BookFor,MobileNo from [EINS_RBMS].[dbo].[RoomBooking] where BookingId=" + BookingId);
            return dtUserEmailID;
        }

        public DataTable getModerator_MAilID(long BookingId)
        {
            DataTable dtVisitorEmailID = new DataTable();
            StringBuilder str = new StringBuilder("");
            DataTable dtEmps = _objCommonDL.executeSelectQuery("select [EmpIds] from [EINS_RBMS].[dbo].[Employee_RoomAccessAuthority] where RoomId=(select RoomId from [EINS_RBMS].[dbo].[RoomBooking] where BookingId=" + BookingId + ")");
            if (dtEmps != null && dtEmps.Rows.Count > 0)
            {
                for (int i = 0; i < dtEmps.Rows.Count; i++)
                {
                    if (str.ToString().Equals(""))
                    {
                        str.Append(dtEmps.Rows[i]["EmpIds"].ToString());
                    }
                    else
                    {
                        str.Append("," + Convert.ToString(dtEmps.Rows[i]["EmpIds"]) + "");
                    }
                }
            }

            dtVisitorEmailID = _objCommonDL.executeSelectQuery("select [EmailID],[Name],[Mobile] FROM [EINS_RBMS].[dbo].[UserMaster] where ID in (" + str.ToString().Trim(',') + ")");
            return dtVisitorEmailID;
        }
        public DataTable getTeamMembers_MAilID(long BookingId)
        {
            DataTable dtVisitorEmailID = new DataTable();
            string strEmps = _objCommonDL.executeScalarQuerystr("select InvitedUsers from [EINS_RBMS].[dbo].[RoomBooking] where [BookingId]=" + BookingId);

            dtVisitorEmailID = _objCommonDL.executeSelectQuery("select [EmailID],[Name],[Mobile] FROM [EINS_RBMS].[dbo].[UserMaster] where ID in (" + strEmps.Trim(',') + ")");
            return dtVisitorEmailID;
        }
        public DataTable getApprover_MailID(long UserID)
        {
            DataTable dtVisitorEmailID = _objCommonDL.executeSelectQuery("select [EmailID],[Name],[Mobile] FROM [EINS_RBMS].[dbo].[UserMaster] where ID = " + UserID);
            return dtVisitorEmailID;
        }
    }

}
