using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Warehouse.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

        public virtual Place Place { get; set; }
        public int PlaceId { get; set; }

        public string Name { get; set; }

        public string BaseBarcode { get; set; }

        public virtual MeasureUnit MeasureUnit { get; set; }
        public int MeasureUnitId { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
        public int ProductGroupId { get; set; }

        //TODO Stoyan Lupov 29 June, 2019 
        //        public virtual ICollection<PriceGroup> PriceGroups { get; set; }

        //цена на дребно
        [Column(TypeName = "decimal(18,5)")]
        public decimal RetailPrice { get; set; }

        //цена на едро
        [Column(TypeName = "decimal(18,5)")]
        public decimal WholesalePrice { get; set; }

        //доставна цена
        [Column(TypeName = "decimal(18,5)")]
        public decimal DistributorPrice { get; set; }

        public int MinimalQuantity { get; set; }
        public int OptimalQuantity { get; set; }

        public bool IsService { get; set; }
    }
}
