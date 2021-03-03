using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;


        public CarManager(ICarDal carDal)
        {
            
            _carDal = carDal;
        }


        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfCarNameExists(car.CarModelName,car.Description));

            if (result != null)
            {
                return result;
            }
            _carDal.Add(car);
            
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(int carId)
        {
            foreach (var carid in _carDal.GetAll())
            {
                if (carid.CarId==carId)
                {
                    _carDal.Delete(carid);
                    return new SuccessResult(Messages.Successed);
                }

            }

            return new ErrorResult(Messages.Error);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            
            return new SuccessDataResult<List<Car>>(Messages.CarListed);
        }

        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p=>p.DailyPrice >= min && p.DailyPrice <= max));
        }

        public IDataResult<List<Car>> GetById(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.CarId == id), Messages.Successed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>> (_carDal.GetCarDetails());
        }

        public IResult Update(int carId,Car car)
        {
            foreach (var ncar in _carDal.GetAll())
            {
                if (ncar.CarId==carId)
                {
                    ncar.CarId = car.CarId;
                    ncar.BrandId = car.BrandId;
                    ncar.ColorId = car.ColorId;
                    ncar.DailyPrice = car.DailyPrice;
                    ncar.Description = car.Description;
                    ncar.ModelYear = car.ModelYear;

                    return new SuccessResult(Messages.Successed);
                }
            }

            return new ErrorResult(Messages.Error);
        }

        private IResult CheckIfCarNameExists(string carName,string carDescription)
        {
            var result = _carDal.GetAll(p=>p.CarModelName == carName && p.Description == carDescription).Any();
            if (result)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.Successed);
        }
    }
}
