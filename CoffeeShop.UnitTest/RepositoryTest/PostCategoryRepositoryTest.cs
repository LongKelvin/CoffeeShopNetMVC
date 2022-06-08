using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace CoffeeShop.UnitTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        private IDbFactory dbFactory;
        private IPostCategoryRepository postCategoryRepository;
        private IUnitOfWork unitOfWork;

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
            Assert.AreEqual("Test-PostCategory", result.Name);
        }

        [TestMethod]
        public void PostCategory_GetAll()
        {
            var result = postCategoryRepository.GetAll();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ToList().Count() > 0, "Expected post category count to be greater than 0.");
        }
    }
}