//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LBD.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cassete_Copies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cassete_Copies()
        {
            this.Cassete_Rentals = new HashSet<Cassete_Rentals>();
        }
    
        public int Copy_Id { get; set; }
        public int Catalog_Id { get; set; }
        public string Status { get; set; }
    
        public virtual Cassetes Cassetes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cassete_Rentals> Cassete_Rentals { get; set; }
    }
}
