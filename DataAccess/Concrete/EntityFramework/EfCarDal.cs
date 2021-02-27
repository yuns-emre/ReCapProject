using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal :EfEntityRepositoryBase<Car,ReCapProjectCarDbContext>,ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (ReCapProjectCarDbContext context = new ReCapProjectCarDbContext())
            {
                var result = from p in context.Cars
                             join b in context.Brands
                             on p.CarId equals b.BrandId
                             join c in context.Colors
                             on p.CarId equals c.ColorId
                             select new CarDetailDto
                             {
                                 CarId = p.CarId,
                                 CarModelName = p.CarModelName,
                                 DailyPrice = p.DailyPrice,
                                 ModelYear = p.ModelYear,
                                 Descriptions = p.Description,
                                 BrandName = b.BrandName,
                                 ColorName = c.ColorName
                                 
                             };
                return result.ToList();
            }
        }
    }
}
