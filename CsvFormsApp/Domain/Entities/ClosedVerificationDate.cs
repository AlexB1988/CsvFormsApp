using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class ClosedVerificationDate
    {
        public int CounterId { get; set; }
        public int PeriodId { get; set; }
        public int? VerificationInterval { get; set; }
        public DateTime? VerificationDate { get; set; }
        public DateTime? FailureDate { get; set; }
        public int? SublistId { get; set; }
        public int? AccountId { get; set; }
        public decimal? Value { get; set; }
        public decimal? Cvalue { get; set; }
        public byte? FlowTypeId { get; set; }
        public DateTime? FlowDate { get; set; }
        public string? CloseType { get; set; }
    }
}
