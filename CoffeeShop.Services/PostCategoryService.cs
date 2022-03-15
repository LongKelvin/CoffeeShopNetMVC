using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class PostCategoryService : IPostCategoryService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IPostCategoryRepository _postCategoryRepository { get; set; }

        public PostCategoryService(IUnitOfWork unitOfWork, IPostCategoryRepository postCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _postCategoryRepository = postCategoryRepository;
        }

        public void Add(PostCategory postCategory)
        {
            _postCategoryRepository.Add(postCategory);
        }

        public void Delete(PostCategory postCategory)
        {
            _postCategoryRepository.Delete(postCategory);
        }

        public void Delete(int id)
        {
            _postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return _postCategoryRepository.GetAll();
        }

        //public IEnumerable<PostCategory> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        //{
        //    //TODO: Select all PostCategory by tag
        //    return _postCategoryRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        //}

        public IEnumerable<PostCategory> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _postCategoryRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public PostCategory GetById(int id)
        {
            return _postCategoryRepository.GetById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(PostCategory postCategory)
        {
            _postCategoryRepository.Update(postCategory);
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return _postCategoryRepository.GetMulti(x => x.Status == true && x.ParentID == parentId);
        }
    }
}