using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraFinal {

    public class Contact {

        public string Name { get; set; }
        public List<int> PhoneNumbers { get; set; }
        public List<string> Emails { get; set; }
        public string Group { get; set; }

        public Contact(string name, List<int> phoneNumbers, List<string> emails, string group) {
            Name = name;
            PhoneNumbers = phoneNumbers;
            Emails = emails;
            Group = group;
        }
    }
}