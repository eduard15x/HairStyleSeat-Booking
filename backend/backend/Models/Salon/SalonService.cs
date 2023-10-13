﻿using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Salon
{
    public class SalonService
    {
        public int Id { get; set; }
        public int SalonId { get; set; }
        [ForeignKey("SalonId")]
        public Salon Salon { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
    }
}