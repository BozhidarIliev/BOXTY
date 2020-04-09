using AutoMapper;
using Boxty.Models;
using Boxty.ViewModels;
using Boxty.ViewModels.InputModels;
using Boxty.ViewModels.OutputModels;
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
            CreateMap<BoxtyUser, UserOutputModel>();
            CreateMap<OrderOutputModel, Order>();
            CreateMap<BaseOrder, ShoppingCartViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(x => x.Product.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(x => x.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(x => x.Product.Price))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(x => x.Comment));
            CreateMap<BaseOrder, OrderOutputModel>()
                 .ForMember(dest => dest.Date, opt => opt.MapFrom(x => x.SentOn))
                 .ForMember(dest => dest.Comment, opt => opt.MapFrom(x => x.Comment));
            CreateMap<Product, BaseOrder>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(x => x));
            CreateMap<BaseOrder, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Product.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(x=>x.Product.Price));
        }
    }
}
