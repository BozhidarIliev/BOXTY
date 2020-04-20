namespace Boxty.Web.ViewModels
{
    using System;

    using Boxty.Data.Models;

    public class OrderOutputModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Comment { get; set; }

        public Product Product { get; set; }
    }
}
