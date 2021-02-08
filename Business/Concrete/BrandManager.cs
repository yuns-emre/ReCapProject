using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void Add()
        {
            Console.WriteLine("Adding a car brand.");
        }

        public void Delete()
        {
            Console.WriteLine("Deleteing a car brand.");
        }

        public List<Brand> GetAll()
        {
            return _brandDal.GetAll();
        }

        public Brand GetByBrandId(int brandId)
        {
            return _brandDal.Get(b=>b.BrandId == brandId);
        }

        public void Update()
        {
            Console.WriteLine("Updateding a car brand");
        }
    }
}
