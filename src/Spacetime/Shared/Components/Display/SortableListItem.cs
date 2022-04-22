using Spacetime.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Shared.Components.Display
{
    public class SortableListItem
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public HtmlOffset Offset { get; set; } = new HtmlOffset();
        public string Value { get; set; }
        public string Prefix { get; set; }
    }
}
