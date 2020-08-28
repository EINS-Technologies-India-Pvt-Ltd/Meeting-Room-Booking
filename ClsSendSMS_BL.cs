using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBDL;
using RBBE;
using System.Data;
namespace RBBL
{
   public class ClsSendSMS_BL
    {
        ClsSendSMS_DL _objSendSms = new ClsSendSMS_DL();

        //To send SMS
        public long insertTO_SendSMS(SendSms _objSms)
        {
            return _objSendSms.insertTO_SendSMS(_objSms);

        }
        public int dtDeleteSendSms(string iRemove)
        {
            return _objSendSms.dtDeleteSendSms(iRemove);
        }
        public DataTable dt_SearchShowAll()
        {
            return _objSendSms.dt_SearchShowAll();
        }
    }
}
