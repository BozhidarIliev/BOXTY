using Boxty.ViewModels.OutputModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boxty.Services.Interfaces
{
    public interface IAdminService
    {
        public IEnumerable<UserOutputModel> FilterRoles(string filter, IEnumerable<UserOutputModel> result);
        IEnumerable<UserOutputModel> AllUsers(string type);
    }
}
