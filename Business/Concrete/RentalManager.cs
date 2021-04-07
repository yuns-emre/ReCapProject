using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
        public IResult Add(Rental rental)
        {
            IResult result = BusinessRules.Run(ChecckIfReturnDate(rental.CarId));

            if (result != null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.Successed);

        }

        public IResult Delete(int rentalId)
        {
            foreach (var rental in _rentalDal.GetAll())
            {
                if (rental.RentalId == rentalId)
                {
                    _rentalDal.Delete(rental);
                    return new SuccessResult(Messages.Successed);
                }
            }
            return new ErrorResult(Messages.Error);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.Successed);
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(p => p.RentalId == rentalId), Messages.Successed);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.Successed);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(int rentalId, Rental rental)
        {
            foreach (var nrental in _rentalDal.GetAll())
            {
                if (nrental.RentalId==rentalId)
                {
                    nrental.CarId = rental.CarId;
                    nrental.CustomerId = rental.CustomerId;
                    nrental.RentDate = rental.RentDate;
                    nrental.ReturnDate = rental.ReturnDate;

                    _rentalDal.Update(rental);
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
