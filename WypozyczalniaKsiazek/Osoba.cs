//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WypozyczalniaKsiazek
{
    using System;
    using System.Collections.Generic;
    
    public partial class Osoba
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Osoba()
        {
            this.Ksiazki = new HashSet<Ksiazka>();
        }
    
        public int PESEL { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string stanowisko { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ksiazka> Ksiazki { get; set; }
    }
}
