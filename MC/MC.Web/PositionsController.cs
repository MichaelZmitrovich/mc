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