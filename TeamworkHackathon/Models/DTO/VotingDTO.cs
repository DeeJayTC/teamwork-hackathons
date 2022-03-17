using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teamwork_Hackathon.Models.DTO
{
    public class VotingDTO : HackathonBaseModel
    {
        public VotingDTO(ApplicationUser pUser) : base(pUser){ }

        public List<VotingTeamOverview> Teams { get; set; }
    }


    public class VotingResultsDTO : HackathonBaseModel
    {
        public VotingResultsDTO(ApplicationUser pUser) : base(pUser) { }

        public List<VotingResultsByCategory> CategoryResults { get; set; }
        public List<VotingResultsTotal> TotalResults { get; set; }

        public List<VotingResultsByCategoryNonTech> CategoryResultsNonTech { get; set; }
        public List<VotingResultsTotalNonTech> TotalResultsNonTech { get; set; }
        

    }

}
