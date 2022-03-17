using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Models.ManageViewModels
{
    public class TwoFactorAuthenticationViewModel : HackathonBaseModel

    {
        public TwoFactorAuthenticationViewModel(ApplicationUser pUser) : base(pUser)
        {
        }

        public bool HasAuthenticator { get; set; }

        public int RecoveryCodesLeft { get; set; }

        public bool Is2faEnabled { get; set; }
    }
}
