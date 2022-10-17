using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class Owner
    {
        public int HouseId { get; set; }
        public string? ShortNameAdress { get; set; }
        public string FlatName { get; set; } = null!;
        public short FlatBookId { get; set; }
        public string AccountName { get; set; } = null!;
        public string? OldAccountName { get; set; }
        public int AccountId { get; set; }
        public string? Fio { get; set; }
        public int FlatId { get; set; }
        public int? IsClosed { get; set; }
    }
}
