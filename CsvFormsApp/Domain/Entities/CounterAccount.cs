using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class CounterAccount
    {
        public int AccountId { get; set; }
        public int CounterId { get; set; }

        public virtual Counter Counter { get; set; } = null!;
    }
}
