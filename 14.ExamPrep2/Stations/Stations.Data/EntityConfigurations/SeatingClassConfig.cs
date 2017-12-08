using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfigurations
{
    public class SeatingClassConfig:IEntityTypeConfiguration<SeatingClass>
    {
        public void Configure(EntityTypeBuilder<SeatingClass> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasAlternateKey(sc => new {sc.Name, sc.Abbreviation});

            //abbreviation added as annotation 
        }
    }
}
