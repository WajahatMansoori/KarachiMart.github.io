//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.OrderTables = new HashSet<OrderTable>();
        }
    
        public int Cus_Id { get; set; }

        [Required(ErrorMessage ="This Feild is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This Feild is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This Feild is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This Feild is Required")]
        public string Mobile { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTable> OrderTables { get; set; }
    }
}
