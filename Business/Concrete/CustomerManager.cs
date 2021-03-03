using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
        public IResult Add(Customer t)
        {
            _customerDal.Add(t);
            return new SuccessResult(Messages.Successed);
        }

        public IResult Delete(int id)
        {
            foreach (var customerId in _customerDal.GetAll())
            {
                if (customerId.UserId == id)
                {
                    _customerDal.Delete(customerId);
                    return new SuccessResult(Messages.Successed);
                }
            }

            return new ErrorResult(Messages.Error);
        }

        public IDataResult<List<Customer>> GetAll()
        {           
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.Successed);
        }

        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(p=>p.UserId == id), Messages.Successed);
        }

        public IResult Update(int id, Customer t)
        {
            foreach (var customer in _customerDal.GetAll())
            {
                if (customer.UserId== id)
                {
                    customer.UserId = t.UserId;
                    customer.CompanyName = t.CompanyName;
                    return new SuccessResult(Messages.Successed);
                }
            }

            return new SuccessResult(Messages.Error);
        }
    }


}
