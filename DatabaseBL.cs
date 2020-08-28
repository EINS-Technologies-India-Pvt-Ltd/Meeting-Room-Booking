using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBDL;
using RBBE;

namespace RBBL
{
   public class DatabaseBL
    {
        DatabaseDL _objDL = new DatabaseDL();

        public int insertUpdateEmpGeneralDetails(DatabaseBE _objDatabase)
        {
            return _objDL.insertUpdateEmpGeneralDetails(_objDatabase);
        }
    }
}
