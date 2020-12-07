using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Angular_Sample.Data
{
    public partial class CompanyProfit
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("Company_id")]
        public int CompanyId { get; set; }
        [Column("year")]
        public int Year { get; set; }
        [Column("season")]
        public int Season { get; set; }
        [Column("optGrossMargin", TypeName = "decimal(9, 4)")]
        public decimal? OptGrossMargin { get; set; }
        [Column("optProfitMargin", TypeName = "decimal(9, 4)")]
        public decimal? OptProfitMargin { get; set; }
        [Column("netProfitMargin", TypeName = "decimal(9, 4)")]
        public decimal? NetProfitMargin { get; set; }
        [Column("retOnAssets", TypeName = "decimal(9, 4)")]
        public decimal? RetOnAssets { get; set; }
        [Column("surplus", TypeName = "decimal(9, 4)")]
        public decimal? Surplus { get; set; }
        [Column("netWorth", TypeName = "decimal(9, 4)")]
        public decimal? NetWorth { get; set; }
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

        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("CompanyProfit")]
        public virtual Company Company { get; set; }
    }
}
