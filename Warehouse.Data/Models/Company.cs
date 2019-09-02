using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Data.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Display(Prompt = "Име на компания", Name = "Име на компания")]
        public string Name { get; set; }

        [Display(Prompt = "ЕИК/Id")]
        public string IdentificationCode { get; set; }

        public virtual Contacts Contacts { get; set; }
        public int ContactsId { get; set; }

        [Display(Prompt = "Собственик")]
        public string OwnerName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }
        public virtual ICollection<MeasureUnit> MeasureUnits { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public virtual ICollection<Place> Places { get; set; }
        public virtual ICollection<IncomeExpense> IncomeExpenses { get; set; }
        public virtual ICollection<Order> Payments { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }

        public virtual ApplicationSettings ApplicationSettings { get; set; }
        public int ApplicationSettingsId { get; set; }
    }
}
