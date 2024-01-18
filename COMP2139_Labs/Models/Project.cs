﻿using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Date)] //using notation to enforce date datatype as an entry
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
    }
}
