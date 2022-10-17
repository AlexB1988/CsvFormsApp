using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public partial class CountersInGroup
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int ParentCounterId { get; set; }
        public int ChildCounterId { get; set; }
        public int? SubListId { get; set; }

        public virtual Group Group { get; set; } = null!;
    }
}
