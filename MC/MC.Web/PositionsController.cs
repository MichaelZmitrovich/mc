using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using MC.Core.Dal;

namespace MC.Web
{
    public class PositionsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        public IEnumerable<Position> Get(double north, double east, double south, double west)
        {
            // need to call DB twice. workaround for now 
            if (west > east)
            {
                west = -180;
            }

            PropertyDao dao = new PropertyDao();
            List<Position> data =
                dao.GetByPositionRange(north, east, south, west)
                .Select(r => new Position { Lat = r.Latitude.Value, Lng = r.Longitude.Value })
                .ToList();

            return data;
        }

        private string ConvertToJson(List<Position> data)
        {
            return new JavaScriptSerializer().Serialize(data);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class Position
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

}
/*
568f97b5-0fad-4d91-bd08-92c28e1205d9	NULL	49.7	-73.9	568f97b5-0fad-4d91-bd08-92c28e1205d9
ed202ff7-805b-4813-be42-94afd28e5f46	NULL	31.60	-86.11	568f97b5-0fad-4d91-bd08-92c28e1205d9
05b88dbb-ce6a-4768-9e7a-9b8c914da576	NULL	48.6	-72.8	568f97b5-0fad-4d91-bd08-92c28e1205d9
cabb7183-8fe1-44f0-9951-c2588269712e	NULL	47.6	-71.71	568f97b5-0fad-4d91-bd08-92c28e1205d9
*/