using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public List<Color> GetAll()
        {
            return _colorDal.GetAll();
        }

        public Color GetByColorId(int colorId)
        {
            return _colorDal.Get(c=>c.ColorId==colorId);
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
