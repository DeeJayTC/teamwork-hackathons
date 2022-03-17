using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Models.ManageViewModels
{
    public class GenerateRecoveryCodesViewModel : HackathonBaseModel
    {
        public GenerateRecoveryCodesViewModel(ApplicationUser pUser) : base(pUser)
        {
        }

        public string[] RecoveryCodes { get; set; }
    }
}
