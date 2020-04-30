namespace Boxty.Data.Models
{
    using Boxty.Data.Common.Models;
    using Boxty.Services.Mapping;

    public class Category : BaseDeletableModel<int>, ICreatorInfo
    {
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
