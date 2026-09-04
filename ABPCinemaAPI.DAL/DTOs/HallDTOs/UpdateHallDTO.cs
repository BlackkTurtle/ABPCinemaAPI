using ABPCinemaAPI.DAL.DTOs.ServiceDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABPCinemaAPI.DAL.DTOs.HallDTOs
{
    public class UpdateHallDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number greater than 0.")]
        public int Capacity { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number greater than 0.")]
        public int Price { get; set; }
        public List<UpdateServiceDTO> Services { get; set; } = new List<UpdateServiceDTO>();
    }
}