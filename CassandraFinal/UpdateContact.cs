using Cassandra;
using Cassandra.Data.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraFinal {

    public class UpdateContact {

        private ConsoleContactManager contactManager;

        public UpdateContact(ConsoleContactManager manager) {
            contactManager = manager;
        }

        public void ContactUpdate() {
            Contact contactToEdit = null;
            string[] options = new string[] {
                "Name", "Phone numbers", "Emails", "Group"
            };

            string name = ConsoleIO.PromptForInput("Input the name of the contact you wish to edit: ", false);
            ISession session = contactManager.Connect(ConsoleContactManager.IP_ADDRESS);
            Table<Contact> contacts = new Table<Contact>(session);
            IEnumerable<Contact> contactsMatched = contacts.Where(c => c.Name.ToLower() == name.ToLower()).Execute();
            if (contactsMatched.Count() > 1) {
                contactToEdit = contactsMatched.ToArray()[ConsoleIO.PromptForMenuSelection("Choose your desired contact: ", contactsMatched.ToArray(), false)];
            } else if (contactsMatched.Count() == 0) {
                Console.WriteLine("\nThere were no contacts that matched that name.\n");
                return;
            } else {
                contactToEdit = contactsMatched.FirstOrDefault();
            }

            switch (ConsoleIO.PromptForMenuSelection("Choose a category to edit: ", options, true, "Exit to the main menu") - 1) {
                case -1:
                    // go to main menu
                    return;

                case 0:
                    // name
                    contactToEdit.Name = ConsoleIO.PromptForInput("Please enter a new name: ", false);
                    session.Execute($"INSERT INTO contacts (id, name) values ({contactToEdit.ID}, \'{contactToEdit.Name}\')");
                    break;

                case 1:
                    // add or remove a phonenumber
                    contactToEdit.PhoneNumbers.RemoveAt(ConsoleIO.PromptForMenuSelection("Select a phone number to remove: ", contactToEdit.PhoneNumbers.ToArray(), true, "Exit") - 1);
                    break;

                case 2:
                    // add or remove an email
                    break;

                case 3:
                    // change group
                    break;
            }
        }
    }
}
