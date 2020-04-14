namespace Boxty.Models
{
    using Boxty.Data.Common.Models;

    public class Table : BaseDeletableModel<int>
    {
        public string Status { get; set; }
    }
}
