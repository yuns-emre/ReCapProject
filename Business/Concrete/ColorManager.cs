﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
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

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult(Messages.Successed);
        }

        public IResult Delete(int colorId)
        {
            foreach (var ncolor in _colorDal.GetAll())
            {
                if (ncolor.ColorId == colorId)
                {
                    _colorDal.Delete(ncolor);
                    return new SuccessResult(Messages.Successed);
                }

            }
            return new ErrorResult(Messages.Error);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>> (_colorDal.GetAll());
        }

        public IDataResult<Color> GetByColorId(int colorId)
        {
            return new SuccessDataResult<Color> (_colorDal.Get(c=>c.ColorId==colorId));
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(int colorId,Color color)
        {
            foreach (var ncolor in _colorDal.GetAll())
            {
                if (ncolor.ColorId == colorId)
                {
                    ncolor.ColorId = color.ColorId;
                    ncolor.ColorName = color.ColorName;
                    _colorDal.Update(color);
                    return new SuccessResult(Messages.Successed);
                }
            }

            return new ErrorResult(Messages.Error);
        }
    }
}
