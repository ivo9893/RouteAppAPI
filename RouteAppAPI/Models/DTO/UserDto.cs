using System;
using System.ComponentModel.DataAnnotations;

namespace RouteAppAPI.Models.DTO
{
    public class UserRegistrationDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

    }

    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public string? Location { get; set; }
        public int TotalRoutes { get; set; }
        public decimal TotalDistanceKm { get; set; }
        public decimal TotalElevationGainM { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UserUpdateDto
    {
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(500)]
        public string? Bio { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }
    }
}
