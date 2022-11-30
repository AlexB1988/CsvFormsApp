﻿using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class House
    {
        public int HouseId { get; set; }
        public int CounterId { get; set; }

        public virtual Counter Counter { get; set; } = null!;
    }
}
