using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Warehouse.Data.Models
{
    public class Contacts
    {
        public int Id { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

        [Display(Prompt = "Държава")]
        public string Country { get; set; }

        [Display(Prompt = "Град")]
        public string City { get; set; }

        [Display(Prompt = "Адрес")]
        public string Address { get; set; }

        [Display(Prompt = "Телефон")]
        public string Phone { get; set; }

        [Display(Prompt = "Email")]
        public string Email { get; set; }
    }
}
