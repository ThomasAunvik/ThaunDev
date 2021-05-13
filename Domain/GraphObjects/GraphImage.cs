using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GraphObjects
{
    public class GraphImage
    {
        public string Name { get; set; }

        /// <summary>
        /// Base64 Encoded File Image
        /// </summary>
        public string Data { get; set; }
    }
}
