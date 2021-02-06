﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll();
        List<Car> GetCarByBrandId(int id);
        List<Car> GetCarByColorId(int id);
        List<Car> GetByDailyPrice(decimal min,decimal max);
    }
}
