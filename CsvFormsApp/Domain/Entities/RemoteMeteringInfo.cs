using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class RemoteMeteringInfo
    {
        public RemoteMeteringInfo()
        {
            Lists = new HashSet<List>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<List> Lists { get; set; }
    }
}
