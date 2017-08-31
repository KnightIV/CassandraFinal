using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cassandra;

namespace CassandraFinal {

    public class CassandraTest {

        private const string IP_ADDRESS = "127.0.0.1";

        public void Run() {
            ISession curSession = Connect(IP_ADDRESS);
            curSession.CreateKeyspaceIfNotExists("test");
            curSession.ChangeKeyspace("test");
            //ExecuteQuery(curSession, "CREATE TABLE contacts (id int, name text, phones set<int>, email set<text>, group text, PRIMARY KEY (group, id));");
            TableTest(curSession);
        }

        private ISession Connect(string node) {
            return Cluster.Builder().AddContactPoint(node).Build().Connect();
        }

        private RowSet ExecuteQuery(ISession session, string cqlQuery) {
            return session.Execute(cqlQuery);
        }

        private void TableTest(ISession session) {
            ExecuteQuery(session, "INSERT INTO testTable (id, testtext) VALUES (1, \'bonjour\')");
            ExecuteQuery(session, "INSERT INTO testTable (id, testtext) VALUES (2, \'Hello\')");
            RowSet set = ExecuteQuery(session, "select * from testtable");
            foreach (Row row in set) {
                Console.WriteLine($"{row.GetValue<int>("id")} {row.GetValue<string>("testtext")}");
            }
        }

        private void ContactsTest(ISession session)
        {
            ExecuteQuery(session, "INSERT INTO testTable )
        }
    }
}
