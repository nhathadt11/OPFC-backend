﻿using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace OPFC.Services.Implementations
{
    public class BrandService : IBrandService
    {
        /// <summary>
        /// OPFC Unit Of Work
        /// </summary>
        private readonly IOpfcUow _opfcUow;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="opfcUow"></param>
        public BrandService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public Brand CreateBrand(Brand brand)
        {
            using(var scope = new TransactionScope())
            {
                var result = _opfcUow.BrandRepository.CreateBrand(brand);
                _opfcUow.Commit();

                var brandSummary = new BrandSummary
                {
                    BrandId = result.Id
                };
                _opfcUow.BrandSummaryRepository.Create(brandSummary);
                _opfcUow.Commit();

                scope.Complete();
                return result;
            }
        }

        public Caterer CreateCaterer(Caterer caterer)
        {
            caterer.User.IsActive = true;
            caterer.User.IsDeleted = false;
            caterer.Brand.IsActive = true;

            return _opfcUow.BrandRepository.CreateCaterer(caterer.User, caterer.Brand);
        }

        public Brand GetBrandById(long id)
        {
            var foundBrand = _opfcUow.BrandRepository.GetBrandById(id);
            var foundUser = _opfcUow.UserRepository.GetById(foundBrand.UserId);

            foundBrand.Avatar = foundUser.Avatar;

            var brandSummary = _opfcUow.BrandSummaryRepository.GetByBrandId(id);
            foundBrand.BrandSummary = brandSummary;

            return foundBrand;
        }

        public Brand UpdateBrand(Brand brand)
        {
            Brand result = null;
            using(var scope = new TransactionScope())
            {
                result = _opfcUow.BrandRepository.UpdateBrand(brand);
                var userToUpdate = _opfcUow.UserRepository.GetById(brand.UserId);
                userToUpdate.Avatar = brand.Avatar;
                _opfcUow.UserRepository.Update(userToUpdate);
    
                _opfcUow.Commit();

                scope.Complete();
            }

            return result;
        }

        public Brand ChangeBrandStatus(long brandId, bool isActive)
        {
            var brand = GetBrandById(brandId);
            if (brand == null) throw new Exception("Brand could not be found.");
            
            brand.IsActive = isActive;
            _opfcUow.BrandRepository.Update(brand);
            _opfcUow.Commit();

            return brand;
        }

        public void SavePhoto(Photo photo)
        {
            if (photo.BrandId == null && photo.MenuId == null) throw new Exception("Invalid data.");

            _opfcUow.PhotoRepository.SavePhoto(photo);
            _opfcUow.Commit();
        }

        public Brand GetBrandByUserId(long id)
        {
            return _opfcUow.BrandRepository.GetBrandByUserId(id);
        }

        public List<Brand> GetAllBrand()
        {
            return _opfcUow.BrandRepository.GetAllBrand();
        }

        public bool Exists(long id)
        {
            return _opfcUow.BrandRepository.Exists(id);
        }

        public bool IsBrandNameAvailable(string brandName)
        {
            return _opfcUow.BrandRepository.IsBrandNameAvailable(brandName);
        }
    }
}
