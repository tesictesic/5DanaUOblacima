using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PlayerStatistic:Entity
    {

        public int wins { get; set; } = 0;
        public int losses { get; set; } = 0;
        public double elo { get; set; } = 0;
        public int hoursPlayed { get; set; } = 0;
        public Guid player_id { get; set; }
        public virtual Player Player { get; set; }

    }
}
