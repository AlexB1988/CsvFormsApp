﻿using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public int CounterId { get; set; }

        public virtual List Counter { get; set; } = null!;
    }
}
