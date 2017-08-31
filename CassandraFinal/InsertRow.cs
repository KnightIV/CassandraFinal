using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraFinal
{
    class InsertRow
    {
        public string Insert(Contact c)
        {
            string query = "INSERT INTO contacts (name, phones, emails, group) VALUES('" + c.Name + "', {";
            foreach (int s in c.PhoneNumbers)
            {
                query += s.ToString();
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
