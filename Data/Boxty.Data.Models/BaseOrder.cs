namespace Boxty.Models
{
    using Boxty.Data.Common.Models;

    public class BaseOrder : BaseDeletableModel<int>
    {
        public int OrderId { get; set; }

        public Product Product { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }
    }
}
