namespace Boxty.Data.Models
{
    using Boxty.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
