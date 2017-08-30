using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cassandra;

namespace CassandraFinal {

    public class ConsoleContactManager {

        private const string IP_ADDRESS = "127.0.0.1";

        public void Run() {

        }

        private ISession Connect(string node) {
            return Cluster.Builder().AddContactPoint(node).Build().Connect();
        }
    }
}
