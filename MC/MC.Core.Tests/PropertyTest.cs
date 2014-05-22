using System;
using System.Collections.Generic;
using System.Transactions;
using MC.Core.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MC.Core.Tests
{
    [TestClass]
    public class PropertyTest
    {
        [TestMethod]
        public void PropertySaveAndGet()
        {
            using (new TransactionScope())
            {
                PropertyDao dao = new PropertyDao();
                Property entity = new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "foo"
                };
                dao.Add(entity);
                dao.SaveChanges();

                dao.Detach(entity);

                Property writtenEntity = dao.GetById(entity.Id);
                Assert.AreEqual(entity.Name, writtenEntity.Name);
            }
        }

        [TestMethod]
        public void PropertyGetByPositionRange()
        {
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
            //                North-East
            //   +---------------+
            //   |               |   
            //   |               |
            //   |    * +40 -70  |         
            //   |               |
            //   |               |   
            //   +---------------+
            // South-West

            using (new TransactionScope())
            {
                PropertyDao dao = new PropertyDao();
                Property entity = new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "foo",
                    Latitude = 40,
                    Longitude = -70
                };
                dao.Add(entity);
                dao.SaveChanges();

                dao.Detach(entity);

                List<Property> list1 = dao.GetByPositionRange(40.1, -69.9, 39.9, -71.1);
                Assert.AreEqual(1, list1.Count);

                List<Property> list2 = dao.GetByPositionRange(39.9, -69.9, 39.8, -71.1);
                Assert.AreEqual(0, list2.Count);
            }
        }
    }
}
