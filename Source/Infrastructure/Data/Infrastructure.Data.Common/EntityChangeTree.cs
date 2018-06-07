using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Common
{
    public class EntityChangeTree
    {
        private readonly IList<EntityChange> _entity;
        private readonly IList<EntityChangeTree> _children;

        public EntityChangeTree()
        {
            _entity = new List<EntityChange>();
            _children = new List<EntityChangeTree>();
        }

        private EntityChangeTree(IList<EntityChange> entityChanges)
        {
            _entity = entityChanges;
            _children = new List<EntityChangeTree>();
        }

        public IList<EntityChange> Entity { get { return _entity; } }
        public IList<EntityChangeTree> Children { get { return _children; } }

        public void Add(IList<EntityChange> entityChanges, bool isBaseEntity)
        {
            if (isBaseEntity)
            {
                AddBase(entityChanges);
            }
            else
            {
                AddChild(entityChanges);
            }
        }

        private void AddChild(IList<EntityChange> entityChanges) => _children.Add(new EntityChangeTree(entityChanges));

        private void AddBase(IList<EntityChange> entityChanges) => entityChanges.ToList().ForEach(x => _entity.Add(x));
    }
}
