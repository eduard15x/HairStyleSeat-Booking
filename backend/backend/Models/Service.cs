﻿using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string HaircutDurationTime { get; set; }
    }
}
