using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

namespace CoffeeShop.UnitTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        IDbFactory dbFactory;
        IPostCategoryRepository postCategoryRepository;
        IUnitOfWork unitOfWork;


        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            postCategoryRepository = new PostCategoryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }


        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory postCategory = new PostCategory
            {
                Name = "Test-PostCategory",
                Alias = "Test-PostCategory-Alias"

            };

            var createPostCategory = postCategoryRepository.Add(postCategory);
            unitOfWork.Commit();

            var loadedPostCategory = postCategoryRepository.GetById(createPostCategory.ID);

            Assert.IsNotNull(createPostCategory);
            Assert.IsNotNull(loadedPostCategory);
            Assert.AreEqual(createPostCategory.Name, loadedPostCategory.Name);
        }

        [TestMethod]
        public void PostCategory_GetById()
        {
            var result = postCategoryRepository.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Alias);
        }
    }
}
