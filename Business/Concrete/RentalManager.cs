using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental t)
        {
            IResult result = BusinessRules.Run(ChecckIfReturnDate(t.CarId));

            if (result != null)
            {
                return result;
            }
            _rentalDal.Add(t);
            return new SuccessResult(Messages.Successed);

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

        [ValidationAspect(typeof(RentalValidator))]
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

                    _rentalDal.Update(t);
                    return new SuccessResult(Messages.Successed);
                }
            }

            return new ErrorResult(Messages.Error);
        }

        private IResult ChecckIfReturnDate(int carId)
        {
            var result = _rentalDal.GetAll(p=>p.CarId==carId);
            var updateRental = result.LastOrDefault();
            if (updateRental.ReturnDate != null)
            {
                return new ErrorResult(Messages.Error);
            }

            updateRental.ReturnDate = DateTime.Now;

            return new SuccessResult();
        }
    }
}
