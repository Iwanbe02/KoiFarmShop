using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Helpers
{
    public class IdGenerator
    {
        public static string GenerateId(IEnumerable<string> existingIds, string prefix, int length = 3)
        {
            if (!existingIds.Any())
            {
                return prefix + "001"; 
            }

            var maxNumber = existingIds
                .Select(id => int.Parse(id.Substring(prefix.Length)))
                .Max();

            return prefix + (maxNumber + 1).ToString($"D{length}");
        }
    }
}
