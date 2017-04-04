using AutoMapper;
using GameOfBreak.Areas.Admin.Models;
using GameOfBreak.Dtos;
using GameOfBreak.Models;
using GameOfBreak.Models.GoB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfBreak.App_Start {
    public class MappingConfig : Profile {

        public MappingConfig () {

            CreateMap<AspNetUsers, Usuario>();
            CreateMap<Usuario, AspNetUsers>();

            CreateMap<ApplicationUser, Usuario>();
            CreateMap<Usuario, ApplicationUser>();

            CreateMap<ApplicationUser, AspNetUsers>();
            CreateMap<AspNetUsers, ApplicationUser>();

        }

    }

}