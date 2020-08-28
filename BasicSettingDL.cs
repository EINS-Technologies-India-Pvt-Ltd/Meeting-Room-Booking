using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;
using RBDL;

namespace RBDL
{
    public class BasicSettingDL
    {
        EINS_RBMSEntities DB = new EINS_RBMSEntities();
        public long insertUpdateBasecSetting(BasicSettingBE.BasicBe basicBe)
        {

            var result = DB.BasicSettings.FirstOrDefault(s => s.BasicSettingID !=null);
            if (result != null)
            {
                
                result.CanteenPersonName= basicBe.CanteenPersonName;
                result.CanteenEmailId = basicBe.Canteen_EmailId;
                result.Activate_Moderator = basicBe.Activate_Moderator;
                result.LastModifiedBy = basicBe.LastModifiedBy;
                result.LastModifiedOn = basicBe.LastModifiedOn;
                result.SettingStatus = true;
                DB.SaveChanges();
                return result.BasicSettingID;

            }
            else
            {
                BasicSetting basicSetting = new BasicSetting();

                basicSetting.CanteenPersonName = basicBe.CanteenPersonName;
                basicSetting.CanteenEmailId = basicBe.Canteen_EmailId;
                basicSetting.AddedBy = basicBe.AddedBy;
                basicSetting.AddedOn = basicBe.AddedOn;
                basicSetting.Activate_Moderator = basicBe.Activate_Moderator;
                basicSetting.LastModifiedBy = basicBe.LastModifiedBy;
                basicSetting.LastModifiedOn = basicBe.LastModifiedOn;
                basicSetting.SettingStatus = true;
                DB.AddToBasicSettings(basicSetting);
                DB.SaveChanges();
                return basicSetting.BasicSettingID;
            }
        }

        public List<BasicSettingBE.BasicBe> getBesicSetting(long settingId)
        {
            var result = from s in DB.BasicSettings
                         select new BasicSettingBE.BasicBe
                          {
                              Activate_Moderator = s.Activate_Moderator,
                              CanteenPersonName = s.CanteenPersonName,
                              Canteen_EmailId = s.CanteenEmailId
                          };
            return result.ToList();
        }
    }
}
