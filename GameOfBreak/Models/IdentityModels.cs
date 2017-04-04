using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace GameOfBreak.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(25)]
        public string PrimerNombre { get; set; }

        [StringLength(50)]
        public string SegundoNombre { get; set; }

        [Required]
        [StringLength(25)]
        public string ApellidoPaterno { get; set; }

        [StringLength(25)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [StringLength(30)]
        public string Calle { get; set; }

        [Required]
        [StringLength(5)]
        public string NumInt { get; set; }

        [StringLength(5)]
        public string NumExt { get; set; }

        public int ID_CP { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaNacimiento { get; set; }

        [StringLength(13)]
        public string TelefonoCasa { get; set; }

        public decimal Saldo { get; set; }

        public DateTime FechaAlta { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("GameOfBreakConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }

}