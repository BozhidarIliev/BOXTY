using Boxty.Data.Common.Repositories;
using Boxty.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Boxty.Services.Data.Tests
{
    public class CategoryServiceTests
    {
        [Fact]
        public async Task CreateCategory()
        {
            var list = new List<Category>();

            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback(
                (Category category) => list.Add(category));

            var service = new CategoryService(mockRepo.Object);

            await service.CreateCategory("category");

            // act - what is tested

            // assert
            Assert.Equal("category", list[0].Name); // ochakvaniq, realnost
        }

        [Fact]
        public async Task UpdateCategory()
        {
            var list = new List<Category>();
            list.Add(new Category { Id = 1, Name = "category" });

            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.Update(It.IsAny<Category>())).Callback(
                (Category category) => list[0].Name = category.Name);

            var service = new CategoryService(mockRepo.Object);

            await service.UpdateCategory(new Category {Id = 1, Name = "category1" });

            Assert.Equal("category1", list[0].Name);
        }

        [Fact]
        public async Task DeleteCategory()
        {
            var list = new List<Category>();
            list.Add(new Category { Id = 1, Name = "category" });

            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();

            mockRepo.Setup(x => x.Delete(It.IsAny<Category>())).Callback(
                (Category category) => list.Remove(list.FirstOrDefault(x => x.Id == category.Id)));

            var service = new CategoryService(mockRepo.Object);

            await service.DeleteCategory(1);

            Assert.Equal(0, list.Count);
        }
    }
}
