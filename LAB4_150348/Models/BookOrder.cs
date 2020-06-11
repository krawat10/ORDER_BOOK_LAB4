﻿using System.ComponentModel.DataAnnotations;

namespace LAB4_150348.Models
{
    public class BookOrder
    {
        [Key]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int BookAmount { get; set; }
        [Key]
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}