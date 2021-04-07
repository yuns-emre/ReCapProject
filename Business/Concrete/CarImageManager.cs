using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.ImageId == id));
        }

        [CacheAspect]
        public IDataResult<CarImage> GetById(int id)
        {
            var result = _carImageDal.Get(c => c.ImageId == id);

            IfCarImageOfCarNotExistsAddDefault(ref result);

            return new SuccessDataResult<CarImage>(result);
        }

        [SecuredOperation("carimage.getall,moderator,admin")]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);

            IfCarImageOfCarNotExistsAddDefault(ref result);

            return new SuccessDataResult<List<CarImage>>(result);
        }

        [SecuredOperation("carimage.add,moderator,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            var result = BusinessRules.Run(
                CheckIfCarImageCountOfCarCorrect(carImage.CarId));
            if (result != null) return result;

            carImage.ImagePath = new FileManagerOnDisk().Add(file, CreateNewPath(file));
            carImage.ImageDate = DateTime.Now;
            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.Successed);
        }

        [SecuredOperation("carimage.update,moderator,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Update(IFormFile file,CarImage carImage)
        {
            var carImageToUpdate = _carImageDal.Get(c => c.ImageId == carImage.ImageId);
            carImage.CarId = carImageToUpdate.CarId;
            carImage.ImagePath = new FileManagerOnDisk().Update(carImageToUpdate.ImagePath, file, CreateNewPath(file));
            carImage.ImageDate = DateTime.Now;
            _carImageDal.Update(carImage);

            return new SuccessResult(Messages.Successed);
        }

        [SecuredOperation("carimage.delete,moderator,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImage carImage)
        {
            new FileManagerOnDisk().Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);

            return new SuccessResult(Messages.Successed);
        }

        private void IfCarImageOfCarNotExistsAddDefault(ref List<CarImage> result)
        {
            if (!result.Any()) result.Add(CreateDefaultCarImage());
        }

        private void IfCarImageOfCarNotExistsAddDefault(ref CarImage result)
        {
            if (result == null) result = CreateDefaultCarImage();
        }

        private CarImage CreateDefaultCarImage()
        {
            var defaultCarImage = new CarImage
            {
                ImagePath =
                    $@"{Environment.CurrentDirectory}\Public\Images\CarImage\default-img.png",
                ImageDate = DateTime.Now
            };

            return defaultCarImage;
        }

        private string CreateNewPath(IFormFile file)
        {
            var fileInfo = new FileInfo(file.FileName);
            var newPath =
                $@"{Environment.CurrentDirectory}\Public\Images\CarImage\Upload\{Guid.NewGuid()}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Year}{fileInfo.Extension}";

            return newPath;
        }

        private IResult CheckIfCarImageCountOfCarCorrect(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5) return new ErrorResult(Messages.Error);

            return new SuccessResult();
        }

        
    }
}

