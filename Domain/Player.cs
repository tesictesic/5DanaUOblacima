using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Player:Entity
    {
        public string nickname { get; set; }
        
        public virtual ICollection<TeamPlayer> TeamPlayer  { get; set; }
        public virtual ICollection<PlayerStatistic> PlayerStatistic { get; set; }
    }
}
