using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TeamPlayer
    {
        public Guid teamId { get; set; }
        public Guid playerId { get;set; }

        public virtual Player Player { get; set; }
        public virtual Team Team {  get; set; } 
    }
}
