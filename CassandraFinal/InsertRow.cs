using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraFinal
{
    public class InsertRow
    {
        public string Insert(Contact c)
        {
            string query = "INSERT INTO contacts (name, phones, emails, group) VALUES('" + c.Name + "', {";
            for (int i = 0; i < c.PhoneNumbers.Count; i++)
            {
                if(c.PhoneNumbers.Count > 1)
                {
                    if (i == (c.PhoneNumbers.Count - 1))
                    {

                    query += c.PhoneNumbers[i];
                    }
                    else
                    {
                        query += c.PhoneNumbers[i] + ",";

                    }
                }
                else
                {
                    query += c.PhoneNumbers[0];
                }
            }
            query += "}, {";
            for (int i = 0; i < c.Emails.Count; i++)
            {
                if (c.Emails.Count > 1)
                {
                    if (i == (c.Emails.Count - 1))
                    {

                        query += c.Emails[i];
                    }
                    else
                    {
                        query += c.Emails[i] + ",";

                    }
                }
                else
                {
                    query += c.Emails[0];
                }
            }
            query += "}); ";
            return query;
        }
    }
}
