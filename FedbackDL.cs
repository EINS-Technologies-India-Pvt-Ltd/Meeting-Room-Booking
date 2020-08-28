using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;
using RBDL;

namespace RBDL
{
    public class FedbackDL
    {
        EINS_RBMSEntities DB = new EINS_RBMSEntities();
        public long saveFeedbackData(FeedbackBE.FeedbackSaveBE feedbackBE)
        {

            var result = DB.feedbacks.FirstOrDefault(s => s.userid == feedbackBE.userid && s.RoomId==feedbackBE.RoomId);
            if (result != null)
            {

                result.Seating_Arrangement = feedbackBE.Seating_Arrangement;
                result.comment = feedbackBE.comment;
                result.Canteen_Facility = feedbackBE.Canteen_Facility;
                result.question = feedbackBE.question;
                result.RoomId = feedbackBE.RoomId;
                result.MeetingDate = feedbackBE.MeetingDate;
                result.Meeting_Your_Needs = feedbackBE.Meeting_Your_Needs;
                result.IT_Facility = feedbackBE.IT_Facility;
                result.LastModifiedBy = feedbackBE.LastModifiedBy;
                result.LastModifiedOn = feedbackBE.LastModifiedOn;
                result.Room_Environment = feedbackBE.Room_Environment;
                DB.SaveChanges();
                return result.FeedbackID;
            }
            else
            {
                feedback fdbk = new feedback();
                fdbk.userid = feedbackBE.userid;
                fdbk.Seating_Arrangement = feedbackBE.Seating_Arrangement;
                fdbk.AddedBy = feedbackBE.AddedBy;
                fdbk.AddedOn = feedbackBE.AddedOn;
                fdbk.comment = feedbackBE.comment;
                fdbk.Canteen_Facility = feedbackBE.Canteen_Facility;
                fdbk.question = feedbackBE.question;
                fdbk.RoomId = feedbackBE.RoomId;
                fdbk.MeetingDate = feedbackBE.MeetingDate;
                fdbk.Meeting_Your_Needs = feedbackBE.Meeting_Your_Needs;
                fdbk.IT_Facility = feedbackBE.IT_Facility;
                fdbk.LastModifiedBy = feedbackBE.LastModifiedBy;
                fdbk.LastModifiedOn = feedbackBE.LastModifiedOn;
                fdbk.Room_Environment = feedbackBE.Room_Environment;
                DB.AddTofeedbacks(fdbk);
                DB.SaveChanges();
                return fdbk.FeedbackID;
            }


        }
        public List<FeedbackBE.feedbackReportBE> lstfeedbackData()
        {
            var result = from f in DB.feedbacks
                         join u in DB.UserMasters on f.userid equals u.ID
                         join r in DB.RoomMasters on f.RoomId.Value equals r.RoomId
                         join c in DB.CompanyMasters on u.Company equals c.CompanyID
                         join l in DB.LocationMasters on u.Location equals l.LocationId
                         select new FeedbackBE.feedbackReportBE
                         {
                             FeedbackID = f.FeedbackID,
                             userName = u.Name,
                             MobileNo = u.Mobile,
                             Email = u.EmailID,
                             RoomName = r.Name,
                             MeetingDate = f.MeetingDate,
                             UserId = f.userid.Value,
                             CompanyName = c.CompanyName,
                             LocationName = l.LocationName,
                             Meeting_Your_Needs = f.Meeting_Your_Needs,
                             question = f.question,
                             Room_Environment = f.Room_Environment,
                             Seating_Arrangement = f.Seating_Arrangement,
                             IT_Facility = f.IT_Facility,
                             Canteen_Facility = f.Canteen_Facility,
                             UserType = u.UserType

                         };
            return result.ToList();
        }
        public List<FeedbackBE.feedbackReportBE> lstfeedbackDataByCompany(long companyID, long locationId)
        {
            var result = from f in DB.feedbacks
                         join u in DB.UserMasters on f.userid equals u.ID
                         join r in DB.RoomMasters on f.RoomId.Value equals r.RoomId
                         join c in DB.CompanyMasters on u.Company equals c.CompanyID
                         join l in DB.LocationMasters on u.Location equals l.LocationId
                         where u.Company.Value == companyID && u.Location.Value == locationId
                         select new FeedbackBE.feedbackReportBE
                            {
                                FeedbackID = f.FeedbackID,
                                userName = u.Name,
                                MobileNo = u.Mobile,
                                Email = u.EmailID,
                                RoomName = r.Name,
                                MeetingDate = f.MeetingDate,
                                UserId = f.userid.Value,
                                CompanyName = c.CompanyName,
                                LocationName=l.LocationName,
                                Meeting_Your_Needs=f.Meeting_Your_Needs,
                                question=f.question,
                                Room_Environment=f.Room_Environment,
                                Seating_Arrangement=f.Seating_Arrangement,
                                IT_Facility=f.IT_Facility,
                                Canteen_Facility=f.Canteen_Facility,
                                UserType=u.UserType
                            };
            return result.ToList();
        }
        public List<FeedbackBE.feedbackReportBE> lstfeedbackDataByEmployee(long userId)
        {
            var result = from f in DB.feedbacks
                         join u in DB.UserMasters on f.userid equals u.ID
                         join r in DB.RoomMasters on f.RoomId.Value equals r.RoomId
                         join c in DB.CompanyMasters on u.Company equals c.CompanyID
                         join l in DB.LocationMasters on u.Location equals l.LocationId
                         where u.ID == userId
                         select new FeedbackBE.feedbackReportBE
                          {
                              FeedbackID = f.FeedbackID,
                              userName = u.Name,
                              MobileNo = u.Mobile,
                              Email = u.EmailID,
                              RoomName = r.Name,
                              MeetingDate = f.MeetingDate,
                              UserId = f.userid.Value,
                              CompanyName = c.CompanyName,
                              LocationName = l.LocationName,
                              Meeting_Your_Needs = f.Meeting_Your_Needs,
                              question = f.question,
                              Room_Environment = f.Room_Environment,
                              Seating_Arrangement = f.Seating_Arrangement,
                              IT_Facility = f.IT_Facility,
                              Canteen_Facility = f.Canteen_Facility,
                              UserType = u.UserType
                          };
            return result.ToList();
        }
        public List<FeedbackBE.feedbackReportBE> lstfeedbackDataByusertype(string usertype)
        {
            var result = from f in DB.feedbacks
                         join u in DB.UserMasters     on f.userid       equals u.ID              
                         join r in DB.RoomMasters     on f.RoomId.Value equals r.RoomId             
                         join c in DB.CompanyMasters  on u.Company      equals c.CompanyID      
                         join l in DB.LocationMasters on u.Location     equals l.LocationId     
                         where u.UserType == usertype                                           
                         select new FeedbackBE.feedbackReportBE                                 
                         {
                             FeedbackID = f.FeedbackID,
                             userName = u.Name,
                             MobileNo = u.Mobile,
                             Email = u.EmailID,
                             RoomName = r.Name,
                             MeetingDate = f.MeetingDate,
                             UserId = f.userid.Value,
                             CompanyName = c.CompanyName,
                             LocationName = l.LocationName,
                             Meeting_Your_Needs = f.Meeting_Your_Needs,
                             question = f.question,
                             Room_Environment = f.Room_Environment,
                             Seating_Arrangement = f.Seating_Arrangement,
                             IT_Facility = f.IT_Facility,
                             Canteen_Facility = f.Canteen_Facility,
                             UserType = u.UserType
                         };

            return result.ToList();
        }
        public List<FeedbackBE.feedbackReportBE> lstfeedbackDataByDate(DateTime fromdate, DateTime todate)
        {
            var result = from f in DB.feedbacks
                         join u in DB.UserMasters on f.userid equals u.ID
                         join r in DB.RoomMasters on f.RoomId.Value equals r.RoomId
                         join c in DB.CompanyMasters on u.Company equals c.CompanyID
                         join l in DB.LocationMasters on u.Location equals l.LocationId
                         where f.MeetingDate >= fromdate && f.MeetingDate <= todate
                         select new FeedbackBE.feedbackReportBE
                         {
                             FeedbackID = f.FeedbackID,
                             userName = u.Name,
                             MobileNo = u.Mobile,
                             Email = u.EmailID,
                             RoomName = r.Name,
                             MeetingDate = f.MeetingDate,
                             UserId = f.userid.Value,
                             CompanyName = c.CompanyName,
                             LocationName = l.LocationName,
                             Meeting_Your_Needs = f.Meeting_Your_Needs,
                             question = f.question,
                             Room_Environment = f.Room_Environment,
                             Seating_Arrangement = f.Seating_Arrangement,
                             IT_Facility = f.IT_Facility,
                             Canteen_Facility = f.Canteen_Facility,
                             UserType = u.UserType
                         };
            return result.ToList();
        }
        public List<FeedbackBE.feedbackReportCRBE> lstfeedbackDataReport(long feedbackId)
        {
            var result = from f in DB.feedbacks
                         join u in DB.UserMasters on f.userid equals u.ID
                         join r in DB.RoomMasters on f.RoomId.Value equals r.RoomId
                         join c in DB.CompanyMasters on u.Company equals c.CompanyID
                         join l in DB.LocationMasters on u.Location equals l.LocationId
                         where f.FeedbackID == feedbackId
                         select new FeedbackBE.feedbackReportCRBE
                         {
                             FeedbackID = f.FeedbackID,
                             UserId = f.userid.Value,
                             userName = u.Name,
                             MobileNo = u.Mobile,
                             Email = u.EmailID,
                             RoomName = r.Name,
                             MeetingDate = f.MeetingDate.Value,
                             CompanyName = c.CompanyName,
                             LocationName = l.LocationName,
                             Meeting_Your_Needs = f.Meeting_Your_Needs,
                             question = f.question,
                             Room_Environment = f.Room_Environment,
                             Seating_Arrangement = f.Seating_Arrangement,
                             IT_Facility = f.IT_Facility,
                             Canteen_Facility = f.Canteen_Facility,
                             UserType = u.UserType
                         };
            return result.ToList();
        }

      
    }
}
