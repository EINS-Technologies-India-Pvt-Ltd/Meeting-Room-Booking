using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class ResourceBE
    {
        public class Resource
        {
            public long ResourceID { get; set; }
            public string AssetCode { get; set; }
            public string ResourceName { get; set; }
            public string Brand { get; set; }
            public string ModelNo { get; set; }
            public string SerialNo { get; set; }
            public long? Quantity { get; set; }
            public long? RequireQuantity { get; set; }
            public long? AvailableQuantity { get; set; }
            public long? UsedQuantity { get; set; }
            public string Description { get; set; }
            public byte[] ResourceImage { get; set; }
            public byte[] SpecificationImage { get; set; }
            public string ResourceBoughtFrom { get; set; }
            public DateTime ResourceBoughtOn { get; set; }
            public bool? Transferrable { get; set; }
            public bool? Status { get; set; }
            public long? AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }
    }
}
