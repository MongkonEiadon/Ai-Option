using System;
using System.Collections.Generic;
using System.Text;
using iqoption.data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iqoption.data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<PersonDto>
    {
        public void Configure(EntityTypeBuilder<PersonDto> builder)
        {
        }
    }
}
