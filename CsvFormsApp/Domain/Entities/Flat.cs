using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class Flat
    {
        public int FlatId { get; set; }
        public int CounterId { get; set; }

        public virtual Counter Counter { get; set; } = null!;
    }
}
