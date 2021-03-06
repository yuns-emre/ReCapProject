﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult(Messages.Successed);
        }

        public IResult Delete(int brandId)
        {
            foreach (var nbrand in _brandDal.GetAll())
            {
                if (nbrand.BrandId==brandId)
                {
                    _brandDal.Delete(nbrand);
                    return new SuccessResult(Messages.Successed);
                }

            }
            return new ErrorResult(Messages.Error);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>( _brandDal.GetAll(),Messages.Successed);
        }

        public IDataResult<Brand> GetByBrandId(int brandId)
        {
            return new SuccessDataResult<Brand> (_brandDal.Get(b=>b.BrandId == brandId),Messages.Successed);
        }

        public IResult Update(int brandId,Brand brand)
        {
            foreach (var nbrand in _brandDal.GetAll())
            {
                if (nbrand.BrandId==brandId)
                {
                    nbrand.BrandId = brand.BrandId;
                    nbrand.BrandName = brand.BrandName;
                    _brandDal.Update(brand);
                    return new SuccessResult(Messages.Successed);
                }
            }

            return new ErrorResult(Messages.Error);
        }
    }
}
