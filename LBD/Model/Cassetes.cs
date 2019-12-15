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
    
    public partial class Cassetes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cassetes()
        {
            this.Cassete_Copies = new HashSet<Cassete_Copies>();
            this.Film_Groups = new HashSet<Film_Groups>();
        }
    
        public string Title { get; set; }
        public int Catalog_Id { get; set; }
        public byte[] Cover { get; set; }
        public Nullable<double> Price { get; set; }
        public string Director { get; set; }
        public Nullable<int> Genere_Id { get; set; }
        public Nullable<int> Departament_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cassete_Copies> Cassete_Copies { get; set; }
        public virtual Generes Generes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Film_Groups> Film_Groups { get; set; }
    }
}