namespace Boxty.Data.Models
{
    using Boxty.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        public string Name { get; set; }
    }
}
