using AtonWebApi.Models;

namespace AtonWebApi.DAL
{
    public static class DatabaseMock
    {
        public static Dictionary<string, User> UsersTable;
        static DatabaseMock()
        {
            UsersTable = new Dictionary<string, User>
            {
                {
                    "MainAdmin",
                    new User("MainAdmin", "12345", "admin", 0, true, "MainAdmin")
                }
            };
            InitUsers();
        }

        static void InitUsers()
        {
            UsersTable.Add("UserJohn", new User("UserJohn", "john12345", "John", 0, false, "MainAdmin", new DateTime(2011, 4, 12)));
            UsersTable.Add("UserSam", new User("UserSam", "sam12345", "Sam", 0, true, "MainAdmin", new DateTime(1999, 8, 1)));
            UsersTable.Add("UserKate", new User("UserKate", "kate12345", "Kate", 1, true, "MainAdmin", new DateTime(2000, 9, 22)));
            UsersTable.Add("UserMike", new User("UserMike", "mike12345", "Mike", 0, false, "MainAdmin", new DateTime(1985, 11, 11)));
            UsersTable.Add("UserCassandra", new User("UserCassandra", "сassandra12345", "Cassandra", 1, false, "MainAdmin", new DateTime(1997, 2, 18)));
            UsersTable.Add("UserMartin", new User("UserMartin", "martin12345", "Martin", 0, true, "MainAdmin", new DateTime(2002, 7, 17)));
            UsersTable.Add("UserEvelyn", new User("UserEvelyn", "evelyn12345", "Evelyn", 2, false, "MainAdmin", new DateTime(2008, 10, 27)));
            UsersTable.Add("UserLora", new User("UserLora", "lora12345", "Lora", 1, false, "MainAdmin", new DateTime(1975, 2, 3)));
            UsersTable.Add("UserBulat", new User("UserBulat", "mraker2000", "Bulat", 0, true, "MainAdmin", new DateTime(2000, 3, 8)));
            UsersTable.Add("UserShanna", new User("UserShanna", "shanna12345", "Shanna", 1, false, "MainAdmin", new DateTime(2001, 9, 20)));
            UsersTable.Add("UserWilliam", new User("UserWilliam", "william12345", "William", 0, false, "MainAdmin", new DateTime(1986, 1, 1)));
            UsersTable.Add("UserLuke", new User("UserLuke", "skywalker12345", "Luke", 0, false, "MainAdmin", new DateTime(1919, 6, 15)));
            UsersTable.Add("UserMelissa", new User("UserMelissa", "melissa12345", "Melissa", 1, false, "MainAdmin", new DateTime(1985, 8, 24)));
            UsersTable.Add("UserAmelia", new User("Amelia", "amelia12345", "Amelia", 1, false, "MainAdmin", new DateTime(1996, 12, 13)));
            UsersTable.Add("UserOliver", new User("UserOliver", "oliver12345", "Oliver", 0, false, "MainAdmin", new DateTime(2004, 7, 19)));
            UsersTable.Add("UserErika", new User("UserErika", "erika12345", "Erika", 1, false, "MainAdmin", new DateTime(1992, 10, 27)));
            UsersTable.Add("UserAbner", new User("UserAbner", "abner12345", "Abner", 2, false, "MainAdmin", new DateTime(2003, 8, 21)));
            UsersTable.Add("UserRoger", new User("UserRoger", "happyRoger12345", "Roger", 0, true, "MainAdmin", new DateTime(1991, 1, 2)));
            UsersTable.Add("UserBruno", new User("UserBruno", "brunoBanani12345", "Bruno", 0, false, "MainAdmin", new DateTime(2010, 10, 23)));
            UsersTable.Add("UserPeter", new User("UserPeter", "parker12345", "Peter", 0, false, "MainAdmin", new DateTime(2000, 5, 31)));
        }

    }
}
