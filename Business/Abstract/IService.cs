using Core.Utities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IService<T>
    {
        //Bu kısa yolu https://github.com/aycakdemr bu arkadaşımdan görüp yaptım.
        IResult Add(T t);
        IResult Delete(int id);
        IResult Update(int id,T t);
        IDataResult<List<T>> GetAll();
        IDataResult<T> GetById(int id);
    }
}
