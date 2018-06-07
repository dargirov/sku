namespace Infrastructure.Data.Common
{
    public class EntityChange
    {
        public EntityChange(BaseTenantEntity entity, string property, string original, string current)
        {
            Entity = entity;
            Property = property;
            Original = original;
            Current = current;
        }

        public BaseTenantEntity Entity { get; private set; }
        public string Property { get; private set; }
        public string Original { get; private set; }
        public string Current { get; private set; }
    }
}
