using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class ValueOld
    {
        public long Id { get; set; }
        public int CounterId { get; set; }
        public decimal Value { get; set; }
        public decimal Cvalue { get; set; }
        public DateTime Date { get; set; }
        public byte FlowTypeId { get; set; }
    }
}
