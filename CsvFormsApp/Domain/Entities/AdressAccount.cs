using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvFormsApp.Domain.Entities
{
    public class AdressAccount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

    }

    public class AddressAccountConfiguration : IEntityTypeConfiguration<AdressAccount>
    {
        public void Configure(EntityTypeBuilder<AdressAccount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Adress.Account");
        }
    }
}
