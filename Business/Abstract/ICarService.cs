using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetByDailyPrice(decimal min,decimal max);
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<Car>> GetAllByBrandId(int id);
        IDataResult<List<Car>> GetAllByColorId(int id);
        IDataResult<Car> GetById(int id);
        IResult Add(Car car);
        IResult Delete(int carId);
        IResult Update(int carId,Car car);
    }
}
