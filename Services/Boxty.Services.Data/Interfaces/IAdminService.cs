namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;

    using Boxty.ViewModels.OutputModels;

    public interface IAdminService
    {
        public IEnumerable<UserOutputModel> FilterRoles(string filter, IEnumerable<UserOutputModel> result);

        IEnumerable<UserOutputModel> AllUsers(string type);
    }
}
