using ABPCinemaAPI.DAL.DTOs.ServiceDTOs;
using ABPCinemaAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.DTOs.HallDTOs
{
    public class CreateHallDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number greater than 0.")]
        public int Capacity { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number greater than 0.")]
        public int Price { get; set; }
        public List<CreateServiceDTO> Services { get; set; } = new List<CreateServiceDTO>();
    }
}
