using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionManager.API.Domain
{
    public class Card
    {
        public Guid CardId { get; set; }
        public Collection? Collection { get; set; }
        public Guid CollectionId { get; set; }
    }
}
