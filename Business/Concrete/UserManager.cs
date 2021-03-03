using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            var result = BusinessRules.Run(ChechkIfEmailExsist(user.Email));

            if (result != null)
            {
                return result;
            }

             _userDal.Add(user);
            return new SuccessResult(Messages.Successed);

        }

        public IResult Delete(int userId)
        {
            foreach (var user in _userDal.GetAll())
            {
                if (user.UserId == userId)
                {
                    _userDal.Delete(user);
                    return new SuccessResult(Messages.Successed);
                }
            }

            return new ErrorResult(Messages.Error);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(),Messages.Successed);
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(p=>p.UserId == userId), Messages.Successed);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u=>u.Email == email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(int userId, User user)
        {
            foreach (var nuser in _userDal.GetAll())
            {
                if (nuser.UserId == userId)
                {
                    nuser.FirstName = user.FirstName;
                    nuser.LastName = user.LastName;
                    nuser.Email = user.Email;
                    

                    return new SuccessResult(Messages.Successed);
                }
            }
            return new ErrorResult(Messages.Error);
        }

        private IResult ChechkIfEmailExsist(string email)
        {
            var result = _userDal.GetAll(u=>u.Email == email).Count;
            if (result != 0)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.Successed);
        }

    }
}
