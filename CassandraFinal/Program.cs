﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cassandra;

namespace CassandraFinal {

    public class Program {

        public static void Main(string[] args) {
            ConsoleContactManager m = new ConsoleContactManager();
            m.Run();
        }
    }
}
