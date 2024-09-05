using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionManager.API.Domain
{
    public class Collection
    {
        public Guid CollectionId { get; set; }
        public Guid AccountId { get; set; }
        public IQueryable<Card>? Cards { get; set; }
        public Account? Account { get; set; }
    }
}
