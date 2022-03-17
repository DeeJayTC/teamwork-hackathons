using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamwork_Hackathon.Models.DTO;

namespace Teamwork_Hackathon.Models.DTO
{
    public class HackathonTeamListDto : HackathonBaseModel
    {
        public HackathonTeamListDto(ApplicationUser pUser) : base(pUser){}

        public List<HackathonTeam> Teams { get; set; }
    }


    public class HackathonTeamDto : HackathonBaseModel
    {
        public HackathonTeamDto(ApplicationUser pUser) : base(pUser){ }
        public HackathonTeam Team { get; set; }
    }


    public class HackathonPeopleSearchDto : HackathonBaseModel
    {
        public HackathonPeopleSearchDto(ApplicationUser pUser) : base(pUser) { }
        public IEnumerable<Teamwork_Hackathon.Models.HackathonSearchMembers> People { get; set; }
    }

    public class HomeDto : HackathonBaseModel
    {
        public HomeDto(ApplicationUser pUser) : base(pUser) { }
    }

}
