using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;


namespace Skill_visualization
{
    public static class DataAccess
    {

        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            //var filePath = @"../myDatabase.db";
            //FileStream fs = new FileStream(filePath, FileMode.CreateNew);
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Collab (ID INTEGER PRIMARY KEY AUTO INCREMENT NOT NULL, " +
                    "Name NVARCHAR(2048) NOT NULL, " +
                    "Resume NVARCHAR(2048) NULL " +
                    ")";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Project (ID INTEGER PRIMARY KEY, " +
                    "Name NVARCHAR(2048) NULL, " +
                    "Description NVARCHAR(2048) NULL, " +
                    "Collab"
                    "FOREIGN KEY ("
                    ")";

                createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Skill (ID INTEGER PRIMARY KEY, " +
                    "Name NVARCHAR(2048) NULL, " +
                    "Description NVARCHAR(2048) NULL " +
                    ")";

                createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static List<String> GetCollabList()
        {
            {
                List<String> entries = new List<string>();

                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                   new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand selectCommand = new SqliteCommand
                        ("SELECT Name, Resume from Collab", db);

                    SqliteDataReader query = selectCommand.ExecuteReader();

                    while (query.Read())
                    {
                        entries.Add(query.GetString(0) + "   " + query.GetString(1));
                    }
                }

                return entries;

            }
        }

        public static string GetCollab()
        {
            string myString = "Hello";
            return myString;
        }

        public static void AddCollab(Collab myCollab)
        {
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                  new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    insertCommand.CommandText = "INSERT INTO Collab VALUES (NULL, @nameEntry, @resumeEntry);";
                    insertCommand.Parameters.AddWithValue("@nameEntry", myCollab.Name);
                    insertCommand.Parameters.AddWithValue("@resumeEntry", myCollab.Resume);

                    insertCommand.ExecuteReader();
                }

            }
        }

        public static void AddProject(Project myProject)
        {
            

        }

        public static void AddSkill(Skill mySkill)
        {
           

        }

    }
}
