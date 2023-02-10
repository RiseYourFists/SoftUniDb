using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionNames
{
    public class Minions
    {
        public Minions(SqlConnection connection)
        {
            Connection = connection;
        }

        public SqlConnection Connection { get; }

        public void AddMinion(string name, int age, string townName, string villainName)
        {
            var queryReaders = new List<QueryReader>() 
            { 
                new QueryReader("VillainSelectQuery"),
                new QueryReader("MinionSelectQuery"),
                new QueryReader("AddMinionQuery"),
                new QueryReader("AddTownQuery"),
                new QueryReader("AddVillainQuery"),
                new QueryReader("AddMinionToVillainQuery")
            };

            queryReaders.ForEach(x => x.InitializeReader());

            /*
             TODO:
             
             1. Checks:
                    minion exists
                    town exists
                    villain exists
             2. Actions:
                    if minion missing TODO: add
                    if town missing TODO: add
                    if villain missing TODO: add

             3. Add Minion to Villain
             */

        }
    }
}
