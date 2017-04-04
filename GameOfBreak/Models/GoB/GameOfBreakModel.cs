namespace GameOfBreak.Models.GoB {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;

    public partial class GameOfBreakModel : DbContext {
        public GameOfBreakModel ()
            : base("name=Gob2") {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Accesorio> Accesorio { get; set; }
        public virtual DbSet<Almacen> Almacen { get; set; }
        public virtual DbSet<Apartado> Apartado { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<CarritoTemporal> CarritoTemporal { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Clasificacion> Clasificacion { get; set; }
        public virtual DbSet<CodigoPostal> CodigoPostal { get; set; }
        public virtual DbSet<Colonia> Colonia { get; set; }
        public virtual DbSet<Desarrolladora> Desarrolladora { get; set; }
        public virtual DbSet<Descuento> Descuento { get; set; }
        public virtual DbSet<DetalleApartado> DetalleApartado { get; set; }
        public virtual DbSet<DetalleCarrito> DetalleCarrito { get; set; }
        public virtual DbSet<DetalleCarritoTemporal> DetalleCarritoTemporal { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Estatus> Estatus { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<MunicipioDelegacion> MunicipioDelegacion { get; set; }
        public virtual DbSet<Plataforma> Plataforma { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<RelProductoPlataforma> RelProductoPlataforma { get; set; }
        public virtual DbSet<RelTiendaEmpleado> RelTiendaEmpleado { get; set; }
        public virtual DbSet<RepositorioMultimedia> RepositorioMultimedia { get; set; }
        public virtual DbSet<Tienda> Tienda { get; set; }
        public virtual DbSet<TipoMultimedia> TipoMultimedia { get; set; }
        public virtual DbSet<TipoPago> TipoPago { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<VideoJuego> VideoJuego { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Ventas> Ventas { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<DetallesVentas> DetallesVentas { get; set; }

        protected override void OnModelCreating (DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Accesorio>()
                .Property(e => e.UPC)
                .IsUnicode(false);

            modelBuilder.Entity<Accesorio>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Accesorio>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Almacen>()
                .Property(e => e.UPC)
                .IsUnicode(false);

            modelBuilder.Entity<Almacen>()
                .Property(e => e.Precio)
                .HasPrecision(8, 2);

            //modelBuilder.Entity<Apartado>()
            //    .HasMany(e => e.DetalleApartado)
            //    .WithRequired(e => e.Apartado)
            //    .WillCascadeOnDelete(false);
            /*
            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Apartado)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.ID_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Apartado1)
                .WithRequired(e => e.AspNetUsers1)
                .HasForeignKey(e => e.ID_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Carrito)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.ID_USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.RelTiendaEmpleado)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.ID_EMPLEADO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Venta)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.ID_EMPLEADO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Venta1)
                .WithRequired(e => e.AspNetUsers1)
                .HasForeignKey(e => e.ID_USUARIO)
                .WillCascadeOnDelete(false);
            */

            //modelBuilder.Entity<Carrito>()
            //    .HasMany(e => e.DetalleCarrito)
            //    .WithRequired(e => e.Carrito)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ciudad>()
                .Property(e => e.Ciudad1)
                .IsUnicode(false);

            //modelBuilder.Entity<Ciudad>()
            //    .HasMany(e => e.CodigoPostal)
            //    .WithRequired(e => e.Ciudad)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clasificacion>()
                .Property(e => e.Clasificacion1)
                .IsUnicode(false);

            modelBuilder.Entity<CodigoPostal>()
                .Property(e => e.CP)
                .IsUnicode(false);

            //modelBuilder.Entity<CodigoPostal>()
            //    .HasMany(e => e.AspNetUsers)
            //    .WithRequired(e => e.CodigoPostal)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CodigoPostal>()
            //    .HasMany(e => e.Tienda)
            //    .WithRequired(e => e.CodigoPostal)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Colonia>()
                .Property(e => e.Colonia1)
                .IsUnicode(false);

            //modelBuilder.Entity<Colonia>()
            //    .HasMany(e => e.CodigoPostal)
            //    .WithRequired(e => e.Colonia)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Desarrolladora>()
                .Property(e => e.Desarrolladora1)
                .IsUnicode(false);

            //modelBuilder.Entity<Desarrolladora>()
            //    .HasMany(e => e.VideoJuego)
            //    .WithRequired(e => e.Desarrolladora)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Descuento>()
                .Property(e => e.Descuento1)
                .IsUnicode(false);

            modelBuilder.Entity<Descuento>()
                .Property(e => e.MontoDescuento)
                .HasPrecision(8, 2);

            modelBuilder.Entity<DetalleApartado>()
                .Property(e => e.UPC)
                .IsUnicode(false);

            modelBuilder.Entity<DetalleApartado>()
                .Property(e => e.Monto)
                .HasPrecision(8, 2);

            modelBuilder.Entity<DetalleApartado>()
                .Property(e => e.PagoInicial)
                .HasPrecision(8, 2);

            modelBuilder.Entity<DetalleCarrito>()
                .Property(e => e.UPC)
                .IsUnicode(false);

            modelBuilder.Entity<DetalleCarrito>()
                .Property(e => e.Monto)
                .HasPrecision(8, 2);

            modelBuilder.Entity<DetalleVenta>()
                .Property(e => e.UPC)
                .IsUnicode(false);

            modelBuilder.Entity<DetalleVenta>()
                .Property(e => e.Monto)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Estado>()
                .Property(e => e.Estado1)
                .IsUnicode(false);

            //modelBuilder.Entity<Estado>()
            //    .HasMany(e => e.CodigoPostal)
            //    .WithRequired(e => e.Estado)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estatus>()
                .Property(e => e.Estatus1)
                .IsUnicode(false);
            
            /*
            modelBuilder.Entity<Estatus>()
                .HasMany(e => e.Almacen)
                .WithRequired(e => e.Estatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genero>()
                .Property(e => e.Genero1)
                .IsUnicode(false);
            */

            //modelBuilder.Entity<MunicipioDelegacion>()
            //    .Property(e => e.MunicipioDelegacion1)
            //    .IsUnicode(false);

            //modelBuilder.Entity<MunicipioDelegacion>()
            //    .HasMany(e => e.CodigoPostal)
            //    .WithRequired(e => e.MunicipioDelegacion)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Plataforma>()
                .Property(e => e.Plataforma1)
                .IsUnicode(false);

            //modelBuilder.Entity<Plataforma>()
            //    .HasMany(e => e.RelProductoPlataforma)
            //    .WithRequired(e => e.Plataforma)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
                .Property(e => e.UPC)
                .IsUnicode(false);

            modelBuilder.Entity<Rating>()
                .Property(e => e.Estrellas)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Rating>()
                .Property(e => e.Promedio)
                .HasPrecision(5, 2);

            modelBuilder.Entity<RelProductoPlataforma>()
                .Property(e => e.UPC)
                .IsUnicode(false);

            modelBuilder.Entity<RepositorioMultimedia>()
                .Property(e => e.UPC)
                .IsUnicode(false);

            modelBuilder.Entity<RepositorioMultimedia>()
                .Property(e => e.Ruta)
                .IsUnicode(false);

            modelBuilder.Entity<Tienda>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Tienda>()
                .Property(e => e.Calle)
                .IsUnicode(false);

            modelBuilder.Entity<Tienda>()
                .Property(e => e.NumInt)
                .IsUnicode(false);

            modelBuilder.Entity<Tienda>()
                .Property(e => e.NumExt)
                .IsUnicode(false);
            /*
            modelBuilder.Entity<Tienda>()
                .HasMany(e => e.Almacen)
                .WithRequired(e => e.Tienda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tienda>()
                .HasMany(e => e.Apartado)
                .WithRequired(e => e.Tienda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tienda>()
                .HasMany(e => e.Carrito)
                .WithRequired(e => e.Tienda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tienda>()
                .HasMany(e => e.RelTiendaEmpleado)
                .WithRequired(e => e.Tienda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tienda>()
                .HasMany(e => e.Venta)
                .WithRequired(e => e.Tienda)
                .WillCascadeOnDelete(false);
            */

            modelBuilder.Entity<TipoMultimedia>()
                .Property(e => e.Multimedia)
                .IsUnicode(false);

            //modelBuilder.Entity<TipoMultimedia>()
            //    .HasMany(e => e.RepositorioMultimedia)
            //    .WithRequired(e => e.TipoMultimedia)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoPago>()
                .Property(e => e.Pago)
                .IsUnicode(false);

            modelBuilder.Entity<TipoPago>()
                .HasMany(e => e.Apartado)
                .WithRequired(e => e.TipoPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoPago>()
                .HasMany(e => e.Carrito)
                .WithRequired(e => e.TipoPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoPago>()
                .HasMany(e => e.Venta)
                .WithRequired(e => e.TipoPago)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Venta>()
            //    .HasMany(e => e.DetalleVenta)
            //    .WithRequired(e => e.Venta)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<VideoJuego>()
                .Property(e => e.UPC)
                .IsUnicode(false);

            modelBuilder.Entity<VideoJuego>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<VideoJuego>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            //modelBuilder.Entity<VideoJuego>()
            //    .HasMany(e => e.Rating)
            //    .WithRequired(e => e.VideoJuego)
            //    .WillCascadeOnDelete(false);

        }

    }

}
