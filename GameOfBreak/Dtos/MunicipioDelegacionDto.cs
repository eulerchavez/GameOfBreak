using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfBreak.Dtos {

    public class MunicipioDelegacionDto {

        [Key]
        public int ID_MUNICIPIO_DELEGACION { get; set; }

        [Column("MunicipioDelegacion")]
        [Required]
        [StringLength(55)]
        public string MunicipioDelegacion1 { get; set; }

    }

}