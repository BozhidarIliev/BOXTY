namespace Boxty.Models
{
    using Boxty.Common;
    using Boxty.Data.Common.Models;
    using Boxty.Services.Mapping;

    public class Table : BaseModel<int>
    {
        public string Status { get; set; } = GlobalConstants.TableAvailable;
    }
}
