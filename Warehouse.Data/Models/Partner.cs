
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Warehouse.Data.Enums;

namespace Warehouse.Data.Models
{
    public class Partner
    {
        public int Id { get; set; }

        [Display(Name = "Име", Prompt = "Име")]
        public string Name { get; set; }

        public PartnerType PartnerType { get; set; }

        public virtual Contacts Contacts { get; set; }
        public int ContactsId { get; set; }

        //TODO Stoyan Lupov 29 June, 2019 Price Group
    }
}
