using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class Authorization
    {
        public int Id { get; set; }
        public int? HouseId { get; set; }
        public string? Url { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public byte? AutomatedSystem { get; set; }
    }
}
