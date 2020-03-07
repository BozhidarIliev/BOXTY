using AutoMapper;
using Boxty.Models;
using Boxty.ViewModels.OutputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Services
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderOutputModel, Order>();
        }
    }
}
