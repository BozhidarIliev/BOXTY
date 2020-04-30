namespace Boxty.Models
{
    using Boxty.Common;
    using Boxty.Data.Common.Models;
    using Boxty.Services.Mapping;

    public class Table : BaseModel<int>, ICreatorInfo
    {
        public bool Available { get; set; } = true;

        public int Seats { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
