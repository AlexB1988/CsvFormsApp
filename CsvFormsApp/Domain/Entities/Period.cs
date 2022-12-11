using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace CsvFormsApp
{
    public  class Period
    {
        public int Id { get; set; }
        public byte MonthId { get; set; }
        public string NameMonth { get; set; } = null!;
        public int Year { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public byte DayInMonth { get; set; }
        public bool IsActive { get; set; }
        public bool IsWorking { get; set; }
        public bool IsOpen { get; set; }
        public DateTime? DateOpen { get; set; }
        public bool? IsVisible { get; set; }
    }

    public class PeriodConfiguration : IEntityTypeConfiguration<Period>
    {
        public void Configure(EntityTypeBuilder<Period> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("List", "Period");
        }
    }
}
