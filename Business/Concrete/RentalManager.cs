using Business.Abstract;
using Business.Constants;
using Core.Utities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        public IResult Add(Rental t)
        {
            if (t.RentalId != 0 && t.CarId!= 0 && t.CustomerId!=0 && t.ReturnDate !=null)
            {
                _rentalDal.Add(t);
                return new SuccessResult(Messages.Successed);
            }

            return new ErrorResult(Messages.Error);
        }

        public IResult Delete(int id)
        {
            foreach (var rental in _rentalDal.GetAll())
            {
                if (rental.RentalId == id)
                {
                    _rentalDal.Delete(rental);
                    return new SuccessResult(Messages.Successed);
                }
            }
            return new ErrorResult(Messages.Error);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(),Messages.Successed);
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(p=>p.RentalId==id),Messages.Successed);
        }

        public IResult Update(int id, Rental t)
        {
            foreach (var rental in _rentalDal.GetAll())
            {
                if (rental.RentalId==id)
                {
                    rental.CarId = t.CarId;
                    rental.CustomerId = t.CustomerId;
                    rental.RentDate = t.RentDate;
                    rental.ReturnDate = t.ReturnDate;

                    return new SuccessResult(Messages.Successed);
                }
            }

            return new ErrorResult(Messages.Error);
        }
    }
}
