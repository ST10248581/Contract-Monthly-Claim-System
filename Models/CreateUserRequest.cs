﻿using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class CreateUserRequest
    {

		[Required(ErrorMessage = "Password is required.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
		public string Password { get; set; }

		[Required(ErrorMessage = "First name is required.")]
		[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last name is required.")]
		[StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }

		public string UserRole { get; set; } 
    }
}