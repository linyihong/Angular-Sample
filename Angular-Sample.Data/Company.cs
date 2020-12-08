using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Angular_Sample.Data
{
    public partial class Company
    {
        public Company()
        {
            CompanyProfit = new HashSet<CompanyProfit>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("SecuritiesType_id")]
        public int SecuritiesTypeId { get; set; }
        [Required]
        [Column("comValue")]
        [StringLength(10)]
        public string ComValue { get; set; }
        [Required]
        [Column("chName")]
        [StringLength(30)]
        public string ChName { get; set; }
        [Column("chAbbreviation")]
        [StringLength(20)]
        public string ChAbbreviation { get; set; }
        [Column("engName")]
        [StringLength(60)]
        public string EngName { get; set; }
        [Column("engAbbreviation")]
        [StringLength(20)]
        public string EngAbbreviation { get; set; }
        [Column("address")]
        [StringLength(100)]
        public string Address { get; set; }
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

        [ForeignKey(nameof(SecuritiesTypeId))]
        [InverseProperty("Company")]
        public virtual SecuritiesType SecuritiesType { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<CompanyProfit> CompanyProfit { get; set; }
    }
}
