using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domaine.MyEntities;


namespace DataAccess.ModelBuilderConfigs
{
    public class TirageLotoConfig : IEntityTypeConfiguration<TirageLoto>
    {
        public void Configure(EntityTypeBuilder<TirageLoto> entityModelBuilder)
        {
            entityModelBuilder.HasIndex(tirage => tirage.Date).IsUnique(); //Pas de doublon
        }
    }
}
