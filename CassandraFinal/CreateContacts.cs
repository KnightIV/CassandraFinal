using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraFinal
{
    public class CreateContacts
    {
        public void CreatAndInsertContacts()
        {
            List<String> names = new List<String> { "Elena", "Sam", "Ramone", "Anthony", "Mason", "Julie" };
            List<String> emailNames = new List<string> { "Penguin", "Elephant", "Cat", "Dog", "Bear" };
            List<String> groups = new List<string> { "Family", "Freinds", "Work", "Enemys" };
            Random r = new Random();

            for(int i = 0; i < 50; i++)
            {
                List<long> phones = new List<long>();
                List<String> emails = new List<String>();

                String name = names.ElementAt(r.Next(6));
                phones.Add(1000000000 + r.Next(100000000, 999999999));
                emails.Add(name + i + "gmail.com");
                String group = groups.ElementAt(r.Next(4));

                int multi = r.Next(2);

                if (multi == 1)
                {
                    int amount = r.Next(1, 4);
                    for(int j = 0; j < amount; j++)
                    {
                        phones.Add(1000000000 + r.Next(100000000, 999999999));
                    }
                }

                multi = r.Next(2);

                if (multi == 1)
                {
                    int amount = r.Next(1, 4);
                    for (int j = 0; j < amount; j++)
                    {
                        emails.Add(emailNames.ElementAt(r.Next(5)) + "@gmail.com");
                    }
                }

                Contact c = new Contact(name, phones, emails, group);

                InsertRow ir = new InsertRow();
                ir.Insert(c);

            }
        }
    }
}
