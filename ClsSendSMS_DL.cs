using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using RBBE;

namespace RBDL
{
   public class ClsSendSMS_DL
    {
       CommonDL _objCommonDL = new CommonDL();
        //To send SMS
        public long insertTO_SendSMS(SendSms _objSms)
        {
            long returnvalue;
            try
            {
                SqlParameter paramContactNo = new SqlParameter("@ContactNo", SqlDbType.NVarChar);
                paramContactNo.Value = _objSms.ContactNo;

                SqlParameter paramMessage = new SqlParameter("@Message", SqlDbType.NVarChar);
                paramMessage.Value = _objSms.Message;

                SqlParameter paramMessage_Status = new SqlParameter("@Message_Status", SqlDbType.VarChar);
                paramMessage_Status.Value = _objSms.Message_Status;

                SqlParameter paramFormName = new SqlParameter("@FormName", SqlDbType.VarChar);
                paramFormName.Value = _objSms.FormName;

                SqlParameter paramScheduleId = new SqlParameter("@ScheduleId", SqlDbType.BigInt);
                paramScheduleId.Value = _objSms.ScheduleId;

                SqlParameter paramAppointmentId = new SqlParameter("@AppointmentId", SqlDbType.BigInt);
                paramAppointmentId.Value = _objSms.AppointmentId;

                SqlParameter paramAddedBy = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                paramAddedBy.Value = _objSms.AddedBy;

                SqlParameter paramSuccess = new SqlParameter("@Success", SqlDbType.Bit);
                paramSuccess.Value = 0;
                paramSuccess.Direction = ParameterDirection.Output;

                SqlParameter[] _parameters = {
                                                paramContactNo,
                                                paramMessage,
                                                paramMessage_Status,
                                                paramFormName,
                                                paramScheduleId,
                                                paramAppointmentId,
                                                paramAddedBy,
                                                paramSuccess
            };
                returnvalue = _objCommonDL.executeReturnLongNonSelectQuery("[dbo].[Isp_SendSMS]", _parameters);
                return returnvalue;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int dtDeleteSendSms(string iRemove)
        {
           // int deleteId = _objCommonDL.executeScalarQuery("UPDATE [EMAIL].[dbo].[BroadcastEmail] SET [bE_Isdeleted]=1  WHERE [bE_Id] IN (" + iRemove + ")");
            int deleteId = _objCommonDL.executeScalarQuery("UPDATE [EINS_RBMS_SMS].[dbo].[Sent] SET [Sms_Status]=0  WHERE [SentId] IN (" + iRemove + ")");
            return deleteId;
        }

        public DataTable dt_SearchShowAll()
        {
            DataTable dt = new DataTable();
            dt = _objCommonDL.executeSelectQuery("select [SentId],[ContactNo],[Date],[Message],[Message_Status],[FormName],[ScheduleId],[AppointmentId],[AddedBy],Convert(DATE,AddedOn) as[AddedOn], CONVERT(varchar, CAST([AddedOn] as time(0)), 100) as Time,[Sms_Status] FROM [EINS_RBMS_SMS].[dbo].[Sent]  WHERE [Sms_Status]=1 order by SentId");
            return dt;
        }

    }
}
