namespace Boxty.Models
{
    using Boxty.Data.Common.Models;

    public class Table : BaseModel<int>, ICreatorInfo
    {
        public bool Available { get; set; } = true;

        public int Seats { get; set; }
        
        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }
    }
}
