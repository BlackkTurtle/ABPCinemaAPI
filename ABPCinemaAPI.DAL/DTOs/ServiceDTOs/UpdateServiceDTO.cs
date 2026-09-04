using System;
using System.ComponentModel.DataAnnotations;

namespace ABPCinemaAPI.DAL.DTOs.ServiceDTOs
{
    public class UpdateServiceDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number greater than 0.")]
        public int Price { get; set; }
    }
}