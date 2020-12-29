namespace Boxty.Web.ViewModels
{
    using Boxty.Data.Models;
    using Boxty.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
