using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Models.ManageViewModels
{
    public class RemoveLoginViewModel : HackathonBaseModel
    {
        public RemoveLoginViewModel(ApplicationUser pUser) : base(pUser)
        {
        }

        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
