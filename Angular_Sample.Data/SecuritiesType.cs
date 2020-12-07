using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Angular_Sample.Data
{
    public partial class SecuritiesType
    {
        public SecuritiesType()
        {
            Company = new HashSet<Company>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("typeName")]
        [StringLength(20)]
        public string TypeName { get; set; }
        [Column("typeValue")]
        [StringLength(10)]
        public string TypeValue { get; set; }
        [Column("category")]
        [StringLength(1)]
        public string Category { get; set; }
        [Required]
        [Column("createUser")]
        [StringLength(10)]
        public string CreateUser { get; set; }
        [Column("createDate", TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        [Required]
        [Column("updateUser")]
        [StringLength(10)]
        public string UpdateUser { get; set; }
        [Column("updateDate", TypeName = "datetime")]
        public DateTime UpdateDate { get; set; }

        [InverseProperty("SecuritiesType")]
        public virtual ICollection<Company> Company { get; set; }
    }
}
