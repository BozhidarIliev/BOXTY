using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BoxtyDbContext context;

        public CategoryRepository(BoxtyDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> Categories => null;
    }
}
