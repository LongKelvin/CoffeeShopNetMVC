using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;
        private List<PostCategory> listPostCategories;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postCategoryService = new PostCategoryService(
                _mockUnitOfWork.Object,
                _mockRepository.Object);

            listPostCategories = new List<PostCategory>
            {
                new PostCategory() { ID=1 ,  Name = "PCG1", Status = true },
                new PostCategory() { ID = 2, Name = "PCG2", Status = true },
                new PostCategory() { ID = 3, Name = "PCG3", Status = true }
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //Setup Method
            _mockRepository.Setup(x => x.GetAll(null)).Returns(listPostCategories);

            //Call Action
            var result = _postCategoryService.GetAll().ToList();

            //Compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            var category = new PostCategory
            {
                Name = "Test-Category",
                Alias = "Test",
                Status = true
            };

            //Setup
            _mockRepository.Setup(m => m.Add(category)).Returns((PostCategory p) =>
            {
                p.ID = 1;
                return p;
            });

            //Call action
            var result = _postCategoryService.Add(category);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}