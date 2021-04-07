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
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, ReCapProjectCarDbContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (ReCapProjectCarDbContext context = new ReCapProjectCarDbContext())
            {
                var result = from c in context.Customers
                             join u in context.Users
                             on c.UserId equals u.UserId
                             select new CustomerDetailDto
                             {
                                 CustomerId = c.CustomerId,
                                 UserFirstName = u.FirstName,
                                 UserLastName = u.LastName,
                                 CompanyName = c.CompanyName
                             };
                return result.ToList();
            }
        }
    }
}
