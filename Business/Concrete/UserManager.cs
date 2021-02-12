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
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IResult Add(User t)
        {
            if (t.FirstName.Length>3 && t.LastName.Length >2 && t.Email.Length != 0 && t.Password.Length >= 8)
            {
                _userDal.Add(t);
                return new SuccessResult(Messages.Successed);
            }

            return new ErrorResult(Messages.Error);
        }

        public IResult Delete(int id)
        {
            foreach (var user in _userDal.GetAll())
            {
                if (user.UserId == id)
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

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(p=>p.UserId == id), Messages.Successed);
        }

        public IResult Update(int id, User t)
        {
            foreach (var user in _userDal.GetAll())
            {
                if (user.UserId == id)
                {
                    user.FirstName = t.FirstName;
                    user.LastName = t.LastName;
                    user.Email = t.Email;
                    user.Password = t.Password;

                    return new SuccessResult(Messages.Successed);
                }
            }
            return new ErrorResult(Messages.Error);
        }
    }
}
