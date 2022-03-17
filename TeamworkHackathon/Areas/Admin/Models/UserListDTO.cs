using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamwork_Hackathon.Models;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Areas.Admin.Models
{
    public class UserListDTO : HackathonBaseModel
    {

        public UserListDTO(ApplicationUser pUser) : base(pUser){ }
        public List<AspNetUsers> Users { get; set; }
    }
}
