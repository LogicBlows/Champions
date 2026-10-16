using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionPortal.Domain.Entities
{
    public class Competency
    {
        public Guid ID { get; set; }

        public string Code { get; set; } = default;

        public string Name { get; set; } = default; 

        public int SortOrder { get; set; }

        public bool isActive { get; set; } = true;

        public string Description { get; set; } = default;

        public Guid? HeadEmployeeId { get; set; }
    }

}
