using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using _1._4.Models;

namespace _1._4.Shared
{
    public class DAL
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ISLibrary"].ConnectionString);

        public static SqlDataReader executeProcedure(string commandName, Dictionary<int, object> dict)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ISLibrary"].ConnectionString);
            connection.Open();
            SqlCommand comm = connection.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = commandName;

            if (dict != null)
            {
                for (int i = 0; i < dict.Count; i = i+3)
                {
                    SqlParameter param = new SqlParameter();
                    object value;
                    bool valueTrue = dict.TryGetValue(i, out value);
                    if (valueTrue)
                    {
                    param.ParameterName = value.ToString();
                    }
                    valueTrue = dict.TryGetValue(i + 1, out value);
                    if (valueTrue)
                    {
                        param.Value = value.ToString();
                    }
                    valueTrue = dict.TryGetValue(i + 2, out value );
                    if (valueTrue)
                    {
                        SqlDbType type = (SqlDbType)value;
                        param.SqlDbType = type;
                    }

                    comm.Parameters.Add(param);

                }
                comm.Parameters.Add("@rowsAffected", SqlDbType.Int).Direction = ParameterDirection.Output;

            }
            
            return comm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static SqlDataReader executeProcedure(string commandName)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ISLibrary"].ConnectionString);
            connection.Open();
            SqlCommand comm = connection.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = commandName;
            
            return comm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using (SqlDataReader reader = executeProcedure("getAllBooks"))
            {
                while (reader.Read())
                {
                    Book book = new Book();
                    book.ID = int.Parse(reader[0].ToString());
                    book.Title = reader[1].ToString();
                    book.Author = reader[2].ToString();
                    book.ISBN = reader[3].ToString();
                    int checkOutId;
                    bool isCheckedOut = int.TryParse(reader[4].ToString(), out checkOutId);
                    if (isCheckedOut)
                    {
                        book.IsBorrowed = isCheckedOut;
                        book.CheckedOut = new Checkout();
                        book.CheckedOut.UserId = int.Parse(reader[6].ToString());
                        book.CheckedOut.CheckoutDate = DateTime.Parse(reader[7].ToString());

                    }
                    if (reader[8].ToString().Length > 0)
                    {
                        book.IsBorrowed = false;
                    }
                    books.Add(book);
                }
            }

            books = books.GroupBy(x => x.ID).Select(x => x.Last()).ToList();

            return books;

        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (SqlDataReader reader = executeProcedure("getAllUsers"))
            {
                while (reader.Read())
                {
                    User user = new User();
                    user.UserNo = int.Parse(reader[0].ToString());
                    user.Name = reader[1].ToString();
                    users.Add(user);
                }
            }
            return users;
        }

        public int CreateUser(User user)
        {
            int returnValue = 0;
            Dictionary<int, object> param = new Dictionary<int, object>();
            param.Add(0, "@userName");
            param.Add(1, user.Name);
            param.Add(2, SqlDbType.NVarChar);
            using (SqlDataReader reader = executeProcedure("createNewUser", param))
            {
                while (reader.Read())
                {
                    returnValue = int.Parse(reader[0].ToString());
                }
            }
            return returnValue;
        }

        public int checkOutBook(int bookID, int userID)
        {
            int returnValue = 0;

            DateTime checkOut = DateTime.Now;
            string sqlFormattedDate = checkOut.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string[] paramnames = { "@bookID", "@userID", "@datetime" };
            string[] paramStrings = { bookID.ToString(), userID.ToString(), sqlFormattedDate };
            SqlDbType[] types = { SqlDbType.Int, SqlDbType.Int, SqlDbType.DateTime };
            Dictionary<int, object> paramValues = createDictionary(paramnames, paramStrings, types);

            using (SqlDataReader reader = executeProcedure("checkOutBook", paramValues))
            {
                while (reader.Read())
                {
                    returnValue = int.Parse(reader[0].ToString());
                }
            }
            return returnValue;
        }

        public int checkInBook(int bookID)
        {
            int returnValue = 0;

            DateTime checkOut = DateTime.Now;
            string sqlFormattedDate = checkOut.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string[] paramnames = { "@bookID",  "@datetime" };
            string[] paramStrings = { bookID.ToString(), sqlFormattedDate };
            SqlDbType[] types = { SqlDbType.Int, SqlDbType.DateTime };
            Dictionary<int, object> paramValues = createDictionary(paramnames, paramStrings, types);

            using (SqlDataReader reader = executeProcedure("checkInBook", paramValues))
            {
                while (reader.Read())
                {
                    returnValue = int.Parse(reader[0].ToString());
                }
            }
            return returnValue;
        }

        public int createNewBook(Book book)
        {
            int returnValue = 0;


            string[] paramnames = { "@title", "@Author", "@ISBN" };
            string[] paramStrings = { book.Title, book.Author, book.ISBN };
            SqlDbType[] types = { SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.NVarChar };

            Dictionary<int, object> paramValues = createDictionary(paramnames, paramStrings, types);

            using (SqlDataReader reader = executeProcedure("createNewBook", paramValues))
            {
                while (reader.Read())
                {
                    returnValue = int.Parse(reader[0].ToString());
                }
            }
            return returnValue;
        }

        private static Dictionary<int, object> createDictionary(string[] paramnames, string[] paramStrings, SqlDbType[] types)
        {
            Dictionary<int, object> paramValues = new Dictionary<int, object>();
            int dictKey = 0;

            for (int i = 0; i < paramStrings.Length; i++)
            {
                paramValues.Add(dictKey, paramnames[i]);
                dictKey++;
                paramValues.Add(dictKey, paramStrings[i]);
                dictKey++;
                paramValues.Add(dictKey, types[i]);
                dictKey++;
            }

            return paramValues;
        }
    }
}