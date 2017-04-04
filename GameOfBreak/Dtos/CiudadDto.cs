using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfBreak.Dtos {

    public class CiudadDto {

        [Key]
        public int ID_CIUDAD { get; set; }

        [Column("Ciudad")]
        [Required]
        [StringLength(55)]
        public string Ciudad1 { get; set; }

    }

}