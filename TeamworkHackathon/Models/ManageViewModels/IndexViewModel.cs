using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Models.ManageViewModels
{
    public class IndexViewModel : HackathonBaseModel
    {
        public IndexViewModel(ApplicationUser pUser) : base(pUser)
        {
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
