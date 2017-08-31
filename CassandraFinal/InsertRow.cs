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
            foreach (string s in c.Emails)
            {
                query += "'" + s + "'";
            }
            query += "}); ";
            return query;
        }
    }
}
