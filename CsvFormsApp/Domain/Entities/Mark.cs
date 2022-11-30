using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class Mark
    {
        public Mark()
        {
            Lists = new HashSet<Counter>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Counter> Lists { get; set; }
    }
}
