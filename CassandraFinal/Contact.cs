using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraFinal {

    public class Contact {

        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<long> PhoneNumbers { get; set; }
        public List<string> Emails { get; set; }
        public string Group { get; set; }

        // Used by Cassandra driver to populate properties
        public Contact() { }

        public Contact(string name, List<long> phoneNumbers, List<string> emails, string group) {
            ID = Guid.NewGuid();
            Name = name;
            PhoneNumbers = phoneNumbers;
            Emails = emails;
            Group = group;
        }

        public override string ToString() {
            string contact = $"ID: {ID}\nName: {Name}Group: {Group}\nPhone numbers:";
            foreach (long number in PhoneNumbers) {
                contact += $"\n\t{number}";
            }
            contact += "\nEmails:";
            foreach (string email in Emails) {
                contact += $"\n\t{email}";
            }
            return contact;
        }
    }
}