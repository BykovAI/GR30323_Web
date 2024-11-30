using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR30323_Web.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; } // Например, "sedans", "SUVs"
        public List<Car>? Cars { get; set; }
    }

}
