using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;
using RBDL;

namespace RBDL
{
    public class DomainInfoDL
    {
        EINS_RBMSEntities DB = new EINS_RBMSEntities();
        List<DomainInfoBE.DomainData> lstDomainData = new List<DomainInfoBE.DomainData>();
        public List<DomainInfoBE.DomainData> getDomainData()
        {
            var result = from s in DB.DomainInformmations
                         select new DomainInfoBE.DomainData
              {
                  ID = s.Id,
                  DomainName = s.DomainName,
                  EmailId = s.EmailId,
                  Password = s.Password
              };
            return result.ToList();
        }
        public long UpdateDomainData(DomainInfoBE.DomainData BE)
        {
            var result = DB.DomainInformmations.FirstOrDefault();
            result.DomainName = BE.DomainName;
            result.EmailId = BE.EmailId;
            result.Password = BE.Password;
            DB.SaveChanges();
            return result.Id;
        }
        public long InsertDomainData(DomainInfoBE.DomainData BE)
        {
            DomainInformmation TableDomain = new DomainInformmation();

            TableDomain.DomainName = BE.DomainName;
            TableDomain.EmailId = BE.EmailId;
            TableDomain.Password = BE.Password;

            DB.AddToDomainInformmations(TableDomain);
            DB.SaveChanges();
            return TableDomain.Id;
        }
    }
}
