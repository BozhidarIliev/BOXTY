namespace Boxty.Models
{
    using Boxty.Common;
    using Boxty.Data.Common.Models;
    using Boxty.Services.Mapping;

    public class Table : BaseModel<int>
    {
        public bool Available { get; set; } = true;
    }
}
