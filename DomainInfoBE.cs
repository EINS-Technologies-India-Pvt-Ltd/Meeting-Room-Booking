using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
   public class DomainInfoBE
    {
       public class DomainData
       {
           public int ID { get; set; }
           public string DomainName { get; set; }
           public string EmailId { get; set; }
           public string Password { get; set; }
       }
    }
}
