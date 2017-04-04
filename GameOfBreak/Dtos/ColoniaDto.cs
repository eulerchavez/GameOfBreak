using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameOfBreak.Dtos {

    public class ColoniaDto {

        [Key]
        public int ID_COLONIA { get; set; }

        [Column("Colonia")]
        [StringLength(150)]
        public string Colonia1 { get; set; }

    }

}