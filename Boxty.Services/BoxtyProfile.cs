using AutoMapper;
using Boxty.Models;
using Boxty.ViewModels.InputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Services
{
    public class BoxtyProfile : Profile
    {
        public BoxtyProfile()
        {
            CreateMap<RegisterInputModel, BoxtyUser>();
        }
    }
}
