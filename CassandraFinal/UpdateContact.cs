using Cassandra;
using Cassandra.Data.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                    string[] removeAddOptions = new string[] {
                        "Remove", "Add"
                    };
                    int choice = ConsoleIO.PromptForMenuSelection("Choose a course of action: ", removeAddOptions, true, "Go back");
                    switch (choice) {
                        case 0:
                            int numberChoice = ConsoleIO.PromptForMenuSelection("Select a phone number to remove: ", contactToEdit.PhoneNumbers.ToArray(), true, "Exit") - 1;
                            if (numberChoice == -1) break;
                            long numberToRemove = contactToEdit.PhoneNumbers[numberChoice];
                            contactToEdit.PhoneNumbers.Remove(numberToRemove);
                            break;

                        case 1:
                            long numToAdd = ConsoleIO.PromptForLong("Enter the number to add: ", "Invalid phone number.", 0);
                            contactToEdit.PhoneNumbers.Add(numToAdd);
                            break;
                    }
                    break;

                case 2:
                    // add or remove an email
                    string[] emailRemoveAddOptions = new string[] {
                        "Remove", "Add"
                    };
                    int emailOption = ConsoleIO.PromptForMenuSelection("Choose a course of action: ", emailRemoveAddOptions, true, "Go back");
                    switch (emailOption) {
                        case 0:
                            int emailChoice = ConsoleIO.PromptForMenuSelection("Select an email to remove: ", contactToEdit.Emails.ToArray(), true, "Exit") - 1;
                            if (emailChoice == -1) break;
                            string emailToRemove = contactToEdit.Emails[emailChoice];
                            contactToEdit.Emails.Remove(emailToRemove);
                            break;

                        case 1:
                            Regex emailCheck = new Regex(@"^(.+?\.?)+?@.+?\..+?$");
                            string email = String.Empty;
                            while (true) {
                                email = ConsoleIO.PromptForInput("Type a new email: ", false);
                                if (!emailCheck.IsMatch(email)) {
                                    Console.WriteLine("\nInvalid email\n");
                                    continue;
                                }
                                break;
                            }
                            contactToEdit.Emails.Add(email);
                            break;
                    }
                    break;

                case 3:
                    // change group
                    contactToEdit.Group = ConsoleIO.PromptForInput("Select a new group for this contact: ", false);
                    break;
            }

            // update contact

        }
    }
}
