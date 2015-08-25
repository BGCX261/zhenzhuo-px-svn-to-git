using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.IO;
using TrainEngine.Core.DataStore;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TrainEngine.Core.Classes;

namespace TrainEngine.Core.Providers
{
    /// <summary>
    /// Generic Database TrainProvider
    /// </summary>
    public partial class DbTrainProvider: TrainProvider
    {
        private string connStringName;
        private string tablePrefix;
        private string parmPrefix;

        /// <summary>
        /// Initializes the provider
        /// </summary>
        /// <param name="name">Configuration name</param>
        /// <param name="config">Configuration settings</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (String.IsNullOrEmpty(name))
            {
                name = "DbTrainProvider";
            }

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Generic Database Train Provider");
            }

            base.Initialize(name, config);

            if (config["connectionStringName"] == null)
            {
                // default to TrainEngine
                config["connectionStringName"] = "TrainEngine";
            }
            connStringName = config["connectionStringName"];
            config.Remove("connectionStringName");

            if (config["tablePrefix"] == null)
            {
                // default
                config["tablePrefix"] = "be_";
            }
            tablePrefix = config["tablePrefix"];
            config.Remove("tablePrefix");

            if (config["parmPrefix"] == null)
            {
                // default
                config["parmPrefix"] = "@";
            }
            parmPrefix = config["parmPrefix"];
            config.Remove("parmPrefix");

            // Throw an exception if unrecognized attributes remain
            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized attribute: " + attr);
            }
        }
        #region 内训
        /// <summary>
        /// Returns a Post based on Id
        /// </summary>
        /// <param name="id">PostID</param>
        /// <returns>post</returns>
        public override Training SelectTraining(Guid id)
        {
            Training training = new Training();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT TrainingID,Title,TrainingContent,DateCreated,DateModified,Author,IsPublished,Days,Teacher,ViewCount,IsGold " +
                                "FROM " + tablePrefix + "Trainings " +
                                "WHERE TrainingID = " + parmPrefix + "id";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    
                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "id";
                    dpID.Value = id.ToString();
                    cmd.Parameters.Add(dpID);

                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();

                            training.Id = rdr.GetGuid(0);
                            training.Title = rdr.GetString(1);
                            training.Content = rdr.GetString(2);
                            //training.Description = rdr.IsDBNull(2) ? String.Empty : rdr.GetString(2);
                            if (!rdr.IsDBNull(3))
                                training.DateCreated = rdr.GetDateTime(3);
                            if (!rdr.IsDBNull(4))
                                training.DateModified = rdr.GetDateTime(4);
                            if (!rdr.IsDBNull(5))
                                training.Author = rdr.GetString(5);
                            if (!rdr.IsDBNull(6))
                                training.IsPublished = rdr.GetBoolean(6);
                            
                            training.Days = rdr.GetInt32(7);
                            training.Teacher = rdr.GetString(8);
                            training.ViewCount = rdr.GetInt32(9);
                            if (!rdr.IsDBNull(10))
                                training.IsGold = rdr.GetBoolean(10);
                        }
                    }

                    // Tags
                    sqlQuery = "SELECT Tag " +
                                "FROM " + tablePrefix + "Tags " +
                                "WHERE ParentID = " + parmPrefix + "id and ParentType=1";
                    cmd.CommandText = sqlQuery;
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                                training.Tags.Add(rdr.GetString(0));
                        }
                    }
                    training.Tags.MarkOld();

                    // Categories
                    sqlQuery = "SELECT CategorieID " +
                                "FROM " + tablePrefix + "TrainingCategorie " +
                                "WHERE TrainingID = " + parmPrefix + "id";
                    cmd.CommandText = sqlQuery;
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Guid key = rdr.GetGuid(0);
                            if (Category.GetCategory(key) != null)
                                training.Categories.Add(Category.GetCategory(key));
                        }
                    }

                    // Fields
                    sqlQuery = "SELECT FieldID " +
                                "FROM " + tablePrefix + "TrainingField " +
                                "WHERE TrainingID = " + parmPrefix + "id";
                    cmd.CommandText = sqlQuery;
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Guid key = rdr.GetGuid(0);
                            if (Field.GetField(key) != null)
                                training.Fields.Add(Field.GetField(key));
                        }
                    }

                    // Comments
                    sqlQuery = "SELECT CommentID,CommentDate,Author,Count,Sex,Phone,Mobile,Email, " +
                                "Company,QQ_msn,Comment,IsDispose,DisposeBy,Country,IP " +
                                "FROM " + tablePrefix + "Comments " +
                                "WHERE ParentID = " + parmPrefix + "id and ParentType = 1";
                    cmd.CommandText = sqlQuery;
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Comment comment = new Comment();
                            comment.Id = rdr.GetGuid(0);
                            comment.IsApproved = false;
                            comment.DateCreated = rdr.GetDateTime(1);
                            comment.Author = rdr.GetString(2);
                            comment.Count = rdr.GetInt32(3);
                            comment.Sex = rdr.GetBoolean(4);
                            if (!rdr.IsDBNull(5))
                                comment.Phone = rdr.GetString(5);
                            comment.Mobile = rdr.GetString(6);
                            if (!rdr.IsDBNull(7))
                                comment.Email = rdr.GetString(7);
                            comment.Company = rdr.GetString(8);
                            if (!rdr.IsDBNull(9))
                                comment.QQ_msn = rdr.GetString(9);
                            comment.Content = rdr.GetString(10);
                            if (!rdr.IsDBNull(11))
                                comment.IsApproved = rdr.GetBoolean(11);
                            if (!rdr.IsDBNull(10))
                                comment.ModeratedBy = rdr.GetString(12);
                            if (!rdr.IsDBNull(6))
                                comment.Country = rdr.GetString(6);
                            if (!rdr.IsDBNull(7))
                                comment.IP = rdr.GetString(7);

                            comment.ParentId = id;
                            comment.ParentType = 1;
                            comment.Parent = training;

                            training.Comments.Add(comment);
                        }
                    }
                    training.Comments.Sort();

                    
                }
            }

            return training;
        }

        /// <summary>
        /// Adds a new post to database
        /// </summary>
        /// <param name="training">new post</param>
        public override void InsertTraining(Training training)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO " + tablePrefix +
                        "Trainings (TrainingID,Title,TrainingContent,DateCreated, " +
                        "DateModified,Author,IsPublished,Days,Teacher,ViewCount,IsGold) " +
                        "VALUES (@id, @title, @content, @created, @modified, " +
                        "@author, @published,@days,@teacher,@ViewCount,@IsGold)";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "id";
                    dpID.Value = training.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpTitle = provider.CreateParameter();
                    dpTitle.ParameterName = parmPrefix + "title";
                    dpTitle.Value = training.Title;
                    cmd.Parameters.Add(dpTitle);

                    DbParameter dpContent = provider.CreateParameter();
                    dpContent.ParameterName = parmPrefix + "content";
                    dpContent.Value = training.Content;
                    cmd.Parameters.Add(dpContent);

                    DbParameter dpCreated = provider.CreateParameter();
                    dpCreated.ParameterName = parmPrefix + "created";
                    dpCreated.Value = training.DateCreated.AddHours(-TrainSettings.Instance.Timezone);
                    cmd.Parameters.Add(dpCreated);

                    DbParameter dpModified = provider.CreateParameter();
                    dpModified.ParameterName = parmPrefix + "modified";
                    if (training.DateModified == new DateTime())
                        dpModified.Value = DateTime.Now;
                    else
                        dpModified.Value = training.DateModified.AddHours(-TrainSettings.Instance.Timezone);
                    cmd.Parameters.Add(dpModified);

                    DbParameter dpAuthor = provider.CreateParameter();
                    dpAuthor.ParameterName = parmPrefix + "author";
                    dpAuthor.Value = training.Author ?? "";
                    cmd.Parameters.Add(dpAuthor);

                    DbParameter dpPublished = provider.CreateParameter();
                    dpPublished.ParameterName = parmPrefix + "published";
                    dpPublished.Value = training.IsPublished;
                    cmd.Parameters.Add(dpPublished);

                    DbParameter dpDays = provider.CreateParameter();
                    dpDays.ParameterName = parmPrefix + "days";
                    dpDays.Value = training.Days;
                    cmd.Parameters.Add(dpDays);

                    DbParameter dpTeacher = provider.CreateParameter();
                    dpTeacher.ParameterName = parmPrefix + "teacher";
                    dpTeacher.Value = training.Teacher.ToString();
                    cmd.Parameters.Add(dpTeacher);

                    DbParameter dpViewCount = provider.CreateParameter();
                    dpViewCount.ParameterName = parmPrefix + "ViewCount";
                    dpViewCount.Value = training.ViewCount;
                    cmd.Parameters.Add(dpViewCount);

                    DbParameter dpIsGold = provider.CreateParameter();
                    dpIsGold.ParameterName = parmPrefix + "IsGold";
                    dpIsGold.Value = training.IsGold;
                    cmd.Parameters.Add(dpIsGold);

                    cmd.ExecuteNonQuery();
                }

                // Tags
                UpdateTags(training, conn, provider);

                // Categories
                UpdateCategories(training, conn, provider);

                //Fields
                UpdateFields(training, conn, provider);

                // Comments
                UpdateComments(training, conn, provider);

            }
        }

        /// <summary>
        /// Saves and existing post in the database
        /// </summary>
        /// <param name="post">post to be saved</param>
        public override void UpdateTraining(Training training)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "UPDATE " + tablePrefix + "Trainings " +
                                  "SET Title = @title, TrainingContent = @content, " +
                                  "DateCreated = @created, DateModified = @modified, Author = @Author, " +
                                  "IsPublished = @published ,Days= @days,Teacher= @teacher,ViewCount=@ViewCount,IsGold=@IsGold " +
                                  "WHERE TrainingID = @id";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "id";
                    dpID.Value = training.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpTitle = provider.CreateParameter();
                    dpTitle.ParameterName = parmPrefix + "title";
                    dpTitle.Value = training.Title;
                    cmd.Parameters.Add(dpTitle);

                    DbParameter dpContent = provider.CreateParameter();
                    dpContent.ParameterName = parmPrefix + "content";
                    dpContent.Value = training.Content;
                    cmd.Parameters.Add(dpContent);

                    DbParameter dpCreated = provider.CreateParameter();
                    dpCreated.ParameterName = parmPrefix + "created";
                    dpCreated.Value = training.DateCreated.AddHours(-TrainSettings.Instance.Timezone);
                    cmd.Parameters.Add(dpCreated);

                    DbParameter dpModified = provider.CreateParameter();
                    dpModified.ParameterName = parmPrefix + "modified";
                    if (training.DateModified == new DateTime())
                        dpModified.Value = DateTime.Now;
                    else
                        dpModified.Value = training.DateModified.AddHours(-TrainSettings.Instance.Timezone);
                    cmd.Parameters.Add(dpModified);

                    DbParameter dpAuthor = provider.CreateParameter();
                    dpAuthor.ParameterName = parmPrefix + "author";
                    dpAuthor.Value = training.Author ?? "";
                    cmd.Parameters.Add(dpAuthor);

                    DbParameter dpPublished = provider.CreateParameter();
                    dpPublished.ParameterName = parmPrefix + "published";
                    dpPublished.Value = training.IsPublished;
                    cmd.Parameters.Add(dpPublished);

                    DbParameter dpDays = provider.CreateParameter();
                    dpDays.ParameterName = parmPrefix + "days";
                    dpDays.Value = training.Days;
                    cmd.Parameters.Add(dpDays);

                    DbParameter dpTeacher = provider.CreateParameter();
                    dpTeacher.ParameterName = parmPrefix + "teacher";
                    dpTeacher.Value = training.Teacher.ToString();
                    cmd.Parameters.Add(dpTeacher);

                    DbParameter dpViewCount = provider.CreateParameter();
                    dpViewCount.ParameterName = parmPrefix + "ViewCount";
                    dpViewCount.Value = training.ViewCount;
                    cmd.Parameters.Add(dpViewCount);

                    DbParameter dpIsGold = provider.CreateParameter();
                    dpIsGold.ParameterName = parmPrefix + "IsGold";
                    dpIsGold.Value = training.IsGold;
                    cmd.Parameters.Add(dpIsGold);

                    cmd.ExecuteNonQuery();
                }

                // Tags
                UpdateTags(training, conn, provider);

                // Categories
                UpdateCategories(training, conn, provider);

                //Fields
                UpdateFields(training, conn, provider);

                // Comments
                UpdateComments(training, conn, provider);
            }
        }
       
        /// <summary>
        /// Deletes a post in the database
        /// </summary>
        /// <param name="post">post to delete</param>
        public override void DeleteTraining(Training training)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "DELETE FROM " + tablePrefix + "Tags WHERE ParentID = @id and ParentType=1;" +
                                      "DELETE FROM " + tablePrefix + "TrainingCategorie WHERE TrainingID = @id;" +
                                      "DELETE FROM " + tablePrefix + "TrainingField WHERE TrainingID = @id;" +
                                      "DELETE FROM " + tablePrefix + "Comments WHERE ParentID = @id and ParentType=1" +
                                      "DELETE FROM " + tablePrefix + "Trainings WHERE TrainingID = @id;";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "id";
                    dpID.Value = training.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets all post from the database
        /// </summary>
        /// <returns>List of posts</returns>
        public override List<Training> FillTrainings()
        {
            List<Training> trainings = new List<Training>();
            List<string> trainingIDs = new List<string>();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT TrainingID FROM " + tablePrefix + "Trainings ";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            trainingIDs.Add(rdr.GetGuid(0).ToString());
                        }
                    }
                }
            }

            foreach (string id in trainingIDs)
            {
                trainings.Add(Training.Load(new Guid(id)));
            }

            trainings.Sort();
            return trainings;
        }
        #endregion

        #region 公开课
        /// <summary>
        /// Returns a Curricula for given ID
        /// </summary>
        /// <param name="id">ID of page to return</param>
        /// <returns>selected page</returns>
        public override Curricula SelectCurricula(Guid id)
        {
            Curricula curricula = new Curricula();

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT CurriculaID,Title,ObjectDes,CurriculaContent, DateCreated, " +
                                        "DateModified,Author,IsPublished,Points,Scores,IsGold,ViewCount " +
                                        "FROM " + tablePrefix + "Curriculas " +
                                        "WHERE CurriculaID = " + parmPrefix + "id";

                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "id";
                    dpID.Value = id.ToString();
                    cmd.Parameters.Add(dpID);

                    conn.Open();
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();

                            curricula.Id = rdr.GetGuid(0);
                            curricula.Title = rdr.IsDBNull(1) ? String.Empty : rdr.GetString(1);
                            curricula.ObjectDes = rdr.IsDBNull(2) ? String.Empty : rdr.GetString(2);
						    curricula.Content = rdr.IsDBNull(3) ? String.Empty : rdr.GetString(3);
                            if (!rdr.IsDBNull(4))
                                curricula.DateCreated = rdr.GetDateTime(4);
                            if (!rdr.IsDBNull(5))
                                curricula.DateModified = rdr.GetDateTime(5);
                            if (!rdr.IsDBNull(6))
                                curricula.Author = rdr.GetString(6);
                            if (!rdr.IsDBNull(7))
                                curricula.IsPublished = rdr.GetBoolean(7);
                            if (!rdr.IsDBNull(8))
                                curricula.Points = rdr.GetInt32(8);
                            if (!rdr.IsDBNull(9))
                                curricula.Scores = rdr.GetInt32(9);
                            if (!rdr.IsDBNull(10))
                                curricula.IsGold = rdr.GetBoolean(10);
                            if (!rdr.IsDBNull(11))
                                curricula.ViewCount = rdr.GetInt32(11);
                        }
                    }

                    //Tags
                    sqlQuery = "SELECT Tag " +
                                "FROM " + tablePrefix + "Tags " +
                                "WHERE ParentID = " + parmPrefix + "id and ParentType=0";
                    cmd.CommandText = sqlQuery;
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                                curricula.Tags.Add(rdr.GetString(0));
                        }
                    }
                    curricula.Tags.MarkOld();
                    // CurriculaInfo
                    sqlQuery = "SELECT InfoID " +
                                "FROM " + tablePrefix + "CurriculaInfo " +
                                "WHERE CurriculaID = " + parmPrefix + "id";
                    cmd.CommandText = sqlQuery;
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Guid key = rdr.GetGuid(0);
                            curricula.CurriculaInfos.Add(CurriculaInfo.GetCurriculaInfo(key));
                        }
                    }
                    

                    // Categories
                    sqlQuery = "SELECT CategorieID " +
                                "FROM " + tablePrefix + "CurriculaCategorie " +
                                "WHERE CurriculaID = " + parmPrefix + "id";
                    cmd.CommandText = sqlQuery;
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Guid key = rdr.GetGuid(0);
                            if (Category.GetCategory(key) != null)
                                curricula.Categories.Add(Category.GetCategory(key));
                        }
                    }

                    // Fields
                    sqlQuery = "SELECT FieldID " +
                                "FROM " + tablePrefix + "CurriculaField " +
                                "WHERE CurriculaID = " + parmPrefix + "id";
                    cmd.CommandText = sqlQuery;
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Guid key = rdr.GetGuid(0);
                            if (Field.GetField(key) != null)
                                curricula.Fields.Add(Field.GetField(key));
                        }
                    }

                    // Comments
                    sqlQuery = "SELECT CommentID,CommentDate,Author,Count,Sex,Phone,Mobile,Email, " +
                                "Company,QQ_msn,Comment,IsDispose,DisposeBy,Country,IP " +
                                "FROM " + tablePrefix + "Comments " +
                                "WHERE ParentID = " + parmPrefix + "id and ParentType = 0";
                    cmd.CommandText = sqlQuery;
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Comment comment = new Comment();
                            comment.Id = rdr.GetGuid(0);
                            comment.IsApproved = false;
                            comment.DateCreated = rdr.GetDateTime(1);
                            comment.Author = rdr.GetString(2);
                            comment.Count = rdr.GetInt32(3);
                            comment.Sex = rdr.GetBoolean(4);
                            if (!rdr.IsDBNull(5))
                                comment.Phone = rdr.GetString(5);
                            comment.Mobile = rdr.GetString(6);
                            if (!rdr.IsDBNull(7))
                                comment.Email = rdr.GetString(7);
                            comment.Company = rdr.GetString(8);
                            if (!rdr.IsDBNull(9))
                                comment.QQ_msn = rdr.GetString(9);
                            comment.Content = rdr.GetString(10);
                            if (!rdr.IsDBNull(11))
                                comment.IsApproved = rdr.GetBoolean(11);
                            if (!rdr.IsDBNull(10))
                                comment.ModeratedBy = rdr.GetString(12);
                            if (!rdr.IsDBNull(6))
                                comment.Country = rdr.GetString(6);
                            if (!rdr.IsDBNull(7))
                                comment.IP = rdr.GetString(7);

                            comment.ParentId = id;
                            comment.ParentType = 0;
                            comment.Parent = curricula;

                            curricula.Comments.Add(comment);
                        }
                    }
                    curricula.Comments.Sort();
                }
            }

            return curricula;
        }

        /// <summary>
        /// Adds a Curricula to the database
        /// </summary>
        /// <param name="curricula">Curricula to be added</param>
        public override void InsertCurricula(Curricula curricula)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO " + tablePrefix + "Curriculas (CurriculaID,Title,ObjectDes,CurriculaContent, DateCreated, " +
                                     "DateModified,Author,IsPublished,Points,Scores,IsGold,ViewCount) " +
                                     "VALUES (@cid, @title, @objectdes, @content, @created, @modified, @author, @ispublished, @points, @scores,@IsGold,@ViewCount)";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "cid";
                    dpID.Value = curricula.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpTitle = provider.CreateParameter();
                    dpTitle.ParameterName = parmPrefix + "title";
                    dpTitle.Value = curricula.Title;
                    cmd.Parameters.Add(dpTitle);

                    DbParameter dpObjectDes = provider.CreateParameter();
                    dpObjectDes.ParameterName = parmPrefix + "objectdes";
                    dpObjectDes.Value = curricula.ObjectDes;
                    cmd.Parameters.Add(dpObjectDes);

                    DbParameter dpContent = provider.CreateParameter();
                    dpContent.ParameterName = parmPrefix + "content";
                    dpContent.Value = curricula.Content;
                    cmd.Parameters.Add(dpContent);

                    DbParameter dpCreated = provider.CreateParameter();
                    dpCreated.ParameterName = parmPrefix + "created";
                    dpCreated.Value = curricula.DateCreated.AddHours(-TrainSettings.Instance.Timezone);
                    cmd.Parameters.Add(dpCreated);

                    DbParameter dpModified = provider.CreateParameter();
                    dpModified.ParameterName = parmPrefix + "modified";
                    if (curricula.DateModified == new DateTime())
                        dpModified.Value = DateTime.Now;
                    else
                        dpModified.Value = curricula.DateModified.AddHours(-TrainSettings.Instance.Timezone);
                    cmd.Parameters.Add(dpModified);

                    DbParameter dpAuthor = provider.CreateParameter();
                    dpAuthor.ParameterName = parmPrefix + "author";
                    dpAuthor.Value = curricula.Author;
                    cmd.Parameters.Add(dpAuthor);

                    DbParameter dpPublished = provider.CreateParameter();
                    dpPublished.ParameterName = parmPrefix + "ispublished";
                    dpPublished.Value = curricula.IsPublished;
                    cmd.Parameters.Add(dpPublished);

                    DbParameter dpPoints = provider.CreateParameter();
                    dpPoints.ParameterName = parmPrefix + "points";
                    dpPoints.Value = curricula.Points;
                    cmd.Parameters.Add(dpPoints);

                    DbParameter dpScores = provider.CreateParameter();
                    dpScores.ParameterName = parmPrefix + "scores";
                    dpScores.Value = curricula.Scores;
                    cmd.Parameters.Add(dpScores);

                    DbParameter dpIsGold = provider.CreateParameter();
                    dpIsGold.ParameterName = parmPrefix + "IsGold";
                    dpIsGold.Value = curricula.IsGold;
                    cmd.Parameters.Add(dpIsGold);

                    DbParameter dpViewCount = provider.CreateParameter();
                    dpViewCount.ParameterName = parmPrefix + "ViewCount";
                    dpViewCount.Value = curricula.ViewCount;
                    cmd.Parameters.Add(dpViewCount);
                   cmd.ExecuteNonQuery();
                }

                // Tags
                UpdateTags(curricula, conn, provider);

                // Categories
                UpdateCategories(curricula, conn, provider);

                //Fields
                UpdateFields(curricula, conn, provider);

                // Comments
                UpdateComments(curricula, conn, provider);
            }
        }

        /// <summary>
        /// Saves an existing curricula in the database
        /// </summary>
        /// <param name="curricula">curricula to be saved</param>
        public override void UpdateCurricula(Curricula curricula)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "UPDATE " + tablePrefix + "Curriculas " +
                                        "SET Title = @title, ObjectDes = @desc, CurriculaContent = @content, " +
                                        "DateCreated = @created, DateModified = @modified, Author = @author, " +
                                        "IsPublished = @ispublished, Points = @points, Scores = @scores, " +
                                        "IsGold = @IsGold, ViewCount=@ViewCount "+
                                        "WHERE CurriculaID = @id";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "id";
                    dpID.Value = curricula.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpTitle = provider.CreateParameter();
                    dpTitle.ParameterName = parmPrefix + "title";
                    dpTitle.Value = curricula.Title;
                    cmd.Parameters.Add(dpTitle);

                    DbParameter dpDesc = provider.CreateParameter();
                    dpDesc.ParameterName = parmPrefix + "desc";
                    dpDesc.Value = curricula.ObjectDes;
                    cmd.Parameters.Add(dpDesc);

                    DbParameter dpContent = provider.CreateParameter();
                    dpContent.ParameterName = parmPrefix + "content";
                    dpContent.Value = curricula.Content;
                    cmd.Parameters.Add(dpContent);

                    DbParameter dpCreated = provider.CreateParameter();
                    dpCreated.ParameterName = parmPrefix + "created";
                    dpCreated.Value = curricula.DateCreated.AddHours(-TrainSettings.Instance.Timezone);
                    cmd.Parameters.Add(dpCreated);

                    DbParameter dpModified = provider.CreateParameter();
                    dpModified.ParameterName = parmPrefix + "modified";
                    if (curricula.DateModified == new DateTime())
                        dpModified.Value = DateTime.Now;
                    else
                        dpModified.Value = curricula.DateModified.AddHours(-TrainSettings.Instance.Timezone);
                    cmd.Parameters.Add(dpModified);

                    DbParameter dpAuthor = provider.CreateParameter();
                    dpAuthor.ParameterName = parmPrefix + "author";
                    dpAuthor.Value = curricula.Author;
                    cmd.Parameters.Add(dpAuthor);

                    DbParameter dpPublished = provider.CreateParameter();
                    dpPublished.ParameterName = parmPrefix + "ispublished";
                    dpPublished.Value = curricula.IsPublished;
                    cmd.Parameters.Add(dpPublished);

                    DbParameter dpPoints = provider.CreateParameter();
                    dpPoints.ParameterName = parmPrefix + "points";
                    dpPoints.Value = curricula.Points;
                    cmd.Parameters.Add(dpPoints);

                    DbParameter dpScores = provider.CreateParameter();
                    dpScores.ParameterName = parmPrefix + "scores";
                    dpScores.Value = curricula.Scores;
                    cmd.Parameters.Add(dpScores);

                    DbParameter dpIsGold = provider.CreateParameter();
                    dpIsGold.ParameterName = parmPrefix + "IsGold";
                    dpIsGold.Value = curricula.IsGold;
                    cmd.Parameters.Add(dpIsGold);

                    DbParameter dpViewCount = provider.CreateParameter();
                    dpViewCount.ParameterName = parmPrefix + "ViewCount";
                    dpViewCount.Value = curricula.ViewCount;
                    cmd.Parameters.Add(dpViewCount);
                    cmd.ExecuteNonQuery();
                }

                // Tags
                UpdateTags(curricula, conn, provider);

                // Categories
                UpdateCategories(curricula, conn, provider);

                //Fields
                UpdateFields(curricula, conn, provider);
                // CurriculaInfo
                UpdateCurriculaInfos(curricula, conn, provider);
                // Comments
                UpdateComments(curricula, conn, provider);
            }
        }

        /// <summary>
        /// Deletes a curricula from the database
        /// </summary>
        /// <param name="curricula">curricula to be deleted</param>
        public override void DeleteCurricula(Curricula curricula)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "DELETE FROM " + tablePrefix + "Tags WHERE ParentID = @id and ParentType=0;" +
                                      "DELETE FROM " + tablePrefix + "CurriculaCategorie WHERE CurriculaID = @id;" +
                                      "DELETE FROM " + tablePrefix + "CurriculaField WHERE CurriculaID = @id;" +
                                      "DELETE FROM " + tablePrefix + "Comments WHERE ParentID = @id and ParentType=0;" +
                                      "DELETE FROM " + tablePrefix + "CurriculaInfo WHERE CurriculaID = @id;" +
                                      "DELETE FROM " + tablePrefix + "Curriculas WHERE CurriculaID = @id;";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "id";
                    dpID.Value = curricula.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets all pages in database
        /// </summary>
        /// <returns>List of pages</returns>
        public override List<Curricula> FillCurriculas()
        {
            List<Curricula> curriculas = new List<Curricula>();
            List<string> curriculaIDs = new List<string>();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT CurriculaID FROM " + tablePrefix + "Curriculas ";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            curriculaIDs.Add(rdr.GetGuid(0).ToString());
                        }
                    }
                }
            }

            foreach (string id in curriculaIDs)
            {
                curriculas.Add(Curricula.Load(new Guid(id)));
            }

            return curriculas;
        }
        #endregion

        #region 分类
        /// <summary>
        /// Returns a category 
        /// </summary>
        /// <param name="id">Id of category to return</param>
        /// <returns></returns>
        public override Category SelectCategory(Guid id)
        {
            List<Category> categories = Category.Categories;

            Category category = new Category();

            foreach (Category cat in categories)
            {
                if (cat.Id == id)
                    category = cat;
            }
            category.MarkOld();
            return category;
        }

        /// <summary>
        /// Adds a new category to the database
        /// </summary>
        /// <param name="category">category to add</param>
        public override void InsertCategory(Category category)
        {
            List<Category> categories = Category.Categories;
            categories.Add(category);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO " + tablePrefix + "Categories (CategoryID, CategoryName, description, ParentID) " +
                                        "VALUES (@catid, @catname, @description, @parentid)";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "catid";
                    dpID.Value = category.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpTitle = provider.CreateParameter();
                    dpTitle.ParameterName = parmPrefix + "catname";
                    dpTitle.Value = category.Title;
                    cmd.Parameters.Add(dpTitle);

                    DbParameter dpDesc = provider.CreateParameter();
                    dpDesc.ParameterName = parmPrefix + "description";
                    dpDesc.Value = category.Description;
                    cmd.Parameters.Add(dpDesc);

                    DbParameter dpParent = provider.CreateParameter();
                    dpParent.ParameterName = parmPrefix + "parentid";
                    if (category.Parent == null)
                        dpParent.Value = DBNull.Value;
                    else 
                        dpParent.Value = category.Parent.ToString();
                    cmd.Parameters.Add(dpParent);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Saves an existing category to the database
        /// </summary>
        /// <param name="field">category to be saved</param>
        public override void UpdateCategory(Category category)
        {
            List<Category> categories = Category.Categories;
            categories.Remove(category);
            categories.Add(category);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "UPDATE " + tablePrefix + "Categories " +
                                  "SET CategoryName = @catname, " +
                                  "Description = @description, ParentID = @parentid " +
                                  "WHERE CategoryID = @catid";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "catid";
                    dpID.Value = category.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpTitle = provider.CreateParameter();
                    dpTitle.ParameterName = parmPrefix + "catname";
                    dpTitle.Value = category.Title;
                    cmd.Parameters.Add(dpTitle);

                    DbParameter dpDesc = provider.CreateParameter();
                    dpDesc.ParameterName = parmPrefix + "description";
                    dpDesc.Value = category.Description;
                    cmd.Parameters.Add(dpDesc);

                    DbParameter dpParent = provider.CreateParameter();
                    dpParent.ParameterName = parmPrefix + "parentid";
                    if (category.Parent == null)
                        dpParent.Value = DBNull.Value;
                    else
                        dpParent.Value = category.Parent.ToString();
                    cmd.Parameters.Add(dpParent); 
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes a category from the database
        /// </summary>
        /// <param name="category">category to be removed</param>
        public override void DeleteCategory(Category category)
        {
            List<Category> categories = Category.Categories;
            categories.Remove(category);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery =
                        "DELETE FROM " + tablePrefix + "CurriculaCategorie  WHERE CategorieID = " + parmPrefix + "catid;" +
                        "DELETE FROM " + tablePrefix + "TrainingCategorie  WHERE CategorieID = " + parmPrefix + "catid;" +
                        "DELETE FROM " + tablePrefix + "Categories  WHERE CategoryID = " + parmPrefix + "catid";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "catid";
                    dpID.Value = category.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets all categories in database
        /// </summary>
        /// <returns>List of categories</returns>
        public override List<Category> FillCategories()
        {
            List<Category> categories = new List<Category>();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT CategoryID, CategoryName, description, ParentID " +
                        "FROM " + tablePrefix + "Categories ";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Category cat = new Category();
                                cat.Title = rdr.GetString(1);
                                if (rdr.IsDBNull(2))
                                    cat.Description = "";
                                else
                                    cat.Description = rdr.GetString(2);
                                if (rdr.IsDBNull(3))
                                    cat.Parent = null;
                                else
                                    cat.Parent = new Guid(rdr.GetGuid(3).ToString());
                                cat.Id = new Guid(rdr.GetGuid(0).ToString());
                                categories.Add(cat);
                                cat.MarkOld();
                            }
                        }
                    }
                }
            }

            return categories;
        }
        #endregion

        #region 领域
        /// <summary>
        /// Returns a field 
        /// </summary>
        /// <param name="id">Id of field to return</param>
        /// <returns></returns>
        public override Field SelectField(Guid id)
        {
            List<Field> fields = Field.Fields;

            Field field = new Field();

            foreach (Field cat in fields)
            {
                if (cat.Id == id)
                    field = cat;
            }
            field.MarkOld();
            return field;
        }

        /// <summary>
        /// Adds a new field to the database
        /// </summary>
        /// <param name="field">field to add</param>
        public override void InsertField(Field field)
        {
            List<Field> fields = Field.Fields;
            fields.Add(field);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO " + tablePrefix + "Fields (FieldID,FieldName,Description) " +
                                        "VALUES (@catid, @catname, @description)";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "catid";
                    dpID.Value = field.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpTitle = provider.CreateParameter();
                    dpTitle.ParameterName = parmPrefix + "catname";
                    dpTitle.Value = field.FieldName;
                    cmd.Parameters.Add(dpTitle);

                    DbParameter dpDesc = provider.CreateParameter();
                    dpDesc.ParameterName = parmPrefix + "description";
                    dpDesc.Value = field.Description;
                    cmd.Parameters.Add(dpDesc);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Saves an existing field to the database
        /// </summary>
        /// <param name="category">field to be saved</param>
        public override void UpdateField(Field field)
        {
            List<Field> fields = Field.Fields;
            fields.Remove(field);
            fields.Add(field);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "UPDATE " + tablePrefix + "Fields " +
                                  "SET FieldName = @catname, " +
                                  "Description = @description " +
                                  "WHERE FieldID = @catid";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "catid";
                    dpID.Value = field.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpTitle = provider.CreateParameter();
                    dpTitle.ParameterName = parmPrefix + "catname";
                    dpTitle.Value = field.FieldName;
                    cmd.Parameters.Add(dpTitle);

                    DbParameter dpDesc = provider.CreateParameter();
                    dpDesc.ParameterName = parmPrefix + "description";
                    dpDesc.Value = field.Description;
                    cmd.Parameters.Add(dpDesc);

                   cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes a field from the database
        /// </summary>
        /// <param name="field">field to be removed</param>
        public override void DeleteField(Field field)
        {
            List<Field> fields = Field.Fields;
            fields.Remove(field);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = 
                        "DELETE FROM " + tablePrefix + "CurriculaField  WHERE FieldID = " + parmPrefix + "catid;" +
                        "DELETE FROM " + tablePrefix + "TrainingField  WHERE FieldID = " + parmPrefix + "catid;"+
                        "DELETE FROM " + tablePrefix + "Fields  WHERE FieldID = " + parmPrefix + "catid";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "catid";
                    dpID.Value = field.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets all categories in database
        /// </summary>
        /// <returns>List of categories</returns>
        public override List<Field> FillFields()
        {
            List<Field> fields = new List<Field>();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT FieldID,FieldName,Description " +
                        "FROM " + tablePrefix + "Fields ";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Field fld = new Field();
                                fld.FieldName = rdr.GetString(1);
                                if (rdr.IsDBNull(2))
                                    fld.Description = "";
                                else
                                    fld.Description = rdr.GetString(2);
                                
                                fld.Id = new Guid(rdr.GetGuid(0).ToString());
                                fields.Add(fld);
                                fld.MarkOld();
                            }
                        }
                    }
                }
            }

            return fields;
        }
        #endregion

        #region 课程安排
        /// <summary>
        /// Returns a field 
        /// </summary>
        /// <param name="id">Id of field to return</param>
        /// <returns></returns>
        public override CurriculaInfo SelectCurriculaInfo(Guid id)
        {
            List<CurriculaInfo> infos = CurriculaInfo.CurriculaInfos;

            CurriculaInfo info = new CurriculaInfo();

            foreach (CurriculaInfo cat in infos)
            {
                if (cat.Id == id)
                    info = cat;
            }
            info.MarkOld();
            return info;
        }

        /// <summary>
        /// Adds a new field to the database
        /// </summary>
        /// <param name="info">field to add</param>
        public override void InsertCurriculaInfo(CurriculaInfo info)
        {
            List<CurriculaInfo> infos = CurriculaInfo.CurriculaInfos;
            infos.Add(info);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO " + tablePrefix + "CurriculaInfo (InfoID, CurriculaID,StartDate,EndDate,Cast,CityTown,IsPublished) " +
                                        "VALUES (@infoid,@catid, @startDate, @enddate, @cast, @citytown,@ispublished)";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "infoid";
                    dpID.Value = info.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpcID = provider.CreateParameter();
                    dpcID.ParameterName = parmPrefix + "catid";
                    dpcID.Value = info.CurriculaId.ToString();
                    cmd.Parameters.Add(dpcID);

                    DbParameter dpStartDate = provider.CreateParameter();
                    dpStartDate.ParameterName = parmPrefix + "startDate";
                    dpStartDate.Value = info.StartDate;
                    cmd.Parameters.Add(dpStartDate);

                    DbParameter dpEndDate = provider.CreateParameter();
                    dpEndDate.ParameterName = parmPrefix + "enddate";
                    dpEndDate.Value = info.EndDate;
                    cmd.Parameters.Add(dpEndDate);

                    DbParameter dpCast = provider.CreateParameter();
                    dpCast.ParameterName = parmPrefix + "cast";
                    dpCast.Value = info.Cast;
                    cmd.Parameters.Add(dpCast);

                    DbParameter dpCityTown = provider.CreateParameter();
                    dpCityTown.ParameterName = parmPrefix + "citytown";
                    dpCityTown.Value = info.CityTown;
                    cmd.Parameters.Add(dpCityTown);

                    DbParameter dpIsPublished = provider.CreateParameter();
                    dpIsPublished.ParameterName = parmPrefix + "ispublished";
                    dpIsPublished.Value = info.IsPublished;
                    cmd.Parameters.Add(dpIsPublished);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Saves an existing field to the database
        /// </summary>
        /// <param name="category">field to be saved</param>
        public override void UpdateCurriculaInfo(CurriculaInfo info)
        {
            List<CurriculaInfo> infos = CurriculaInfo.CurriculaInfos;
            infos.Remove(info);
            infos.Add(info);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "UPDATE " + tablePrefix + "CurriculaInfo " +
                                  "SET CurriculaID=@catid,StartDate=@startDate,EndDate=@enddate,Cast=@cast,CityTown=@citytown,IsPublished=@ispublished " +
                                  "WHERE InfoID = @infoID ";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "infoid";
                    dpID.Value = info.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpcID = provider.CreateParameter();
                    dpcID.ParameterName = parmPrefix + "catid";
                    dpcID.Value = info.CurriculaId.ToString();
                    cmd.Parameters.Add(dpcID);

                    DbParameter dpStartDate = provider.CreateParameter();
                    dpStartDate.ParameterName = parmPrefix + "startDate";
                    dpStartDate.Value = info.StartDate;
                    cmd.Parameters.Add(dpStartDate);

                    DbParameter dpEndDate = provider.CreateParameter();
                    dpEndDate.ParameterName = parmPrefix + "enddate";
                    dpEndDate.Value = info.EndDate;
                    cmd.Parameters.Add(dpEndDate);

                    DbParameter dpCast = provider.CreateParameter();
                    dpCast.ParameterName = parmPrefix + "cast";
                    dpCast.Value = info.Cast;
                    cmd.Parameters.Add(dpCast);

                    DbParameter dpCityTown = provider.CreateParameter();
                    dpCityTown.ParameterName = parmPrefix + "citytown";
                    dpCityTown.Value = info.CityTown;
                    cmd.Parameters.Add(dpCityTown);

                    DbParameter dpIsPublished = provider.CreateParameter();
                    dpIsPublished.ParameterName = parmPrefix + "ispublished";
                    dpIsPublished.Value = info.IsPublished;
                    cmd.Parameters.Add(dpIsPublished);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes a field from the database
        /// </summary>
        /// <param name="info">field to be removed</param>
        public override void DeleteCurriculaInfo(CurriculaInfo info)
        {
            List<CurriculaInfo> infos = CurriculaInfo.CurriculaInfos;
            infos.Remove(info);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery =
                        "DELETE FROM " + tablePrefix + "CurriculaInfo  WHERE InfoID = " + parmPrefix + "catid";
                       
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "catid";
                    dpID.Value = info.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets all categories in database
        /// </summary>
        /// <returns>List of categories</returns>
        public override List<CurriculaInfo> FillCurriculaInfos()
        {
            List<CurriculaInfo> infos = new List<CurriculaInfo>();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT InfoID, CurriculaID,StartDate,EndDate,Cast,CityTown,IsPublished " +
                        "FROM " + tablePrefix + "CurriculaInfo ";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                CurriculaInfo inf = new CurriculaInfo();
                                inf.CurriculaId = rdr.GetGuid(1);
                                inf.StartDate = rdr.GetDateTime(2);
                                inf.EndDate = rdr.GetDateTime(3);
                                inf.Cast = rdr.GetInt32(4);
                                inf.CityTown = rdr.GetString(5);
                                if (rdr.IsDBNull(6))
                                {
                                    inf.IsPublished = rdr.GetBoolean(6);
                                }
                                else
                                {
                                    inf.IsPublished = false;
                                }
                                
                                inf.Id = new Guid(rdr.GetGuid(0).ToString());
                                infos.Add(inf);
                                inf.MarkOld();
                            }
                        }
                    }
                }
            }

            return infos;
        }
        #endregion

        #region 资料
        /// <summary>
        /// Returns a field 
        /// </summary>
        /// <param name="id">Id of field to return</param>
        /// <returns></returns>
        public override Res SelectRes(Guid id)
        {
            List<Res> Ress = Res.Ress;

            Res res = new Res();

            foreach (Res cat in Ress)
            {
                if (cat.Id == id)
                    res = cat;
            }
            res.MarkOld();
            return res;
        }

        /// <summary>
        /// Adds a new field to the database
        /// </summary>
        /// <param name="res">field to add</param>
        public override void InsertRes(Res res)
        {
            List<Res> ress = Res.Ress;
            ress.Add(res);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO " + tablePrefix + "Res (ResID,FileName,ResType,Description,Author,IsPublished,DateCreated,Points) " +
                                        "VALUES (@ResID,@FileName, @ResType, @Description,@Author,@IsPublished,@DateCreated,@Points)";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "ResID";
                    dpID.Value = res.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpcFileName = provider.CreateParameter();
                    dpcFileName.ParameterName = parmPrefix + "FileName";
                    dpcFileName.Value = res.FileName;
                    cmd.Parameters.Add(dpcFileName);

                    DbParameter dpResType = provider.CreateParameter();
                    dpResType.ParameterName = parmPrefix + "ResType";
                    dpResType.Value = res.ResType;
                    cmd.Parameters.Add(dpResType);

                    DbParameter dpDescription = provider.CreateParameter();
                    dpDescription.ParameterName = parmPrefix + "Description";
                    dpDescription.Value = res.Description;
                    cmd.Parameters.Add(dpDescription);

                    DbParameter dpAuthor = provider.CreateParameter();
                    dpAuthor.ParameterName = parmPrefix + "Author";
                    dpAuthor.Value = res.Author;
                    cmd.Parameters.Add(dpAuthor);

                    DbParameter dpPublished = provider.CreateParameter();
                    dpPublished.ParameterName = parmPrefix + "IsPublished";
                    dpPublished.Value = res.IsPublished;
                    cmd.Parameters.Add(dpPublished);

                    DbParameter dpDateCreated = provider.CreateParameter();
                    dpDateCreated.ParameterName = parmPrefix + "DateCreated";
                    dpDateCreated.Value = res.DateCreated;
                    cmd.Parameters.Add(dpDateCreated);

                    DbParameter dpPoints = provider.CreateParameter();
                    dpPoints.ParameterName = parmPrefix + "Points";
                    dpPoints.Value = res.Points;
                    cmd.Parameters.Add(dpPoints);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Saves an existing field to the database
        /// </summary>
        /// <param name="category">field to be saved</param>
        public override void UpdateRes(Res res)
        {
            List<Res> Ress = Res.Ress;
            Ress.Remove(res);
            Ress.Add(res);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "UPDATE " + tablePrefix + "Res " +
                                  "SET FileName=@FileName,ResType=@ResType,Description=@Description,Author=@Author,IsPublished=@IsPublished,DateCreated=@DateCreated,Points=@Points " +
                                  "WHERE ResID=@ResID";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "ResID";
                    dpID.Value = res.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpFileName = provider.CreateParameter();
                    dpFileName.ParameterName = parmPrefix + "FileName";
                    dpFileName.Value = res.FileName.ToString();
                    cmd.Parameters.Add(dpFileName);

                    DbParameter dpResType = provider.CreateParameter();
                    dpResType.ParameterName = parmPrefix + "ResType";
                    dpResType.Value = res.ResType;
                    cmd.Parameters.Add(dpResType);

                    DbParameter dpDescription = provider.CreateParameter();
                    dpDescription.ParameterName = parmPrefix + "Description";
                    dpDescription.Value = res.Description;
                    cmd.Parameters.Add(dpDescription);

                    DbParameter dpAuthor = provider.CreateParameter();
                    dpAuthor.ParameterName = parmPrefix + "Author";
                    dpAuthor.Value = res.Author;
                    cmd.Parameters.Add(dpAuthor);

                    DbParameter dpPublished = provider.CreateParameter();
                    dpPublished.ParameterName = parmPrefix + "IsPublished";
                    dpPublished.Value = res.IsPublished;
                    cmd.Parameters.Add(dpPublished);

                    DbParameter dpDateCreated = provider.CreateParameter();
                    dpDateCreated.ParameterName = parmPrefix + "DateCreated";
                    dpDateCreated.Value = res.DateCreated;
                    cmd.Parameters.Add(dpDateCreated);

                    DbParameter dpPoints = provider.CreateParameter();
                    dpPoints.ParameterName = parmPrefix + "Points";
                    dpPoints.Value = res.Points;
                    cmd.Parameters.Add(dpPoints);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes a field from the database
        /// </summary>
        /// <param name="res">field to be removed</param>
        public override void DeleteRes(Res res)
        {
            List<Res> infos = Res.Ress;
            infos.Remove(res);

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery =
                        "DELETE FROM " + tablePrefix + "Res  WHERE ResID = " + parmPrefix + "ResID";

                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "ResID";
                    dpID.Value = res.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Gets all categories in database
        /// </summary>
        /// <returns>List of categories</returns>
        public override List<Res> FillRess()
        {
            List<Res> infos = new List<Res>();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT ResID,FileName,ResType,Description,Author,IsPublished,DateCreated,Points " +
                        "FROM " + tablePrefix + "Res ";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Res inf = new Res();
                                inf.Id = new Guid(rdr.GetGuid(0).ToString());
                                inf.FileName = rdr.GetString(1);
                                inf.ResType = rdr.GetString(2);
                                inf.Description = rdr.GetString(3);
                                inf.Author = rdr.GetString(4);
                                inf.IsPublished = rdr.GetBoolean(5);
                                inf.DateCreated = rdr.GetDateTime(6);
                                inf.Points = rdr.GetInt32(7);
                                infos.Add(inf);
                                inf.MarkOld();
                            }
                        }
                    }
                }
            }

            return infos;
        }

        public override int UpateBlob(Res res)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = tablePrefix + "UploadFilePrd";

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "ResID";
                    dpID.Value = res.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpResType = provider.CreateParameter();
                    dpResType.ParameterName = parmPrefix + "ResType";
                    dpResType.Value = res.ResType;
                    cmd.Parameters.Add(dpResType);

                    DbParameter dpBlob = provider.CreateParameter();
                    dpBlob.ParameterName = parmPrefix + "Blob";
                    dpBlob.Value = res.CurrentPostFileBuffer;
                    cmd.Parameters.Add(dpBlob);

                    return cmd.ExecuteNonQuery();
                }
            }

        }

        public override byte[] GetBlob(Res res)
        {
             string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT Blob " +
                           "FROM " + tablePrefix + "Res "+
                           "WHERE ResID = " + parmPrefix + "ResID";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "ResID";
                    dpID.Value = res.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {

                        if (rdr.Read())
                        {
                            return ((Byte[])rdr["Blob"]);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        #endregion

        #region 精彩
        /// <summary>
        /// Returns a field 
        /// </summary>
        /// <param name="id">Id of Excellent to return</param>
        /// <returns></returns>
        public override Excellent SelectExcellent(Guid id)
        {
            Excellent excellent = new Excellent();

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT ExcellentID,Title,CityTown,Teacher,TrainingDate, " +
                                        " MastPic,IsPublished,Author " +
                                        "FROM " + tablePrefix + "Excellent " +
                                        "WHERE ExcellentID = " + parmPrefix + "id";

                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "id";
                    dpID.Value = id.ToString();
                    cmd.Parameters.Add(dpID);

                    conn.Open();
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();

                            excellent.Id = rdr.GetGuid(0);
                            excellent.Title =  rdr.GetString(1);
                            excellent.CityTown =  rdr.GetString(2);
                            excellent.Teacher = rdr.GetString(3);
                            excellent.TrainingDate = rdr.GetDateTime(4);
                            excellent.MastPic = rdr.GetGuid(5);
                            excellent.IsPublished = rdr.GetBoolean(6);
                            excellent.Author = rdr.GetString(7);
                        }
                    }

                    //Ress
                    sqlQuery = "SELECT ResID " +
                                "FROM " + tablePrefix + "ExcellentRes " +
                                "WHERE ExcellentID = " + parmPrefix + "id";
                    cmd.CommandText = sqlQuery;
                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                                excellent.Ress.Add(Res.GetRes(rdr.GetGuid(0)));
                        }
                    }
                    excellent.Ress.MarkOld();
                    
                }
            }

            return excellent;
        }

        /// <summary>
        /// Adds a new Excellent to the database
        /// </summary>
        /// <param name="info">Excellent to add</param>
        public override void InsertExcellent(Excellent info)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO " + tablePrefix + "Excellent (ExcellentID,Title,CityTown,Teacher,TrainingDate,MastPic,IsPublished,Author) " +
                                        "VALUES (@ExcellentID,@Title, @CityTown, @Teacher, @TrainingDate, @MastPic,@IsPublished,@Author)";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "ExcellentID";
                    dpID.Value = info.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpcID = provider.CreateParameter();
                    dpcID.ParameterName = parmPrefix + "Title";
                    dpcID.Value = info.Title.ToString();
                    cmd.Parameters.Add(dpcID);

                    DbParameter dpCityTown = provider.CreateParameter();
                    dpCityTown.ParameterName = parmPrefix + "CityTown";
                    dpCityTown.Value = info.CityTown;
                    cmd.Parameters.Add(dpCityTown);

                    DbParameter dpTeacher = provider.CreateParameter();
                    dpTeacher.ParameterName = parmPrefix + "Teacher";
                    dpTeacher.Value = info.Teacher;
                    cmd.Parameters.Add(dpTeacher);

                    DbParameter dpTrainingDate = provider.CreateParameter();
                    dpTrainingDate.ParameterName = parmPrefix + "TrainingDate";
                    dpTrainingDate.Value = info.TrainingDate;
                    cmd.Parameters.Add(dpTrainingDate);

                    DbParameter dpMastPic = provider.CreateParameter();
                    dpMastPic.ParameterName = parmPrefix + "MastPic";
                    dpMastPic.Value = info.MastPic;
                    cmd.Parameters.Add(dpMastPic);

                    DbParameter dpIsPublished = provider.CreateParameter();
                    dpIsPublished.ParameterName = parmPrefix + "IsPublished";
                    dpIsPublished.Value = info.IsPublished;
                    cmd.Parameters.Add(dpIsPublished);

                    DbParameter dpAuthor = provider.CreateParameter();
                    dpAuthor.ParameterName = parmPrefix + "Author";
                    dpAuthor.Value = info.Author;
                    cmd.Parameters.Add(dpAuthor);
                    cmd.ExecuteNonQuery();
                }

                UpdateRessForExcellent(info, conn, provider);
            }
        }

        /// <summary>
        /// Saves an existing field to the database
        /// </summary>
        /// <param name="category">field to be saved</param>
        public override void UpdateExcellent(Excellent info)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "UPDATE " + tablePrefix + "Excellent " +
                                  "SET Title=@Title,CityTown=@CityTown,Teacher=@Teacher,TrainingDate=@TrainingDate,MastPic=@MastPic,IsPublished=@IsPublished,Author=@Author " +
                                  "WHERE ExcellentID = @ExcellentID ";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "ExcellentID";
                    dpID.Value = info.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    DbParameter dpTitle = provider.CreateParameter();
                    dpTitle.ParameterName = parmPrefix + "Title";
                    dpTitle.Value = info.Title.ToString();
                    cmd.Parameters.Add(dpTitle);

                    DbParameter dpCityTown = provider.CreateParameter();
                    dpCityTown.ParameterName = parmPrefix + "CityTown";
                    dpCityTown.Value = info.CityTown;
                    cmd.Parameters.Add(dpCityTown);

                    DbParameter dpTeacher = provider.CreateParameter();
                    dpTeacher.ParameterName = parmPrefix + "Teacher";
                    dpTeacher.Value = info.Teacher;
                    cmd.Parameters.Add(dpTeacher);

                    DbParameter dpTrainingDate = provider.CreateParameter();
                    dpTrainingDate.ParameterName = parmPrefix + "TrainingDate";
                    dpTrainingDate.Value = info.TrainingDate;
                    cmd.Parameters.Add(dpTrainingDate);

                    DbParameter dpMastPic = provider.CreateParameter();
                    dpMastPic.ParameterName = parmPrefix + "MastPic";
                    dpMastPic.Value = info.MastPic;
                    cmd.Parameters.Add(dpMastPic);

                    DbParameter dpIsPublished = provider.CreateParameter();
                    dpIsPublished.ParameterName = parmPrefix + "IsPublished";
                    dpIsPublished.Value = info.IsPublished;
                    cmd.Parameters.Add(dpIsPublished);

                    DbParameter dpAuthor = provider.CreateParameter();
                    dpAuthor.ParameterName = parmPrefix + "Author";
                    dpAuthor.Value = info.Author;
                    cmd.Parameters.Add(dpAuthor);
                    cmd.ExecuteNonQuery();
                }

                UpdateRessForExcellent(info, conn, provider);
            }
        }

        /// <summary>
        /// Deletes a field from the database
        /// </summary>
        /// <param name="info">field to be removed</param>
        public override void DeleteExcellent(Excellent info)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery =
                        "DELETE FROM " + tablePrefix + "Excellent  WHERE ExcellentID = " + parmPrefix + "ExcellentID;"+
                        "DELETE FROM " + tablePrefix + "ExcellentRes WHERE ExcellentID = " + parmPrefix + "ExcellentID";

                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "ExcellentID";
                    dpID.Value = info.Id.ToString();
                    cmd.Parameters.Add(dpID);

                    cmd.ExecuteNonQuery();
                }
            }
            foreach (Res rs in info.Ress)
            {
                rs.Delete();
                rs.Save();
            }
        }

        /// <summary>
        /// Gets all categories in database
        /// </summary>
        /// <returns>List of categories</returns>
        public override List<Excellent> FillExcellents()
        {
            List<Excellent> excellents = new List<Excellent>();
            List<string> excellentIDs = new List<string>();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT ExcellentID " +
                        "FROM " + tablePrefix + "Excellent ";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                excellentIDs.Add(rdr.GetGuid(0).ToString());
                            }
                        }
                    }
                }
                
            }
            foreach (string id in excellentIDs)
            {
                excellents.Add(Excellent.Load(new Guid(id)));
            }

            excellents.Sort();
            return excellents;
        }
        #endregion

        #region 用户动作历史
        /// <summary>
        /// Loads AuthorProfile from database
        /// </summary>
        /// <param name="id">username</param>
        /// <returns></returns>
        public override UserHis SelectUserHis(string id)
        {
            UserHis his = new UserHis(id);

            // Retrieve Profile data from Db
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT EventType, EventValue,EventDate,EventDes FROM " + tablePrefix + "UserHis " +
                                        "WHERE UserName = " + parmPrefix + "name";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpName = provider.CreateParameter();
                    dpName.ParameterName = parmPrefix + "name";
                    dpName.Value = id;
                    cmd.Parameters.Add(dpName);

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            his.EventType = rdr.GetString(0);
                            his.EventValue = rdr.GetString(1);
                            his.EventDate = rdr.GetDateTime(2);
                            if (!rdr.IsDBNull(3))
                            {
                                his.EventDes = rdr.GetString(3);
                            }
                        }
                    }
                }
            }
           

            return his;
        }

        /// <summary>
        /// Adds AuthorProfile to database
        /// </summary>
        /// <param name="profile"></param>
        public override void InsertUserHis(UserHis his)
        {
            UpdateUserHis(his);
        }

        /// <summary>
        /// Updates AuthorProfile to database
        /// </summary>
        /// <param name="profile"></param>
        public override void UpdateUserHis(UserHis his)
        {

            //DeleteUserHis(his);
            // Save 
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {

                    string sqlQuery = "INSERT INTO " + tablePrefix + "UserHis (UserName,EventType,EventValue,EventDate,EventDes) " +
                                          "VALUES (@user, @EventType,@EventValue,@EventDate,@EventDes)";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.Parameters.Clear();

                    DbParameter dpUser = provider.CreateParameter();
                    dpUser.ParameterName = parmPrefix + "user";
                    dpUser.Value = his.Id;
                    cmd.Parameters.Add(dpUser);

                    DbParameter dpEventType = provider.CreateParameter();
                    dpEventType.ParameterName = parmPrefix + "EventType";
                    dpEventType.Value = his.EventType;
                    cmd.Parameters.Add(dpEventType);

                    DbParameter dpValue = provider.CreateParameter();
                    dpValue.ParameterName = parmPrefix + "EventValue";
                    dpValue.Value = his.EventValue;
                    cmd.Parameters.Add(dpValue);

                    DbParameter dpEventDate = provider.CreateParameter();
                    dpEventDate.ParameterName = parmPrefix + "EventDate";
                    dpEventDate.Value = his.EventDate;
                    cmd.Parameters.Add(dpEventDate);

                    DbParameter dpEventDes = provider.CreateParameter();
                    dpEventDes.ParameterName = parmPrefix + "EventDes";
                    dpEventDes.Value = his.EventDes;
                    cmd.Parameters.Add(dpEventDes);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        /// <summary>
        /// Remove AuthorProfile from database
        /// </summary>
        /// <param name="profile"></param>
        public override void DeleteUserHis(UserHis his)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "DELETE FROM " + tablePrefix + "UserHis " +
                                      "WHERE UserName = " + parmPrefix + "name";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpName = provider.CreateParameter();
                    dpName.ParameterName = parmPrefix + "name";
                    dpName.Value = his.Id;
                    cmd.Parameters.Add(dpName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Return collection for AuthorProfiles from database
        /// </summary>
        /// <returns></returns>
        public override List<UserHis> FillUserHiss()
        {
            List<UserHis> hiss = new List<UserHis>();
            List<string> hisNames = new List<string>();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT UserName FROM " + tablePrefix + "UserHis " +
                                      "GROUP BY UserName";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            hisNames.Add(rdr.GetString(0));
                        }
                    }
                }
            }

            foreach (string name in hisNames)
            {
                hiss.Add(BusinessBase<UserHis, string>.Load(name));
            }

            return hiss;
        }
        #endregion


        /// <summary>
        /// Gets the settings from the database
        /// </summary>
        /// <returns>dictionary of settings</returns>
        public override StringDictionary LoadSettings()
        {
            StringDictionary dic = new StringDictionary();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT SettingName, SettingValue FROM " + tablePrefix + "Settings";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string name = rdr.GetString(0);
                            string value = rdr.GetString(1);

                            dic.Add(name, value);
                        }
                    }
                }
            }

            return dic;
        }

        /// <summary>
        /// Saves the settings to the database
        /// </summary>
        /// <param name="settings">dictionary of settings</param>
        public override void SaveSettings(StringDictionary settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "DELETE FROM " + tablePrefix + "Settings";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                    foreach (string key in settings.Keys)
                    {
                        sqlQuery = "INSERT INTO " + tablePrefix + "Settings (SettingName, SettingValue) " +
                                   "VALUES (" + parmPrefix + "name, " + parmPrefix + "value)";
                        cmd.CommandText = sqlQuery;
                        cmd.Parameters.Clear();

                        DbParameter dpName = provider.CreateParameter();
                        dpName.ParameterName = parmPrefix + "name";
                        dpName.Value = key;
                        cmd.Parameters.Add(dpName);

                        DbParameter dpValue = provider.CreateParameter();
                        dpValue.ParameterName = parmPrefix + "value";
                        dpValue.Value = settings[key];
                        cmd.Parameters.Add(dpValue);

                        cmd.ExecuteNonQuery();
                    }

                }
            }
        }

        /// <summary>
        /// Get stopwords from the database
        /// </summary>
        /// <returns>collection of stopwords</returns>
        public override StringCollection LoadStopWords()
        {
            StringCollection col = new StringCollection();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT StopWord FROM " + tablePrefix + "StopWords";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (!col.Contains(rdr.GetString(0)))
                                col.Add(rdr.GetString(0));
                        }
                    }
                }
            }

            return col;
        }

        /// <summary>
        /// Load user data from DataStore
        /// </summary>
        /// <param name="exType">type of info</param>
        /// <param name="exId">id of info</param>
        /// <returns>stream of detail data</returns>
        public override object LoadFromDataStore(ExtensionType exType, string exId)
        {
            //MemoryStream stream;
            object o = null;
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT Settings FROM " + tablePrefix + "DataStoreSettings " +
                                        "WHERE ExtensionType = " + parmPrefix + "etype AND ExtensionId = " + parmPrefix + "eid";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    DbParameter dpeType = provider.CreateParameter();
                    dpeType.ParameterName = parmPrefix + "etype";
                    dpeType.Value = exType.GetHashCode();
                    cmd.Parameters.Add(dpeType);
                    DbParameter dpeId = provider.CreateParameter();
                    dpeId.ParameterName = parmPrefix + "eid";
                    dpeId.Value = exId;
                    cmd.Parameters.Add(dpeId);

                    o = cmd.ExecuteScalar();
                }
            }
            return o;
        }

        /// <summary>
        /// Save to DataStore
        /// </summary>
        /// <param name="exType">type of info</param>
        /// <param name="exId">id of info</param>
        /// <param name="settings">data of info</param>
        public override void SaveToDataStore(ExtensionType exType, string exId, object settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            // Save
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            XmlSerializer xs = new XmlSerializer(settings.GetType());
            string objectXML = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
              xs.Serialize(sw, settings);
              objectXML = sw.ToString();
            }
            
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "DELETE FROM " + tablePrefix + "DataStoreSettings " +
                                      "WHERE ExtensionType = @type AND ExtensionId = @id; ";

                    
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "type";
                    dpID.Value = exType.GetHashCode();
                    cmd.Parameters.Add(dpID);
                    DbParameter dpType = provider.CreateParameter();
                    dpType.ParameterName = parmPrefix + "id";
                    dpType.Value = exId;
                    cmd.Parameters.Add(dpType);

                    cmd.ExecuteNonQuery();

                    sqlQuery = "INSERT INTO " + tablePrefix + "DataStoreSettings " +
                        "(ExtensionType, ExtensionId, Settings) " +
                        "VALUES (@type, @id, @file)";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpFile = provider.CreateParameter();
                    dpFile.ParameterName = parmPrefix + "file";
                    dpFile.Value = objectXML; // settings.ToString(); // file;
                    cmd.Parameters.Add(dpFile);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes an item from the dataStore
        /// </summary>
        /// <param name="exType">type of item</param>
        /// <param name="exId">id of item</param>
        public override void RemoveFromDataStore(ExtensionType exType, string exId)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "DELETE FROM " + tablePrefix + "DataStoreSettings " +
                        "WHERE ExtensionType = " + parmPrefix + "type AND ExtensionId = " + parmPrefix + "id";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = parmPrefix + "type";
                    dpID.Value = exType;
                    cmd.Parameters.Add(dpID);
                    DbParameter dpType = provider.CreateParameter();
                    dpType.ParameterName = parmPrefix + "id";
                    dpType.Value = exId;
                    cmd.Parameters.Add(dpType);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Storage location on web server
        /// </summary>
        /// <returns>string with virtual path to storage</returns>
        public override string StorageLocation()
        {
            if (String.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings["StorageLocation"]))
                return @"~/app_data/";
            return System.Web.Configuration.WebConfigurationManager.AppSettings["StorageLocation"];
        }

        private void UpdateTags(IPublishable parent, DbConnection conn, DbProviderFactory provider)
        {
            int type = 0;
            if (parent is Training)
            {
                type = 1;
                
            }
            string sqlQuery = "DELETE FROM " + tablePrefix + "Tags WHERE ParentID = " + parmPrefix + "id and ParentType="+type;
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                DbParameter dpID = provider.CreateParameter();
                dpID.ParameterName = parmPrefix + "id";
                dpID.Value = parent.Id.ToString() ;
                cmd.Parameters.Add(dpID);
                cmd.ExecuteNonQuery();

                foreach (string tag in (parent is Training) ? ((Training)parent).Tags : ((Curricula)parent).Tags)
                {
                    cmd.CommandText = "INSERT INTO " + tablePrefix + "Tags (ParentID,ParentType, Tag) " +
                        "VALUES (" + parmPrefix + "parentid, " + parmPrefix + "parenttype, " + parmPrefix + "tag)";
                    cmd.Parameters.Clear();
                   
                    DbParameter dpTrainingID = provider.CreateParameter();
                    dpTrainingID.ParameterName = parmPrefix + "parentid";
                    dpTrainingID.Value = parent.Id.ToString();
                    cmd.Parameters.Add(dpTrainingID);
                    DbParameter dpParentType = provider.CreateParameter();
                    dpParentType.ParameterName = parmPrefix + "parenttype";
                    dpParentType.Value = type;
                    cmd.Parameters.Add(dpParentType);
                    DbParameter dpTag = provider.CreateParameter();
                    dpTag.ParameterName = parmPrefix + "tag";
                    dpTag.Value = tag;
                    cmd.Parameters.Add(dpTag);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateCategories(IPublishable parent, DbConnection conn, DbProviderFactory provider)
        {
            
            string table = "CurriculaCategorie";
            string key = "CurriculaID";
            if (parent is Training)
            {
                
                table = "TrainingCategorie";
                key = "TrainingID";
            }
            string sqlQuery = "DELETE FROM " + tablePrefix + table + " WHERE " + key + " = " + parmPrefix + "id";
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                DbParameter dpID = provider.CreateParameter();
                dpID.ParameterName = parmPrefix + "id";
                dpID.Value = parent.Id.ToString();
                cmd.Parameters.Add(dpID);
                cmd.ExecuteNonQuery();

                foreach (Category cat in parent.Categories)
                {
                    cmd.CommandText = "INSERT INTO " + tablePrefix + table + " (" + key + ", CategorieID) " +
                        "VALUES (" + parmPrefix + "id, " + parmPrefix + "cat)";
                    cmd.Parameters.Clear();
                    DbParameter dpPostID = provider.CreateParameter();
                    dpPostID.ParameterName = parmPrefix + "id";
                    dpPostID.Value = parent.Id.ToString();
                    cmd.Parameters.Add(dpPostID);
                    DbParameter dpCat = provider.CreateParameter();
                    dpCat.ParameterName = parmPrefix + "cat";
                    dpCat.Value = cat.Id.ToString();
                    cmd.Parameters.Add(dpCat);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        private void UpdateFields(IPublishable parent, DbConnection conn, DbProviderFactory provider)
        {

            string table = "CurriculaField";
            string key = "CurriculaID";
            if (parent is Training)
            {

                table = "TrainingField";
                key = "TrainingID";
            }
            string sqlQuery = "DELETE FROM " + tablePrefix + table + " WHERE " + key + " = " + parmPrefix + "id";
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                DbParameter dpID = provider.CreateParameter();
                dpID.ParameterName = parmPrefix + "id";
                dpID.Value = parent.Id.ToString();
                cmd.Parameters.Add(dpID);
                cmd.ExecuteNonQuery();

                foreach (Field fld in parent.Fields)
                {
                    cmd.CommandText = "INSERT INTO " + tablePrefix + table + " (" + key + ", FieldID) " +
                        "VALUES (" + parmPrefix + "id, " + parmPrefix + "fld)";
                    cmd.Parameters.Clear();
                    DbParameter dpPostID = provider.CreateParameter();
                    dpPostID.ParameterName = parmPrefix + "id";
                    dpPostID.Value = parent.Id.ToString();
                    cmd.Parameters.Add(dpPostID);
                    DbParameter dpfld = provider.CreateParameter();
                    dpfld.ParameterName = parmPrefix + "fld";
                    dpfld.Value = fld.Id.ToString();
                    cmd.Parameters.Add(dpfld);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        private void UpdateComments(IPublishable parent, DbConnection conn, DbProviderFactory provider)
        {
            int type = 0;
            if (parent is Training)
            {
                type = 1;

            }
            string sqlQuery = "DELETE FROM " + tablePrefix + "Comments WHERE ParentID = " + parmPrefix + "id and ParentType=" + type;
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                DbParameter dpID = provider.CreateParameter();
                dpID.ParameterName = parmPrefix + "id";
                dpID.Value = parent.Id.ToString();
                cmd.Parameters.Add(dpID);
                cmd.ExecuteNonQuery();

                foreach (Comment comment in parent.Comments)
                {
                    sqlQuery = "INSERT INTO " + tablePrefix + "Comments (CommentID,ParentID,ParentType,CommentDate,Author,Count,Sex,Phone,Mobile,Email,Company,QQ_msn,Comment,IsDispose,DisposeBy,Country,IP) " +
                                        "VALUES (@commentid, @parentid, @type, @date, @author,@count,@sex,@phone,@mobile, @email, @company,@qq_msn,@comment, @isapproved, @moderatedby, @country, @ip)";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.Parameters.Clear();
                    DbParameter dpCommentID = provider.CreateParameter();
                    dpCommentID.ParameterName = parmPrefix + "commentid";
                    dpCommentID.Value = comment.Id.ToString();
                    cmd.Parameters.Add(dpCommentID);
					
					DbParameter dpParentID = provider.CreateParameter();
					dpParentID.ParameterName = parmPrefix + "parentid";
					dpParentID.Value = comment.ParentId.ToString();
					cmd.Parameters.Add(dpParentID);

                    DbParameter dpParentType = provider.CreateParameter();
                    dpParentType.ParameterName = parmPrefix + "type";
                    dpParentType.Value = type;
                    cmd.Parameters.Add(dpParentType);

                    DbParameter dpCommentDate = provider.CreateParameter();
                    dpCommentDate.ParameterName = parmPrefix + "date";
                    dpCommentDate.Value = comment.DateCreated.AddHours(-TrainSettings.Instance.Timezone);
                    cmd.Parameters.Add(dpCommentDate);

                    DbParameter dpAuthor = provider.CreateParameter();
                    dpAuthor.ParameterName = parmPrefix + "author";
                    dpAuthor.Value = comment.Author;
                    cmd.Parameters.Add(dpAuthor);

                    DbParameter dpCount = provider.CreateParameter();
                    dpCount.ParameterName = parmPrefix + "count";
                    dpCount.Value = comment.Count;
                    cmd.Parameters.Add(dpCount);

                    DbParameter dpSex = provider.CreateParameter();
                    dpSex.ParameterName = parmPrefix + "sex";
                    dpSex.Value = comment.Sex;
                    cmd.Parameters.Add(dpSex);

                    DbParameter dpPhone = provider.CreateParameter();
                    dpPhone.ParameterName = parmPrefix + "phone";
                    dpPhone.Value = comment.Phone;
                    cmd.Parameters.Add(dpPhone);

                    DbParameter dpMobile = provider.CreateParameter();
                    dpMobile.ParameterName = parmPrefix + "mobile";
                    dpMobile.Value = comment.Mobile;
                    cmd.Parameters.Add(dpMobile);

                    DbParameter dpEmail = provider.CreateParameter();
                    dpEmail.ParameterName = parmPrefix + "email";
                    dpEmail.Value = comment.Email ?? "";
                    cmd.Parameters.Add(dpEmail);

                    DbParameter dpCompany = provider.CreateParameter();
                    dpCompany.ParameterName = parmPrefix + "company";
                    if (comment.Company == null)
                        dpCompany.Value = string.Empty;
                    else
                        dpCompany.Value = comment.Company.ToString();
                    cmd.Parameters.Add(dpCompany);

                    DbParameter dpQQ_msn = provider.CreateParameter();
                    dpQQ_msn.ParameterName = parmPrefix + "qq_msn";
                    if (comment.QQ_msn == null)
                        dpQQ_msn.Value = string.Empty;
                    else
                        dpQQ_msn.Value = comment.QQ_msn.ToString();
                    cmd.Parameters.Add(dpQQ_msn);

                    DbParameter dpContent = provider.CreateParameter();
                    dpContent.ParameterName = parmPrefix + "comment";
                    dpContent.Value = comment.Content;
                    cmd.Parameters.Add(dpContent);

                    DbParameter dpIsApproved = provider.CreateParameter();
                    dpIsApproved.ParameterName = parmPrefix + "isapproved";
                    dpIsApproved.Value = comment.IsApproved;
                    cmd.Parameters.Add(dpIsApproved);

                    DbParameter dpModeratedBy = provider.CreateParameter();
                    dpModeratedBy.ParameterName = parmPrefix + "moderatedby";
                    dpModeratedBy.Value = comment.ModeratedBy ?? string.Empty;
                    cmd.Parameters.Add(dpModeratedBy);

                    DbParameter dpCountry = provider.CreateParameter();
                    dpCountry.ParameterName = parmPrefix + "country";
                    dpCountry.Value = comment.Country ?? string.Empty;
                    cmd.Parameters.Add(dpCountry);

                    DbParameter dpIP = provider.CreateParameter();
                    dpIP.ParameterName = parmPrefix + "ip";
                    dpIP.Value = comment.IP ?? string.Empty;
                    cmd.Parameters.Add(dpIP);

                   
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateCurriculaInfos(Curricula parent, DbConnection conn, DbProviderFactory provider)
        {
            string table = "CurriculaInfo";
            string key = "CurriculaID";
            
            string sqlQuery = "DELETE FROM " + tablePrefix + table + " WHERE " + key + " = " + parmPrefix + "id";
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                DbParameter dpID = provider.CreateParameter();
                dpID.ParameterName = parmPrefix + "id";
                dpID.Value = parent.Id.ToString();
                cmd.Parameters.Add(dpID);
                cmd.ExecuteNonQuery();

                foreach (CurriculaInfo info in parent.CurriculaInfos)
                {
                    cmd.CommandText = "INSERT INTO " + tablePrefix + table + " (" + key + ", InfoID,StartDate,EndDate,Cast,CityTown,IsPublished) " +
                        "VALUES (" + parmPrefix + "pid, " + parmPrefix + "InfoID, " + parmPrefix + "StartDate, " + parmPrefix + "EndDate, " + parmPrefix + "Cast, " + parmPrefix + "CityTown, " + parmPrefix + "IsPublished)";
                    cmd.Parameters.Clear();
                    DbParameter dpcID = provider.CreateParameter();
                    dpcID.ParameterName = parmPrefix + "pid";
                    dpcID.Value = parent.Id.ToString();
                    cmd.Parameters.Add(dpcID);

                    DbParameter dpInfoID = provider.CreateParameter();
                    dpInfoID.ParameterName = parmPrefix + "InfoID";
                    dpInfoID.Value = info.Id.ToString();
                    cmd.Parameters.Add(dpInfoID);

                    DbParameter dpStartDate = provider.CreateParameter();
                    dpStartDate.ParameterName = parmPrefix + "StartDate";
                    dpStartDate.Value = info.StartDate;
                    cmd.Parameters.Add(dpStartDate);

                    DbParameter dpEndDate = provider.CreateParameter();
                    dpEndDate.ParameterName = parmPrefix + "EndDate";
                    dpEndDate.Value = info.EndDate;
                    cmd.Parameters.Add(dpEndDate);

                    DbParameter dpCast = provider.CreateParameter();
                    dpCast.ParameterName = parmPrefix + "Cast";
                    dpCast.Value = info.Cast;
                    cmd.Parameters.Add(dpCast);

                    DbParameter dpCityTown = provider.CreateParameter();
                    dpCityTown.ParameterName = parmPrefix + "CityTown";
                    dpCityTown.Value = info.CityTown;
                    cmd.Parameters.Add(dpCityTown);

                    DbParameter dpIsPublished = provider.CreateParameter();
                    dpIsPublished.ParameterName = parmPrefix + "IsPublished";
                    dpIsPublished.Value = info.IsPublished;
                    cmd.Parameters.Add(dpIsPublished);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        private void UpdateRessForExcellent(Excellent parent, DbConnection conn, DbProviderFactory provider)
        {
            string table = "ExcellentRes";
            string key = "ExcellentID";

            string sqlQuery = "DELETE FROM " + tablePrefix + table + " WHERE " + key + " = " + parmPrefix + "id";
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                DbParameter dpID = provider.CreateParameter();
                dpID.ParameterName = parmPrefix + "id";
                dpID.Value = parent.Id.ToString();
                cmd.Parameters.Add(dpID);
                cmd.ExecuteNonQuery();

                foreach (Res info in parent.Ress)
                {
                    cmd.CommandText = "INSERT INTO " + tablePrefix + table + " (" + key + ", ResID) " +
                        "VALUES (" + parmPrefix + "pid, " + parmPrefix + "ResID)";
                    cmd.Parameters.Clear();
                    DbParameter dpcID = provider.CreateParameter();
                    dpcID.ParameterName = parmPrefix + "pid";
                    dpcID.Value = parent.Id.ToString();
                    cmd.Parameters.Add(dpcID);

                    DbParameter dpInfoID = provider.CreateParameter();
                    dpInfoID.ParameterName = parmPrefix + "ResID";
                    dpInfoID.Value = info.Id.ToString();
                    cmd.Parameters.Add(dpInfoID);

                    cmd.ExecuteNonQuery();

                }
            }

        }

        
        /// <summary>
        /// Loads AuthorProfile from database
        /// </summary>
        /// <param name="id">username</param>
        /// <returns></returns>
		public override AuthorProfile SelectProfile(string id)
		{
            StringDictionary dic = new StringDictionary();
            AuthorProfile profile = new AuthorProfile(id);

            // Retrieve Profile data from Db
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT SettingName, SettingValue FROM " + tablePrefix + "Profiles " +
                                        "WHERE UserName = " + parmPrefix + "name";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpName = provider.CreateParameter();
                    dpName.ParameterName = parmPrefix + "name";
                    dpName.Value = id;
                    cmd.Parameters.Add(dpName);

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            dic.Add(rdr.GetString(0), rdr.GetString(1));
                        }
                    }
                }
            }
            if (dic.ContainsKey("IsPrivate"))
                profile.IsPrivate = dic["IsPrivate"].ToLower() == "true";
            if (dic.ContainsKey("IsVip"))
                profile.IsVip = dic["IsVip"].ToLower() == "true";
            if (dic.ContainsKey("IsGoldTch"))
                profile.IsGoldTch = dic["IsGoldTch"].ToLower() == "true";
            // Load profile with data from dictionary
            if (dic.ContainsKey("DisplayName"))
                profile.DisplayName = dic["DisplayName"];

            if (dic.ContainsKey("PhotoURL"))
                profile.PhotoURL = dic["PhotoURL"];

            if (dic.ContainsKey("CityTown"))
                profile.CityTown = dic["CityTown"];

            if (dic.ContainsKey("RegionState"))
                profile.RegionState = dic["RegionState"];

            if (dic.ContainsKey("PhoneMain"))
                profile.PhoneMain = dic["PhoneMain"];

            if (dic.ContainsKey("PhoneMobile"))
                profile.PhoneMobile = dic["PhoneMobile"];

            if (dic.ContainsKey("PhoneFax"))
                profile.PhoneFax = dic["PhoneFax"];

            if (dic.ContainsKey("Company"))
                profile.Company = dic["Company"];

            if (dic.ContainsKey("Pay"))
                profile.Pay = dic["Pay"];

            if (dic.ContainsKey("Fields"))
                profile.Fields = dic["Fields"];

            if (dic.ContainsKey("Address"))
                profile.Address = dic["Address"];

            if (dic.ContainsKey("MSN_QQ"))
                profile.Address = dic["MSN_QQ"];

            if (dic.ContainsKey("AboutMe"))
                profile.AboutMe = dic["AboutMe"];

            if (dic.ContainsKey("Description1"))
                profile.Description1 = dic["Description1"];

            if (dic.ContainsKey("Description2"))
                profile.Description2 = dic["Description2"];

            if (dic.ContainsKey("WebSit"))
                profile.WebSit = dic["WebSit"];

            if (dic.ContainsKey("Points"))
                profile.Points = dic["Points"];

            if (dic.ContainsKey("Scores"))
                profile.Scores = dic["Scores"];
            if (dic.ContainsKey("NoMess"))
                profile.NoMess = dic["NoMess"];
            if (dic.ContainsKey("ViewCount"))
                profile.ViewCount = Convert.ToInt32(dic["ViewCount"]);
		    return profile;
		}

        /// <summary>
        /// Adds AuthorProfile to database
        /// </summary>
        /// <param name="profile"></param>
		public override void InsertProfile(AuthorProfile profile)
		{
			UpdateProfile(profile);
		}

        /// <summary>
        /// Updates AuthorProfile to database
        /// </summary>
        /// <param name="profile"></param>
		public override void UpdateProfile(AuthorProfile profile)
		{
			// Remove Profile
            DeleteProfile(profile);

            // Create Profile Dictionary
            StringDictionary dic = new StringDictionary();

            if (!String.IsNullOrEmpty(profile.DisplayName))
                dic.Add("DisplayName", profile.DisplayName);
            if (!String.IsNullOrEmpty(profile.PhotoURL))
                dic.Add("PhotoURL", profile.PhotoURL);
            if (!String.IsNullOrEmpty(profile.CityTown))
                dic.Add("CityTown", profile.CityTown);
            if (!String.IsNullOrEmpty(profile.RegionState))
                dic.Add("RegionState", profile.RegionState);
            if (!String.IsNullOrEmpty(profile.PhoneMain))
                dic.Add("PhoneMain", profile.PhoneMain);
            if (!String.IsNullOrEmpty(profile.PhoneMobile))
                dic.Add("PhoneMobile", profile.PhoneMobile);
            if (!String.IsNullOrEmpty(profile.PhoneFax))
                dic.Add("PhoneFax", profile.PhoneFax);
            if (!String.IsNullOrEmpty(profile.Company))
                dic.Add("Company", profile.Company);
            if (!String.IsNullOrEmpty(profile.Pay))
                dic.Add("Pay", profile.Pay);
            if (!String.IsNullOrEmpty(profile.Fields))
                dic.Add("Fields", profile.Fields);
            if (!String.IsNullOrEmpty(profile.Address))
                dic.Add("Address", profile.Address);
            if (!String.IsNullOrEmpty(profile.MSN_QQ))
                dic.Add("MSN_QQ", profile.MSN_QQ);
            if (!String.IsNullOrEmpty(profile.AboutMe))
                dic.Add("AboutMe", profile.AboutMe);
            if (!String.IsNullOrEmpty(profile.Description1))
                dic.Add("Description1", profile.Description1);
            if (!String.IsNullOrEmpty(profile.Description2))
                dic.Add("Description2", profile.Description2);
            if (!String.IsNullOrEmpty(profile.WebSit))
                dic.Add("WebSit", profile.WebSit);
            if (!String.IsNullOrEmpty(profile.Points))
                dic.Add("Points", profile.Points);
            if (!String.IsNullOrEmpty(profile.Scores))
                dic.Add("Scores", profile.Scores);
            if (!String.IsNullOrEmpty(profile.NoMess))
                dic.Add("NoMess", profile.NoMess);
            dic.Add("ViewCount",profile.ViewCount.ToString());
            dic.Add("IsPrivate", profile.IsPrivate.ToString());
            dic.Add("IsVip", profile.IsVip.ToString());
            dic.Add("IsGoldTch", profile.IsGoldTch.ToString());
                // Save Profile Dictionary
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    foreach (string key in dic.Keys)
                    {
                        string sqlQuery = "INSERT INTO " + tablePrefix + "Profiles (UserName, SettingName, SettingValue) " +
                                          "VALUES (@user, @name, @value)";
                        if (parmPrefix != "@")
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        cmd.CommandText = sqlQuery;
                        cmd.Parameters.Clear();

                        DbParameter dpUser = provider.CreateParameter();
                        dpUser.ParameterName = parmPrefix + "user";
                        dpUser.Value = profile.Id;
                        cmd.Parameters.Add(dpUser);

                        DbParameter dpName = provider.CreateParameter();
                        dpName.ParameterName = parmPrefix + "name";
                        dpName.Value = key;
                        cmd.Parameters.Add(dpName);

                        DbParameter dpValue = provider.CreateParameter();
                        dpValue.ParameterName = parmPrefix + "value";
                        dpValue.Value = dic[key];
                        cmd.Parameters.Add(dpValue);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
		}

        /// <summary>
        /// Remove AuthorProfile from database
        /// </summary>
        /// <param name="profile"></param>
		public override void DeleteProfile(AuthorProfile profile)
		{
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "DELETE FROM " + tablePrefix + "Profiles " +
                                      "WHERE UserName = " + parmPrefix + "name";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpName = provider.CreateParameter();
                    dpName.ParameterName = parmPrefix + "name";
                    dpName.Value = profile.Id;
                    cmd.Parameters.Add(dpName);

                    cmd.ExecuteNonQuery();
                }
            }
		}

        /// <summary>
        /// Return collection for AuthorProfiles from database
        /// </summary>
        /// <returns></returns>
		public override List<AuthorProfile> FillProfiles()
		{
            List<AuthorProfile> profiles = new List<AuthorProfile>();
            List<string> profileNames = new List<string>();
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT UserName FROM " + tablePrefix + "Profiles " +
                                      "GROUP BY UserName";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    using (DbDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            profileNames.Add(rdr.GetString(0));
                        }
                    }
                }
            }

		    foreach (string name in profileNames)
		    {
		        profiles.Add(BusinessBase<AuthorProfile, string>.Load(name));
		    }

		    return profiles;
		}

        public override void UpdateViewCount(string table,string whereString, string viewCount)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "UPDATE " + tablePrefix + table +
                                  " SET ViewCount=@ViewCount " +
                                  "WHERE " + whereString;
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    
                    DbParameter dpViewCount = provider.CreateParameter();
                    dpViewCount.ParameterName = parmPrefix + "ViewCount";
                    dpViewCount.Value = viewCount;
                    cmd.Parameters.Add(dpViewCount);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public override void UpdateViewCount(AuthorProfile ap)
        {
            string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connString;
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "UPDATE " + tablePrefix + "Profiles "+
                                  "SET SettingValue=@ViewCount " +
                                  "WHERE UserName='" + ap.Id.ToString() + "' and SettingName='ViewCount'";
                    if (parmPrefix != "@")
                        sqlQuery = sqlQuery.Replace("@", parmPrefix);
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;


                    DbParameter dpViewCount = provider.CreateParameter();
                    dpViewCount.ParameterName = parmPrefix + "ViewCount";
                    dpViewCount.Value = ap.ViewCount;
                    cmd.Parameters.Add(dpViewCount);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
