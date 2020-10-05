using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ImportXMLToDB
{
    class SqlHelper
    {
        private static Dictionary<string, SqlParameter> typeMap;

        static SqlHelper()
        {
            typeMap = new Dictionary<string, SqlParameter>();
            typeMap["CARDCODE"] = new SqlParameter("@CARDCODE", SqlDbType.Decimal, 20);
            typeMap["STARTDATE"] = new SqlParameter("@STARTDATE", SqlDbType.Date);
            typeMap["FINISHDATE"] = new SqlParameter("@FINISHDATE", SqlDbType.Date);
            typeMap["LASTNAME"] = new SqlParameter("@LASTNAME", SqlDbType.NVarChar, 20);
            typeMap["FIRSTNAME"] = new SqlParameter("@FIRSTNAME", SqlDbType.NVarChar, 20);
            typeMap["SURNAME"] = new SqlParameter("@SURNAME", SqlDbType.VarChar, 20);
            typeMap["GENDER"] = new SqlParameter("@GENDER", SqlDbType.VarChar, 1);
            typeMap["BIRTHDAY"] = new SqlParameter("@BIRTHDAY", SqlDbType.Date);
            typeMap["PHONEHOME"] = new SqlParameter("@PHONEHOME", SqlDbType.VarChar, 20);
            typeMap["PHONEMOBIL"] = new SqlParameter("@PHONEMOBIL", SqlDbType.VarChar, 20);
            typeMap["EMAIL"] = new SqlParameter("@EMAIL", SqlDbType.VarChar, 50);
            typeMap["CITY"] = new SqlParameter("@CITY", SqlDbType.VarChar, 20);
            typeMap["STREET"] = new SqlParameter("@STREET", SqlDbType.VarChar, 20);
            typeMap["HOUSE"] = new SqlParameter("@HOUSE", SqlDbType.VarChar, 10);
            typeMap["APARTMENT"] = new SqlParameter("@APARTMENT", SqlDbType.Int);
        }

        public static SqlParameter GetSqlParameter(string giveSqlParameter)
        {
            if (typeMap.ContainsKey(giveSqlParameter))
            {
                return typeMap[giveSqlParameter];
            }

            throw new ArgumentException($"{giveSqlParameter} is not a supported SqlParameter");
        }
    }
}
