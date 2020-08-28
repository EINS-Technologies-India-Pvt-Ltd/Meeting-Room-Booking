using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;
using RBDL;
using System.Data;

namespace RBBL
{
   public class ClsSendEmail_BL
    {
       ClsSendEmail_DL _objEmail = new ClsSendEmail_DL();
       public DataTable getEmailConfigurationDetails()
       {
           return _objEmail.getEmailConfigurationDetails();
       }
        //To send Mail
        public long insertTO_SendEmail(SendEmail _objEmailPro)
        {
            return _objEmail.insertTO_SendEmail(_objEmailPro);
        }
        public DataTable dtSend_EmailDetails(string dtAddedOn)
        {
            return _objEmail.dtSend_EmailDetails(dtAddedOn);
        }
        public DataTable dt_SearchWith_EmailId(string EmailId)
        {
            return _objEmail.dt_SearchWith_EmailId(EmailId);
        }
        public DataTable dt_SearchShowAll()
        {
            return _objEmail.dt_SearchShowAll();
        }
        public DataTable dt_ViewDetails(int Id)
        {
            return _objEmail.dt_ViewDetails(Id);
        }
        public int dtDeleteSendEmail(string iRemove)
        {
            return _objEmail.dtDeleteSendEmail(iRemove);
        }
       //For Sending EMAIL
        //Get Email Configuration Details
        public DataTable getEmailConfigurationDetails(int iComID, int iLocID)
        {
            return _objEmail.getEmailConfigurationDetails(iComID, iLocID);
        }
        public DataTable getGuest_MAilID(long BookingId) {
            return _objEmail.getGuest_MAilID(BookingId);
        }
        public DataTable getRoomUser_MAilID(long BookingId)
        {
            return _objEmail.getRoomUser_MAilID(BookingId);
        }
        public DataTable getModerator_MAilID(long BookingId)
        {
            return _objEmail.getModerator_MAilID(BookingId);
        }
        public DataTable getTeamMembers_MAilID(long BookingId)
        {
            return _objEmail.getTeamMembers_MAilID(BookingId);
        }
        public DataTable getApprover_MailID(long UserId)
        {
            return _objEmail.getApprover_MailID(UserId);
        }
    }
}
