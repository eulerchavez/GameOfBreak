using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfBreak.Dtos {

    public class EstadoDto {

        [Key]
        public int ID_ESTADO { get; set; }

        [Column("Estado")]
        [Required]
        [StringLength(45)]
        public string Estado1 { get; set; }

    }

}