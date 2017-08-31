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

            string nums = "";
            foreach (int i in c.PhoneNumbers)
            {
                nums += i + ",";
            }
            nums = nums.Remove(nums.LastIndexOf(','));
            query += nums;

            query += "}, {";

            string mails = "";
            foreach (string i in c.Emails)
            {
                mails += "\'" + i + "\',";
            }
            mails = mails.Remove(mails.LastIndexOf(','));
            query += mails;
            query += "}); ";
            return query;
        }
    }
}
