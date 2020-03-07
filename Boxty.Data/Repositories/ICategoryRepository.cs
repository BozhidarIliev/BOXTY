using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Data.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
