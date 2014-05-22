using System.Data.Entity.Infrastructure;
using System.Data.Objects;

namespace MC.Core.Dal
{
    public partial class MCEntities
    {
        public ObjectContext ObjectContext()
        {
            return (this as IObjectContextAdapter).ObjectContext;
        }
    }
}
