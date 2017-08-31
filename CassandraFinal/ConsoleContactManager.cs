using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cassandra;

namespace CassandraFinal {

    public class ConsoleContactManager {

        public const string IP_ADDRESS = "127.0.0.1";

        public void Run() {
            ISession session = Connect(IP_ADDRESS);
            session.CreateKeyspaceIfNotExists("test");
            session.ChangeKeyspace("test");
            ExecuteQuery(session, "CREATE TABLE IF NOT EXISTS contacts (id uuid, name text, phones set<int>, emails set<text>, group text, PRIMARY KEY (group, id));");
            //TestCreateAndInsertContacts(session);
        }

        public ISession Connect(string node) {
            return Cluster.Builder().AddContactPoint(node).Build().Connect();
        }

        public string Insert(Contact c) {
            string query = "INSERT INTO contacts (id, name, group, phones, emails) VALUES(" + c.ID + ", '" + c.Name + "', '" + c.Group + "', {";

            string nums = "";
            foreach (int i in c.PhoneNumbers) {
                nums += i + ",";
            }
            nums = nums.Remove(nums.LastIndexOf(','));
            query += nums;

            query += "}, {";

            string mails = "";
            foreach (string i in c.Emails) {
                mails += "'" + i + "',";
            }
            mails = mails.Remove(mails.LastIndexOf(','));
            query += mails;
            query += "});";
            return query;
        }

        public void TestCreateAndInsertContacts(ISession session) {
            List<String> names = new List<String> { "Elena", "Sam", "Ramone", "Anthony", "Mason", "Julie" };
            List<String> emailNames = new List<string> { "Penguin", "Elephant", "Cat", "Dog", "Bear" };
            List<String> groups = new List<string> { "Family", "Freinds", "Work", "Enemys" };
            Random r = new Random();

            for (int i = 0; i < 50; i++) {
                List<long> phones = new List<long>();
                List<String> emails = new List<String>();

                String name = names[r.Next(6)];
                phones.Add(1000000000 + r.Next(100000000, 999999999));
                emails.Add(name + i + "gmail.com");
                String group = groups[r.Next(4)];

                int multi = r.Next(2);

                if (multi == 1) {
                    int amount = r.Next(1, 4);
                    for (int j = 0; j < amount; j++) {
                        phones.Add(1000000000 + r.Next(100000000, 999999999));
                    }
                }

                multi = r.Next(2);

                if (multi == 1) {
                    int amount = r.Next(1, 4);
                    for (int j = 0; j < amount; j++) {
                        emails.Add(emailNames[r.Next(5)] + "@gmail.com");
                    }
                }

                Contact c = new Contact(name, phones, emails, group);

                string query = Insert(c);
                ExecuteQuery(session, query);
            }
        }

        private RowSet ExecuteQuery(ISession session, string cqlQuery) {
            return session.Execute(cqlQuery);
        }
    }
}
