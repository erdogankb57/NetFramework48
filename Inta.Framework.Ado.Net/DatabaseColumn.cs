using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Ado.Net
{
    public class DatabaseColumn : Attribute
    {
        public string Name { get; set; }
    }
}
