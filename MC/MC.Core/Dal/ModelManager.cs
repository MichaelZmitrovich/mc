using System.Runtime.Remoting.Messaging;
using System.Web;

namespace MC.Core.Dal
{
    public class ModelManager
    {
        private const string contextKey = "MODEL";

        public MCEntities Model
        {
            get
            {
                MCEntities result;
                result = (IsWeb ? HttpContext.Current.Items[contextKey] : CallContext.GetData(contextKey)) as MCEntities;
                if(result == null)
                {
                    result = new MCEntities();
                    if (this.IsWeb)
                    {
                        HttpContext.Current.Items[contextKey] = result;
                    }
                    else
                    {
                        CallContext.SetData(contextKey, result);
                    }
                }
                return result;
            }
        }

        private bool IsWeb
        {
            get { return HttpContext.Current != null; }
        }
    }
}

