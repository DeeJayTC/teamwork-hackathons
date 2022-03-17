using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Models.ManageViewModels
{
    public class ExternalLoginsViewModel : HackathonBaseModel
    {
        public ExternalLoginsViewModel(ApplicationUser pUser) : base(pUser)
        {
        }

        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationScheme> OtherLogins { get; set; }

        public bool ShowRemoveButton { get; set; }

        public string StatusMessage { get; set; }
    }
}
