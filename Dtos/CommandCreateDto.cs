﻿using System.ComponentModel.DataAnnotations;

namespace Commander.API.Dtos
{
    public class CommandCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Platform { get; set; }
    }
}
