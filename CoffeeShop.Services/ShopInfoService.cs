﻿using CoffeeShop.Data.Insfrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Models.Models;

namespace CoffeeShop.Services
{
    public class ShopInfoService : IShopInfoService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IShopInfoRepository _shopInfoRepository { get; set; }

        public ShopInfoService(IUnitOfWork unitOfWork, IShopInfoRepository shopInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _shopInfoRepository = shopInfoRepository;
        }

        public ShopInformation GetShopInfo(int id = 1)
        {
            return _shopInfoRepository.GetById(id);
        }
    }
}