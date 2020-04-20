namespace Boxty.Data.Common.Models
{
    public interface ICreatorInfo
    {
        string CreatedBy { get; set; }

        string ModifiedBy { get; set; }
    }
}
