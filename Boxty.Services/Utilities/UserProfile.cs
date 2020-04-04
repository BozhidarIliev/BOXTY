using AutoMapper;
using Boxty.Models;
using Boxty.ViewModels.OutputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Services.Utilities
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<BoxtyUser,UserOutputModel>();
        }
    }
}
