using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult Add(Rental rental);
        IResult Delete(int rentalId);
        IResult Update(int rentalId, Rental rental);
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetById(int rentalId);
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
    }
}
