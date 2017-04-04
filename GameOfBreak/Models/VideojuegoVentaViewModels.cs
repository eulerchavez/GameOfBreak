using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameOfBreak.Models {

    public class ProductoVentaViewModel {

        [Key]
        [StringLength(30)]
        public string UPC { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Titulo")]
        public string Nombre { get; set; }

        public Estatus Estatus { get; set; }

        public Desarrolladora Desarrolladora { get; set; }

        public decimal Precio { get; set; }

        public Clasificacion Clasificacion { get; set; }

        public Genero Genero { get; set; }

        [Display(Name = "Fecha de salida")]
        [Column(TypeName = "date")]
        public DateTime FechaSalida { get; set; }

        public string Ruta { get; set; }

    }

    public class VideojuegoDetalleViewModel {

        public VideoJuego Videojuego { get; set; }

        public decimal Precio { get; set; }

        public Rating Rating { get; set; }

        public Plataforma Consola { get; set; }

        public Estatus Estatus { get; set; }

        public IEnumerable<RepositorioMultimedia> Repositorio { get; set; }

    }

    public class AccesorioDetalleViewModel {

        public Accesorio Videojuego { get; set; }

        public decimal Precio { get; set; }

        public Estatus Estatus { get; set; }

        public Plataforma Consola { get; set; }

        public IEnumerable<RepositorioMultimedia> Repositorio { get; set; }

    }

}