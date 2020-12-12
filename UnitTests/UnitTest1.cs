using System.Collections.Generic;
using System.Linq;
using SCB.Client;
using SCB.Domain;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {

        [Fact]
        public void Test1()
        {
            SCBClient reader = new SCBClient("api.scb.se/OV0104");
            reader.ReadTable();

        }

        [Fact]
        public void TestReadTable()
        {
            SCBClient reader = new SCBClient("api.scb.se/OV0104");

            SCBNode node = new SCBNode
            {
                path = "http://api.scb.se/OV0104/v1/doris/en/ssd/BE/BE0401/BE0401A/BefolkPrognRevN",
                type = "t",
                id = "BefProgFoddaMedel19"
            };

            SCBMetaData meta = reader.GetMetaData(node.path).Result;

            SCBQuery query = new SCBQuery { response = new SCBResponse("json") };

            query.SetUp(meta.variables);

            query.SetSelection(meta.variables.Find(s=>s.code == "Fodelseregion"),SCBQuery.Filter.item, new List<string>(){"13"});
            query.SetSelection(meta.variables.Find(s => s.code == "Kon"), SCBQuery.Filter.item, new List<string>() { "1" });
            query.SetSelection(meta.variables.Find(s => s.code == "Tid"), SCBQuery.Filter.item, new List<string>() { "2020" });

            SCBTable table = reader.GetTable(node, query).Result;



        }

        [Fact]
        public void TestDrillDown()
        {
            SCBClient reader = new SCBClient("api.scb.se/OV0104");

            SCBNode node = new SCBNode
            {
                path = "http://api.scb.se/OV0104/v1/doris/en/ssd/",
                type = "l",
                id = ""
            };

            while (node.type == "l")
            {
                List<SCBNode> nodes = reader.GetNodesBelow(node).Result;
                node = nodes.First();
            }

            if (node.type == "t")
            {
                SCBMetaData meta = reader.GetMetaData(node.path).Result;

                SCBQuery query = new SCBQuery();
                

            }

        }

        [Fact]
        public void TestForEquality()
        {
                SCBValue val1 = new SCBValue("ok1", "SomeText1");
                SCBValue val2 = new SCBValue("ok2", "SomeText2");
                SCBValue val3 = new SCBValue("ok3", "SomeText3");

                SCBKey key1 = new SCBKey(new List<SCBValue>(){val1,val2,val3});

                SCBKey key2 = new SCBKey(new List<SCBValue>() { val1, val2, val3 });

                SCBKey key3 = new SCBKey(new List<SCBValue>() { val1, val1, val3 });

                Assert.True(key1.Equals(key2));

                Assert.True(!key1.Equals(key3));
        }

    }
}
