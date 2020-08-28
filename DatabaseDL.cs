using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;

namespace RBDL
{
    public class DatabaseDL
    {
        EINS_RBMSEntities objEntity = new EINS_RBMSEntities();

        public int insertUpdateEmpGeneralDetails(DatabaseBE _objDatabase)
        {
            int returnvalue = 0;
            try
            {
                if (_objDatabase.TaskID != 0)
                {
                    var objDb = objEntity.DatabaseBackups.FirstOrDefault(z => z.TaskID == _objDatabase.TaskID);
                    objDb.ScheduleType = _objDatabase.ScheduleType;
                    objDb.Monday = _objDatabase.IsMonday;
                    objDb.Tuesday = _objDatabase.IsTuesday;
                    objDb.Wednesday = _objDatabase.IsWednesday;
                    objDb.Thursday = _objDatabase.IsThursday;
                    objDb.Friday = _objDatabase.IsFriday;
                    objDb.Saturday = _objDatabase.IsSaturday;
                    objDb.Sunday = _objDatabase.IsSunday;
                    objDb.TaskName = _objDatabase.TaskName;
                    objDb.Overright = _objDatabase.IsOverride;
                    objDb.StartTime = _objDatabase.TimeValue;
                    objDb.FolderName = _objDatabase.ServerPath;
                    objDb.LastModifiedOn = DateTime.Now;
                    objDb.Status = true;
                    objDb.LastModifiedBy = _objDatabase.LastModifiedBy;
                    objEntity.SaveChanges();
                    returnvalue = objDb.TaskID;
                }
                else
                {
                    DatabaseBackup objDb = new DatabaseBackup();
                    objDb.TaskID = _objDatabase.TaskID;
                    objDb.ScheduleType = _objDatabase.ScheduleType;
                    objDb.Monday = _objDatabase.IsMonday;
                    objDb.Tuesday = _objDatabase.IsTuesday;
                    objDb.Wednesday = _objDatabase.IsWednesday;
                    objDb.Thursday = _objDatabase.IsThursday;
                    objDb.Friday = _objDatabase.IsFriday;
                    objDb.Saturday = _objDatabase.IsSaturday;
                    objDb.Sunday = _objDatabase.IsSunday;
                    objDb.TaskName = _objDatabase.TaskName;
                    objDb.Overright = _objDatabase.IsOverride;
                    objDb.StartTime = _objDatabase.TimeValue;
                    objDb.FolderName = _objDatabase.ServerPath;
                    objDb.AddedOn = DateTime.Now;
                    objDb.AddedBy = _objDatabase.AddedBy;
                    objDb.Status = true;
                    objEntity.AddToDatabaseBackups(objDb);
                    objEntity.SaveChanges();
                    returnvalue = objDb.TaskID;
                }


                return returnvalue;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<DatabaseBE> getDataBaseBackupsData()
        {
            var result = from s in objEntity.DatabaseBackups
                         where s.Status == true
                         select new DatabaseBE
                         {
                             TaskID = s.TaskID,
                             TaskName = s.TaskName,
                             TimeValue = s.StartTime.Value,
                             ServerPath = s.FolderName,
                             strScheduleType = s.ScheduleType.Value?"Daily":"Weekly",
                             IsOverride = s.Overright.Value,
                             IsMonday = s.Monday.Value,
                             IsTuesday = s.Tuesday.Value,
                             IsWednesday = s.Wednesday.Value,
                             IsThursday = s.Thursday.Value,
                             IsFriday = s.Friday.Value,
                             IsSaturday = s.Saturday.Value,
                             IsSunday = s.Sunday.Value,
                             LastModifiedBy = s.LastModifiedBy.Value,
                             AddedBy = s.AddedBy.Value,

                         };
            return result.ToList();
        }
        public int deleteDataBaseData(int taskid)
        {
            var result = objEntity.DatabaseBackups.FirstOrDefault(s => s.TaskID == taskid);
            result.Status = false;
            objEntity.SaveChanges();
            return result.TaskID;
        }
    }
}
