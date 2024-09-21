using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Helpers
{
    public interface ISoftDelete
    {
        bool? IsDeleted { get; set; }
    }
}
