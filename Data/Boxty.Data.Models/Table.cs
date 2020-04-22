namespace Boxty.Models
{
    using Boxty.Data.Common.Models;
    using Boxty.Services.Mapping;

    public class Table : BaseDeletableModel<int>
    {
        public string Status { get; set; }
    }
}
