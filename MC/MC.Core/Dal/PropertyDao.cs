using System;
using System.Collections.Generic;
using System.Linq;
using MC.Core.Dal;

namespace KZ.MoreCottages.Core.Dal
{
    public class PropertyDao : DaoBase<Property>
    {
        public Property GetById(Guid id)
        {
            var query = from property in Model.Properties where property.Id == id select property;
            return query.First();
        }

        public List<Property> GetByPositionRange(double north, double east, double south, double west)
        {
            //
            //                North-East
            //   +---------------+
            //   |               |+90
            //   |               |
            //   |               | Latitude
            //   | -180     +180 |
            //   |   Longitude   |-90
            //   +---------------+
            // South-West
            //
            var query = from property in Model.Properties 
                        where 
                            property.Latitude <= north &&  property.Latitude >= south &&
                            property.Longitude <= east && property.Longitude >= west
                        select property;

            return query.ToList();            
        }
    }
}
