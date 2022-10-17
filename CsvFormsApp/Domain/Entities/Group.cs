using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class Group
    {
        public Group()
        {
            CountersInGroups = new HashSet<CountersInGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? SubListId { get; set; }
        public string? SublistIdby { get; set; }

        public virtual ICollection<CountersInGroup> CountersInGroups { get; set; }
    }
}
