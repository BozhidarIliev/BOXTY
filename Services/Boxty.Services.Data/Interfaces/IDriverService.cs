using Boxty.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Services.Data.Interfaces
{
    public interface IDriverService
    {

        List<OrderDriverViewModel> GetCurrentOrderItems();
    }
}
