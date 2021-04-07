using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal: EfEntityRepositoryBase<Rental, ReCapProjectCarDbContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (ReCapProjectCarDbContext context = new ReCapProjectCarDbContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarId equals c.CarId
                             join p in context.Customers
                             on r.CustomerId equals p.CustomerId
                             select new RentalDetailDto
                             {
                                 RentalId = r.RentalId,
                                 CarModelName = c.CarModelName,
                                 CompanyName = p.CompanyName,
                                 DailyPrice = c.DailyPrice,
                                 Description= c.Description,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
