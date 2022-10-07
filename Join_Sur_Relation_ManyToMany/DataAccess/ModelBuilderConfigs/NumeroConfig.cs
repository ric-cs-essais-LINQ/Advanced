using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domaine.MyEntities;


namespace DataAccess.ModelBuilderConfigs
{
    public class NumeroConfig : IEntityTypeConfiguration<Numero>
    {
        public void Configure(EntityTypeBuilder<Numero> entityModelBuilder)
        {
            entityModelBuilder.Property(numero => numero.Valeur).IsRequired();

            entityModelBuilder.HasIndex(numero => numero.Valeur).IsUnique(); //Pas de doublon

        }
    }
}
