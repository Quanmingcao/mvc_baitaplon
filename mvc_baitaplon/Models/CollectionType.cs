namespace mvc_baitaplon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CollectionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CollectionType()
        {
            Collections = new HashSet<Collection>();
        }

        [Key]
        public int TypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Collection> Collections { get; set; }
    }
}
