using Core.Utities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IService<T>
    {
        IResult Add(T t);
        IResult Delete(int id);
        IResult Update(int id,T t);
        IDataResult<List<T>> GetAll();
        IDataResult<T> GetById(int id);
    }
}
