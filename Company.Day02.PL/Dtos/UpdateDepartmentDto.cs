﻿using System.ComponentModel.DataAnnotations;

namespace Company.Day02.PL.Dtos
{
    public class UpdateDepartmentDto
    {
        [Required(ErrorMessage = "Code Is Required ! ")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Required ! ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "CreateAt Is Required ! ")]
        public DateTime CreateAt { get; set; }

    }
}
