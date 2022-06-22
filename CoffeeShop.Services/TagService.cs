using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

using System.Collections.Generic;

namespace CoffeeShop.Services
{
    public class TagService : ITagService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public ITagRepository _tagRepository { get; set; }

        public TagService(IUnitOfWork unitOfWork, ITagRepository tagRepository)
        {
            _unitOfWork = unitOfWork;
            _tagRepository = tagRepository;
        }

        public void Add(Tag tag)
        {
            _tagRepository.Add(tag);
        }

        public void Delete(Tag tag)
        {
            _tagRepository.Delete(tag);
        }

        public void Delete(int id)
        {
            _tagRepository.Delete(id);
        }

        public IEnumerable<Tag> GetAll()
        {
            return _tagRepository.GetAll(new string[] { "Posts", "Products" });
        }

        public IEnumerable<Tag> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all Tag by tag
            return _tagRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public IEnumerable<Tag> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _tagRepository.GetMultiPaging(null, out totalRow, page, pageSize);
        }

        public Tag GetById(int id)
        {
            return _tagRepository.GetById(id);
        }

        public Tag GetByIdString(string id)
        {
            return _tagRepository.GetByIdString(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Tag tag)
        {
            _tagRepository.Update(tag);
        }
    }
}