using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Warehouse.Data.Models
{
    public class ApplicationSettings
    {
        [Key]
        public int Id { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

        //1 unit => 5.5 BGN
        //2 unit => 5.45 BGN
        //3 unit => 5.445 BGN
        public byte PricesPrecisionUnits { get; set; }

        public bool? PricesIncludeVAT { get; set; }

        //TODO Stoyan Lupov 29 June, 2019 

        //Метод разплащане при ревизия

        //Фирмата е регистрирана по ЗДДС

        //Сървър локация
    }
}
