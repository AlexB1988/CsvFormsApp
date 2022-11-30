using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class VerificationHistory
    {
        public int Id { get; set; }
        public int CounterId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SessionId { get; set; }
        public DateTime VerificationDate { get; set; }

        public virtual Counter Counter { get; set; } = null!;
    }
}
