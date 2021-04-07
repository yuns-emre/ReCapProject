using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class RentalDetailDto:IDto
    {
        public int RentalId { get; set; }
        public decimal DailyPrice { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CarModelName { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
    }
}
