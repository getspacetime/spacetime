using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Core.Formatters
{
    public interface IFormatter
    {
        public string Format(string text);
    }
}
