//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccesLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_Details()
        {
            this.Page_Details = new HashSet<Page_Details>();
        }
    
        public string userId { get; set; }
        public string password { get; set; }
        public string user_first_name { get; set; }
        public string user_last_name { get; set; }
        public Nullable<bool> isadmin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Page_Details> Page_Details { get; set; }
    }
}