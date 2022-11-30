using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class Flow
    {
        public int CounterId { get; set; }
        public int PeriodId { get; set; }
        public decimal PrevValue { get; set; }
        public decimal? Value { get; set; }
        public decimal Cvalue { get; set; }
        public byte? FlowTypeId { get; set; }
        public int SubListId { get; set; }
        public decimal? Flow1 { get; set; }
        public decimal? Rate { get; set; }
        public DateTime? PrevDate { get; set; }
        public DateTime? Date { get; set; }
        public int? SessionId { get; set; }
        public DateTime? SecurityDate { get; set; }

        public virtual Counter Counter { get; set; } = null!;
    }
}
