using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.Successed);
        }

        public IResult Delete(int customerId)
        {
            foreach (var customer in _customerDal.GetAll())
            {
                if (customer.CustomerId == customerId)
                {
                    _customerDal.Delete(customer);
                    return new SuccessResult(Messages.Successed);
                }
            }

            return new ErrorResult(Messages.Error);
        }

        public IDataResult<List<Customer>> GetAll()
        {           
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.Successed);
        }

        public IDataResult<Customer> GetByCustomerId(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(p=>p.CustomerId == id), Messages.Successed);
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails());
        }

        public IResult Update(int customerId, Customer customer)
        {
            foreach (var ncustomer in _customerDal.GetAll())
            {
                if (ncustomer.CustomerId== customerId)
                {
                    ncustomer.UserId = customer.UserId;
                    ncustomer.CompanyName = customer.CompanyName;
                    return new SuccessResult(Messages.Successed);
                }
            }

            return new SuccessResult(Messages.Error);
        }
    }


}
