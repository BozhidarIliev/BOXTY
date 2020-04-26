namespace Boxty.Web.ViewModels
{

    using Boxty.Models;
    using Boxty.Services.Mapping;

    public class TableViewModel : IMapFrom<Table>
    {
        public int Id { get; set; }
        public bool Available { get; set; }

    }
}
