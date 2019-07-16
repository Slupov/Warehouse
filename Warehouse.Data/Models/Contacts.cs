using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Data.Models
{
    public class Contacts
    {
        public int Id { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
