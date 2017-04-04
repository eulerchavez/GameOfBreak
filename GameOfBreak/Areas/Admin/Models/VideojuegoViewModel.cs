using GameOfBreak.Models.GoB;
using System.Collections.Generic;

namespace GameOfBreak.Areas.Admin.Models {

    public class VideojuegoViewModel {

        public string UPC { get; set; }

        public VideoJuego Videojuego { get; set; }

        public Plataforma Plataforma { get; set; }

        public IEnumerable<Plataforma> Plataformas { get; set; }

        public Desarrolladora Desarrolladora { get; set; }

        public IEnumerable<Desarrolladora> Desarrolladoras { get; set; }

        public Clasificacion Clasificacion { get; set; }

        public IEnumerable<Clasificacion> Clasificaciones { get; set; }

        public Genero Genero { get; set; }

        public IEnumerable<Genero> Generos { get; set; }

    }

}