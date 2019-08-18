using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Име", Prompt = "Име")]
        public string Name { get; set; }

        [Display(Name = "Баркод", Prompt = "Баркод")]
        public string BaseBarcode { get; set; }

        public virtual MeasureUnit MeasureUnit { get; set; }
        public int MeasureUnitId { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
        public int ProductGroupId { get; set; }

        //TODO Stoyan Lupov 29 June, 2019 
        //        public virtual ICollection<PriceGroup> PriceGroups { get; set; }

        //цена на дребно
        [Column(TypeName = "decimal(18,5)")]
        [Display(Name = "Цена на дребно", Prompt = "Цена на дребно")]
        public decimal RetailPrice { get; set; }

        //цена на едро
        [Column(TypeName = "decimal(18,5)")]
        [Display(Name = "Цена на едро", Prompt = "Цена на едро")]
        public decimal WholesalePrice { get; set; }

        //доставна цена
        [Column(TypeName = "decimal(18,5)")]
        [Display(Name = "Доставна цена", Prompt = "Доставна цена")]
        public decimal DistributorPrice { get; set; }

        [Display(Name = "Минимално количество", Prompt = "Минимално количество")]
        public int MinimalQuantity { get; set; }

        [Display(Name = "Оптимално количество", Prompt = "Оптимално количество")]
        public int OptimalQuantity { get; set; }

        [Display(Name = "Услуга", Prompt = "Услуга")]
        public bool IsService { get; set; }
    }
}
