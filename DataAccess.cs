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
                    "EXISTS Collab (ID INTEGER PRIMARY KEY NOT NULL, " +
                    "Name NVARCHAR(2048) NOT NULL UNIQUE, " +
                    "Resume NVARCHAR(2048) NULL " +
                    ")";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Project (ID INTEGER PRIMARY KEY, " +
                    "Name NVARCHAR(2048) NOT NULL UNIQUE, " +
                    "Description NVARCHAR(2048) NULL " +
                    ")";

                createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Skill (ID INTEGER PRIMARY KEY, " +
                    "Name NVARCHAR(2048) NOT NULL UNIQUE, " +
                    "Description NVARCHAR(2048) NULL " +
                    ")";

                createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS T_Skills_Collabs (IDCollab INT NOT NULL REFERENCES Collab(ID) ON DELETE CASCADE," +
                    "IDSkill INT NOT NULL REFERENCES Skill(ID) ON DELETE CASCADE)";

                createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS T_Projects_Collabs (IDCollab INT NOT NULL REFERENCES Collab(ID) ON DELETE CASCADE," +
                    "IDProject INT NOT NULL REFERENCES Project(ID) ON DELETE CASCADE)";

                createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();

                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS T_Projects_Skills (IDProject INT NOT NULL REFERENCES Project(ID) ON DELETE CASCADE," +
                    "IDSkill INT NOT NULL REFERENCES Skill(ID) ON DELETE CASCADE)";

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
                        ("SELECT ID, Name, Resume from Collab", db);

                    SqliteDataReader query = selectCommand.ExecuteReader();

                    while (query.Read())
                    {
                        int currentID = query.GetInt32(0);
                        string nameSkill = "";
                        entries.Add(query.GetString(1) + "  |  " + query.GetString(2));
                        List<String> skillsList = GetSkillsListByCollabId(currentID);
                        string aggregateSkillList = "";
                        foreach (string skill in skillsList)
                        {
                            aggregateSkillList += skill;
                        }
                        entries.Add("Compétences : " + aggregateSkillList);
                        List<String> projectList = GetProjectListByCollabId(currentID);
                        string aggregateProjectList = "";
                        foreach (string project in projectList)
                        {
                            aggregateProjectList += project;
                        }
                        entries.Add("Projets : " + aggregateProjectList);
                        entries.Add("-----------------------------------");
                    }

                }

                return entries;

            }
        }

        public static int GetCollabByName(string name)
        {
            List<int> entries = new List<int>();
            int myId = -1;
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * FROM Collab WHERE Name = @name", db);
                selectCommand.Parameters.AddWithValue("@name", name);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    myId = query.GetInt32(0);
                }
            }

            return myId;

        }

        public static List<String> GetSkillsListByCollabId(int collabId)
        {
            List<String> skillsList = new List<String>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                List<int> idSkill = new List<int>();
                string nameSkill = "";
                SqliteCommand selectSecond = new SqliteCommand("SELECT IDSkill FROM T_Skills_Collabs WHERE IDCollab = @collabID", db);
                selectSecond.Parameters.AddWithValue("@collabID", collabId);
                SqliteDataReader querySecond = selectSecond.ExecuteReader();
                while (querySecond.Read())
                {
                    idSkill.Add(querySecond.GetInt32(0));
                }
                idSkill = idSkill.Distinct().ToList();
                foreach (int id in idSkill)
                {
                    SqliteCommand selectThird = new SqliteCommand("SELECT Name FROM Skill WHERE ID = @IDSkill", db);
                    selectThird.Parameters.AddWithValue("@IDSkill", id);
                    try
                    {
                        SqliteDataReader queryThird = selectThird.ExecuteReader();
                        while (queryThird.Read())
                        {
                            nameSkill = queryThird.GetString(0);
                            skillsList.Add(nameSkill + ", ");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return skillsList;

        }

        public static List<String> GetProjectListByCollabId(int collabId)
        {
            List<String> projectList = new List<String>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                List<int> idProject = new List<int>();
                string nameProject = "";
                SqliteCommand selectSecond = new SqliteCommand("SELECT IDProject FROM T_Projects_Collabs WHERE IDCollab = @collabID", db);
                selectSecond.Parameters.AddWithValue("@collabID", collabId);
                SqliteDataReader querySecond = selectSecond.ExecuteReader();
                while (querySecond.Read())
                {
                    idProject.Add(querySecond.GetInt32(0));
                }
                idProject = idProject.Distinct().ToList();
                foreach (int Id in idProject)
                {
                    SqliteCommand selectThird = new SqliteCommand("SELECT Name FROM Project WHERE ID = @IDProject", db);
                    selectThird.Parameters.AddWithValue("@IDProject", Id);
                    try
                    {
                        SqliteDataReader queryThird = selectThird.ExecuteReader();
                        while (queryThird.Read())
                        {
                            nameProject = queryThird.GetString(0);
                            projectList.Add(nameProject + ", ");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return projectList;

        }

        public static int GetProjectByName(string name)
        {
            int myId = -1;
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT ID FROM Project WHERE Name = @nameEntry", db);
                selectCommand.Parameters.AddWithValue("@nameEntry", name);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    myId = query.GetInt32(0);
                }
            }

            return myId;

        }
        public static int GetSkillByName(string name)
        {
            int myId = -1;
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT ID FROM Skill WHERE Name = @nameEntry", db);
                selectCommand.Parameters.AddWithValue("@nameEntry", name);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    myId = query.GetInt32(0);
                }
            }


            return myId;

        }


        public static void AddCollab(Collab myCollab, string[] skillName)
        {
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                  new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    if (CollabExist(myCollab.Name) == false)
                    {
                        SqliteCommand insertCommand = new SqliteCommand();
                        insertCommand.Connection = db;

                        // Use parameterized query to prevent SQL injection attacks
                        insertCommand.CommandText = "INSERT INTO Collab VALUES (NULL, @nameEntry, @resumeEntry);";
                        insertCommand.Parameters.AddWithValue("@nameEntry", myCollab.Name);
                        insertCommand.Parameters.AddWithValue("@resumeEntry", myCollab.Resume);

                        insertCommand.ExecuteReader();
                    }

                    if (skillName.Length != 0)
                    {
                        for (int i = 0; i < skillName.Length; i++)
                        {
                            bool isSkillExist = SkillExist(skillName[i]);
                            if (isSkillExist == false)
                            {
                                SqliteCommand insertCommandSkill = new SqliteCommand();
                                insertCommandSkill.Connection = db;

                                // Use parameterized query to prevent SQL injection attacks
                                insertCommandSkill.CommandText = "INSERT INTO Skill VALUES (NULL, @nameEntry, NULL);";
                                insertCommandSkill.Parameters.AddWithValue("@nameEntry", skillName[i]);


                                insertCommandSkill.ExecuteReader();
                            }

                            int skillId = GetSkillByName(skillName[i]);
                            int collabId = GetCollabByName(myCollab.Name);


                            SqliteCommand insertCommand = new SqliteCommand();
                            insertCommand.Connection = db;

                            insertCommand.CommandText = "INSERT INTO T_Skills_Collabs VALUES (@collabID, @skillID);";
                            insertCommand.Parameters.AddWithValue("@skillID", skillId);
                            insertCommand.Parameters.AddWithValue("@collabID", collabId);

                            insertCommand.ExecuteReader();
                        }
                    }
                }

            }
        }

        public static bool SkillExist(string skillName)
        {
            bool isExisting = false;
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                  new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    int skill = GetSkillByName(skillName);
                    if(skill == -1)
                    {
                        isExisting = false;
                    }
                    else
                    {
                        isExisting = true;
                    }

                }

            }
            return isExisting;
        }

        public static bool CollabExist(string collabName)
        {
            bool isExisting = false;
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                  new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    int collab = GetCollabByName(collabName);
                    if (collab == -1)
                    {
                        isExisting = false;
                    }
                    else
                    {
                        isExisting = true;
                    }

                }

            }
            return isExisting;
        }

        public static bool projectExist(string projectName)
        {
            bool isExisting = false;
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                  new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    int project = GetProjectByName(projectName);
                    if (project == -1)
                    {
                        isExisting = false;
                    }
                    else
                    {
                        isExisting = true;
                    }

                }

            }
            return isExisting;
        }

        public static void AddProject(Project myProject, string[] collabName)
        {
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                  new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    if (projectExist(myProject.Name) == false)
                    {
                        // Use parameterized query to prevent SQL injection attacks
                        insertCommand.CommandText = "INSERT INTO Project VALUES (NULL, @nameEntry, @descEntry);";
                        insertCommand.Parameters.AddWithValue("@nameEntry", myProject.Name);
                        insertCommand.Parameters.AddWithValue("@descEntry", myProject.Description);

                        insertCommand.ExecuteReader();
                    }

                    int projectId = GetProjectByName(myProject.Name);

                    for (int i = 0; i < collabName.Length; i++)
                    {
                        int collabId = GetCollabByName(collabName[i]);
                        insertCommand = new SqliteCommand();
                        insertCommand.Connection = db;


                        insertCommand.CommandText = "INSERT INTO T_Projects_Collabs VALUES (@collabID, @projectID);";
                        insertCommand.Parameters.AddWithValue("@collabID", collabId);
                        insertCommand.Parameters.AddWithValue("@projectID", projectId);

                        insertCommand.ExecuteReader();
                    }
                }

            }
        }

        public static void AddSkill(Skill mySkill, string[] collabName, string[] projectName)
        {
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
                using (SqliteConnection db =
                  new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    if (SkillExist(mySkill.Name) == false)
                    {
                        // Use parameterized query to prevent SQL injection attacks
                        insertCommand.CommandText = "INSERT INTO Skill VALUES (NULL, @nameEntry, @descEntry);";
                        insertCommand.Parameters.AddWithValue("@nameEntry", mySkill.Name);
                        insertCommand.Parameters.AddWithValue("@descEntry", mySkill.Description);

                        insertCommand.ExecuteReader();
                    }
                    int skillId = GetSkillByName(mySkill.Name);
                    //FIN
                    for (int i = 0; i < collabName.Length; i++)
                    {
                        int collabId = GetCollabByName(collabName[i]);

                        insertCommand = new SqliteCommand();
                        insertCommand.Connection = db;


                        insertCommand.CommandText = "INSERT INTO T_Skills_Collabs VALUES (@collabID, @skillID);";
                        insertCommand.Parameters.AddWithValue("@skillID", skillId);
                        insertCommand.Parameters.AddWithValue("@collabID", collabId);

                        insertCommand.ExecuteReader();
                    }
                    //FIN

                    for (int i = 0; i < projectName.Length; i++)
                    {
                        int projectId = GetProjectByName(projectName[i]);
                        insertCommand = new SqliteCommand();
                        insertCommand.Connection = db;


                        insertCommand.CommandText = "INSERT INTO T_Projects_Skills VALUES (@projectID, @skillID);";
                        insertCommand.Parameters.AddWithValue("@skillID", skillId);
                        insertCommand.Parameters.AddWithValue("@projectID", projectId);

                        insertCommand.ExecuteReader();
                    }
                }

            }

        }

        public static void SuppCollab(string collabName)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
                new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    insertCommand.CommandText = "DELETE FROM Collab WHERE Name = @collabName";
                    insertCommand.Parameters.AddWithValue("@collabName", collabName);
                    insertCommand.ExecuteReader();
                }

        }

    }
}
