using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IResult Add(Customer customer);
        IResult Delete(int customerId);
        IResult Update(int customerId, Customer customer);
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetByCustomerId(int id);
        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
    }
}
