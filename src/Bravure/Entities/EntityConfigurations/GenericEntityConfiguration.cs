using Bravure.Entities.Abstractions;

namespace Bravure.Entities.EntityConfigurations
{
    public class GenericEntityConfiguration<T> : BaseEntityConfiguration<T> where T : BaseEntity
    {
        public override void Configure()
        {  }
    }
}
