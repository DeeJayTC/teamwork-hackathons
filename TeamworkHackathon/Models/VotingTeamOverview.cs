using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Teamwork_Hackathon.Models
{
    [Table("viewVotingTeamOverview")]
    public class VotingTeamOverview
    {
        [Key]
        public int ID { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public string Members { get; set; }

        public int SortKey { get; set; }
        public string Logo { get; set; }

        [NotMapped]
        public bool HasVoted { get; set; }
        public List<HackathonVotingVote> Votes { get; set; }
    }

    [Table("viewVotingResultsByCategoryNonTech")]
    public class VotingResultsByCategoryNonTech
    {
        [Key]
        public Int64 RowID { get; set; }
        public int Score { get; set; }
        public int TeamID { get; set; }
        public int CategoryID { get; set; }

        public string TeamName { get; set; }
        [Column("Name")]
        public string CategoryName { get; set; }
    }


    [Table("viewVotingResultsTotalNonTech")]
    public class VotingResultsTotalNonTech
    {
        [Key]
        public Int64 RowID { get; set; }
        public int Score { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }


    }


    [Table("viewVotingResultsByCategory")]
    public class VotingResultsByCategory
    {
        [Key]
        public Int64 RowID { get; set; }
        public int Score { get; set; }
        public int TeamID { get; set; }
        public int CategoryID { get; set; }

        public string TeamName { get; set; }
        [Column("Name")]
        public string CategoryName { get; set; }
    }


    [Table("viewVotingResultsTotal")]
    public class VotingResultsTotal
    {
        [Key]
        public Int64 RowID { get; set; }
        public int Score { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }


    }
}
