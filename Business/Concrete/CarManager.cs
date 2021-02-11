﻿using Business.Abstract;
using Business.Constants;
using Core.Utities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
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

        public IResult Add(Car car)
        {
            if (car.Descriptions.Length<2 && car.DailyPrice < 0)
            {
                return new ErrorResult(Messages.CarNameInvalid);
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
                    ncar.Descriptions = car.Descriptions;
                    ncar.ModelYear = car.ModelYear;

                    return new SuccessResult(Messages.Successed);
                }
            }

            return new ErrorResult(Messages.Error);
        }
    }
}
