namespace Boxty.Web.ViewModels
{

    using AutoMapper;
    using Boxty.Data.Models;
    using Boxty.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Image.RemoteImageUrl != null ?
                        x.Image.RemoteImageUrl :
                        "/images/products/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
