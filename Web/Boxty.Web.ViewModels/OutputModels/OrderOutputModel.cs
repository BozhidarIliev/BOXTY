namespace Boxty.Web.ViewModels
{
    using System;

    using Boxty.Models;

    public class OrderOutputModel
    {
        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Comment { get; set; }

        public Product Product { get; set; }
    }
}
