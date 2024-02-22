﻿using System.ComponentModel.DataAnnotations;

namespace QomfortHotelFinal.Areas.Admin.ViewModels.Role
{
    public class CreateRoleVM
    {
        [Required(ErrorMessage = "A Name must be included")]
        [MaxLength(100, ErrorMessage = "No more than 100 characters")]
        [MinLength(4, ErrorMessage = "Be less than 4 characters")]
        public string Name { get; set; }
    }
}
