using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;
using System.Globalization;
using TrainEngine.Core.Providers;
using System.ComponentModel;

namespace TrainEngine.Core.Classes
{
    public class Curricula : BusinessBase<Curricula, Guid>, IComparable<Curricula>, IPublishable
    {
        #region Constructor

		/// <summary>
		/// The default contstructor assign default values.
		/// </summary>
        public Curricula()
		{
			base.Id = Guid.NewGuid();
			_Comments = new List<Comment>();
            _CurriculaInfos = new StateList<CurriculaInfo>();
			_Categories = new StateList<Category>();
            _Fields = new StateList<Field>();
			_Tags = new StateList<string>();
			DateCreated = DateTime.Now;
			_IsPublished = false;
            _IsGold = false;
            _ViewCount = 0;
		}

		#endregion

        #region Properties

        private string _Author;
        /// <summary>
        /// Gets or sets the Author or the post.
        /// </summary>
        public string Author
        {
            get { return _Author; }
            set
            {
                if (_Author != value) MarkChanged("Author");
                _Author = value;
            }
        }

        public AuthorProfile AuthorProfile
        {
            get { return AuthorProfile.GetProfile(Author); }
        }

        private string _Title;
        /// <summary>
        /// Gets or sets the Title or the post.
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value) MarkChanged("Title");
                _Title = value;
            }
        }

        private string _ObjectDes;
        public string ObjectDes
        {
            get { return _ObjectDes; }
            set
            {
                if (_ObjectDes != value) MarkChanged("ObjectDes");
                _ObjectDes = value;
            }
        }

        /// <summary>
        /// Gets or sets the Description or the post.
        /// </summary>
        public string Description
        {
            get { return string.Empty; }
        }

        private string _Content;
        /// <summary>
        /// Gets or sets the Content or the post.
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set
            {
                if (_Content != value)
                {
                    MarkChanged("CurriculaContent");
                    HttpContext.Current.Cache.Remove("CurriculaContent_" + this.Id);
                    _Content = value;
                }
            }
        }

        private readonly List<Comment> _Comments;
        /// <summary>
        /// A collection of Approved comments for the post sorted by date.
        /// </summary>
        public List<Comment> DisposedComments
        {
            get { return _Comments.FindAll(c => c.IsApproved); }
        }

        /// <summary>
        /// A collection of comments waiting for approval for the post, sorted by date.
        /// </summary>
        public List<Comment> NotDisposedComments
        {
            get { return _Comments.FindAll(c => !c.IsApproved); }
        }

        /// <summary>
        /// A Collection of All Comments for the post
        /// </summary>
        public List<Comment> Comments
        {
            get { return _Comments; }

        }

        private StateList<CurriculaInfo> _CurriculaInfos;
        /// <summary>
        /// An unsorted List of CurriculaInfos.
        /// </summary>
        public StateList<CurriculaInfo> CurriculaInfos
        {
            get { return _CurriculaInfos; }
        }

        private StateList<Category> _Categories;
        /// <summary>
        /// An unsorted List of categories.
        /// </summary>
        public StateList<Category> Categories
        {
            get { return _Categories; }
        }

        private StateList<Field> _Fields;
        /// <summary>
        /// An unsorted collection of fields.
        /// </summary>
        public StateList<Field> Fields
        {
            get { return _Fields; }
        }

        private StateList<string> _Tags;
        /// <summary>
        /// An unsorted collection of tags.
        /// </summary>
        public StateList<string> Tags
        {
            get { return _Tags; }
        }

        private bool _IsGold;
        /// <summary>
        /// Gets or sets whether or not the post is Gold.
        /// </summary>
        public bool IsGold
        {
            get { return _IsGold; }
            set
            {
                if (_IsGold != value) MarkChanged("IsGold");
                _IsGold = value;
            }
        }

        private int _ViewCount;
        public int ViewCount
        {
            get { return _ViewCount; }
            set
            {
                if (_ViewCount != value) MarkChanged("ViewCount");
                _ViewCount = value;
            }
        }

        private bool _IsPublished;
        /// <summary>
        /// Gets or sets whether or not the post is published.
        /// </summary>
        public bool IsPublished
        {
            get { return _IsPublished; }
            set
            {
                if (_IsPublished != value) MarkChanged("IsPublished");
                _IsPublished = value;
            }
        }
        private int _Points;
        /// <summary>
        /// Gets or sets the Points of the Curricula.
        /// </summary>
        public int Points
        {
            get { return _Points; }
            set
            {
                if (_Points != value) MarkChanged("Points");
                _Points = value;
            }
        }

        private int _Scores;
        /// <summary>
        /// Gets or sets the Scores of the Curricula.
        /// </summary>
        public int Scores
        {
            get { return _Scores; }
            set
            {
                if (_Scores != value) MarkChanged("Scores");
                _Scores = value;
            }
        }
        private string _Appuser;
        public string Appuser
        {
            get { return _Appuser; }
            set
            {
                if (_Appuser != value) MarkChanged("Scores");
                _Appuser = value;
            }
        }
       

        

        /// <summary>
        /// Gets whether or not the post is visible or not.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                if (IsAuthenticated || (IsPublished && DateCreated <= DateTime.Now.AddHours(TrainSettings.Instance.Timezone)))
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Gets whether a post is available to visitors not logged into the blog.
        /// </summary>
        public bool IsVisibleToPublic
        {
            get
            {
                return IsPublished && DateCreated <= DateTime.Now.AddHours(TrainSettings.Instance.Timezone);
            }
        }

        private Curricula _Prev;
        /// <summary>
        /// Gets the previous post relative to this one based on time.
        /// <remarks>
        /// If this post is the oldest, then it returns null.
        /// </remarks>
        /// </summary>
        public Curricula Previous
        {
            get { return _Prev; }
        }

        private Curricula _Next;
        /// <summary>
        /// Gets the next post relative to this one based on time.
        /// <remarks>
        /// If this post is the newest, then it returns null.
        /// </remarks>
        /// </summary>
        public Curricula Next
        {
            get { return _Next; }
        }

        #endregion

        #region Links

        /// <summary>
        /// The absolute permanent link to the Training.
        /// </summary>
        public Uri PermaLink
        {
            get { return new Uri(Utils.AbsoluteWebRoot.ToString() + "Curricula.aspx?id=" + Id.ToString()); }
        }

        /// <summary>
        /// A relative-to-the-site-root path to the post.
        /// Only for in-site use.
        /// </summary>
        public string RelativeLink
        {
            get
            {
                string slug = Utils.RemoveIllegalCharacters(this.Title) + TrainSettings.Instance.FileExtension;

                if (TrainSettings.Instance.TimeStampPostLinks)
                    return Utils.RelativeWebRoot + "Curricula/" + DateCreated.ToString("yyyy/MM/dd/", CultureInfo.InvariantCulture) + slug;

                return Utils.RelativeWebRoot + "Curricula/" + slug;
            }
        }

        /// <summary>
        /// The absolute link to the post.
        /// </summary>
        public Uri AbsoluteLink
        {
            get { return Utils.ConvertToAbsolute(RelativeLink); }
        }

        /// <summary>
        /// Gets the relative link to the feed for this category's posts.
        /// </summary>
        public string FeedRelativeLink
        {
            get
            {
                return Utils.RelativeWebRoot + "Curricula/feed/" + Utils.RemoveIllegalCharacters(this.Title) + TrainSettings.Instance.FileExtension;
            }
        }

        /// <summary>
        /// Gets the absolute link to the feed for this category's posts.
        /// </summary>
        public Uri FeedAbsoluteLink
        {
            get { return Utils.ConvertToAbsolute(FeedRelativeLink); }
        }

        #endregion

        #region Methods

        private static object _SyncRoot = new object();
        private static List<Curricula> _Curriculas;

        /// <summary>
        /// A sorted collection of all Trainings in the Web.
        /// Sorted by date.
        /// </summary>
        public static List<Curricula> Curriculas
        {
            get
            {
                if (_Curriculas == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Curriculas == null)
                        {
                            _Curriculas = TrainService.FillCurriculas();
                            _Curriculas.TrimExcess();
                            AddRelations();
                        }
                    }
                }

                return _Curriculas;
            }
        }

        /// <summary>
        /// Sets the Previous and Next properties to all posts.
        /// </summary>
        private static void AddRelations()
        {
            for (int i = 0; i < _Curriculas.Count; i++)
            {
                _Curriculas[i]._Next = null;
                _Curriculas[i]._Prev = null;
                if (i > 0)
                    _Curriculas[i]._Next = _Curriculas[i - 1];

                if (i < _Curriculas.Count - 1)
                    _Curriculas[i]._Prev = _Curriculas[i + 1];
            }
        }

        /// <summary>
        /// Returns all Curriculas in the specified category
        /// </summary>
        public static List<Curricula> GetCurriculasByCategory(Guid categoryId)
        {
            Category cat = Category.GetCategory(categoryId);
            List<Curricula> col = _Curriculas.FindAll(p => p.Categories.Contains(cat));
            col.Sort();
            col.TrimExcess();
            return col;
        }

        /// <summary>
        /// Returns all Curriculas in the specified Field
        /// </summary>
        public static List<Curricula> GetCurriculasByField(Guid fieldId)
        {
            Field fld = Field.GetField(fieldId);
            List<Curricula> col = Curriculas.FindAll(p => p.Fields.Contains(fld));
            col.Sort();
            col.TrimExcess();
            return col;
        }
        /// <summary>
        /// Returns all Curriculas in the specified Field
        /// </summary>
        public static Curricula GetCurriculaByCurriculaInfo(Guid infoId)
        {
            CurriculaInfo info = CurriculaInfo.GetCurriculaInfo(infoId);
            return Curricula.GetCurricula(info.CurriculaId);
        }
        /// <summary>
        /// Returs a Curricula based on the specified id.
        /// </summary>
        public static Curricula GetCurricula(Guid id)
        {
            return Curriculas.Find(p => p.Id == id);
        }

        /// <summary>
        /// Checks to see if the specified title has already been used
        /// by another Curricula.
        /// <remarks>
        /// Titles must be unique because the title is part of the URL.
        /// </remarks>
        /// </summary>
        public static bool IsTitleUnique(string title)
        {
            string legal = Utils.RemoveIllegalCharacters(title);
            foreach (Curricula training in Curriculas)
            {
                if (Utils.RemoveIllegalCharacters(training.Title).Equals(legal, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns all posts written by the specified author.
        /// </summary>
        public static List<Curricula> GetCurriculasByAuthor(string author)
        {
            string legalAuthor = Utils.RemoveIllegalCharacters(author);
            List<Curricula> list = Curriculas.FindAll(delegate(Curricula p)
            {
                string legalTitle = Utils.RemoveIllegalCharacters(p.Author);
                return legalAuthor.Equals(legalTitle, StringComparison.OrdinalIgnoreCase);
            });

            list.TrimExcess();
            return list;
        }

        /// <summary>
        /// Returns all posts tagged with the specified tag.
        /// </summary>
        public static List<Curricula> GetCurriculasByTag(string tag)
        {
            tag = Utils.RemoveIllegalCharacters(tag);
            List<Curricula> list = Curriculas.FindAll(delegate(Curricula p)
            {
                foreach (string t in p.Tags)
                {
                    if (Utils.RemoveIllegalCharacters(t).Equals(tag, StringComparison.OrdinalIgnoreCase))
                        return true;
                }

                return false;
            });

            list.TrimExcess();
            return list;
        }

        /// <summary>
        /// Returns all posts published between the two dates.
        /// </summary>
        public static List<Curricula> GetCurriculasByDate(DateTime dateFrom, DateTime dateTo)
        {
            List<Curricula> list = Curriculas.FindAll(p => p.DateCreated.Date >= dateFrom && p.DateCreated.Date <= dateTo);
            list.TrimExcess();
            return list;
        }

        /// <summary>
        /// Imports Training (without all standard saving routines
        /// </summary>
        public void Import()
        {
            if (this.IsDeleted)
            {
                if (!this.IsNew)
                {
                    TrainService.DeleteCurricula(this);
                }
            }
            else
            {
                if (this.IsNew)
                {
                    TrainService.InsertCurricula(this);
                }
                else
                {
                    TrainService.UpdateCurricula(this);
                }
            }
        }
        public void UpdateViewCount()
        {
            TrainService.UpdateViewCount("Curriculas", "CurriculaID='" + this.Id.ToString() + "'", this.ViewCount.ToString());
        }
        /// <summary>
        /// Force reload of all posts
        /// </summary>
        public static void Reload()
        {
            _Curriculas = TrainService.FillCurriculas();
            _Curriculas.Sort();
            AddRelations();
        }
        /// <summary>
        /// Adds a CurriculaInfo to the collection and saves the Curricula.
        /// </summary>
        /// <param name="crl">The comment to add to the post.</param>
        public void AddCurriculaInfo(CurriculaInfo crl)
        {
            CurriculaInfo.CurriculaInfos.Add(crl);
            CurriculaInfos.Add(crl);
            DataUpdate();
        }
        /// <summary>
        /// Adds a CurriculaInfo to the collection and saves the Curricula.
        /// </summary>
        /// <param name="crl">The comment to add to the post.</param>
        public void RemoveCurriculaInfo(CurriculaInfo crl)
        {
            CurriculaInfos.Remove(crl);
            CurriculaInfo.CurriculaInfos.Remove(crl);
            DataUpdate();
        }
        /// <summary>
        /// Adds a comment to the collection and saves the post.
        /// </summary>
        /// <param name="comment">The comment to add to the post.</param>
        public void AddComment(Comment comment)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnAddingComment(comment, e);
            if (!e.Cancel)
            {
                Comments.Add(comment);
                DataUpdate();
                OnCommentAdded(comment);

            }
        }

        /// <summary>
        /// Updates a comment in the collection and saves the post.
        /// </summary>
        /// <param name="comment">The comment to update in the post.</param>
        public void UpdateComment(Comment comment)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnUpdatingComment(comment, e);
            if (!e.Cancel)
            {
                int inx = Comments.IndexOf(comment);

                Comments[inx].IsApproved = comment.IsApproved;
                Comments[inx].Content = comment.Content;
                Comments[inx].Author = comment.Author;
                Comments[inx].Country = comment.Country;
                Comments[inx].Email = comment.Email;
                Comments[inx].IP = comment.IP;
                Comments[inx].ModeratedBy = comment.ModeratedBy;

                DateModified = DateTime.Now;
                DataUpdate();

                DataUpdate();
                OnCommentUpdated(comment);
            }
        }

        /// <summary>
        /// Imports a comment to comment collection and saves.  Does not
        /// notify user or run extension events.
        /// </summary>
        /// <param name="comment">The comment to add to the post.</param>
        public void ImportComment(Comment comment)
        {
            Comments.Add(comment);
            DataUpdate();

        }



        /// <summary>
        /// Removes a comment from the collection and saves the post.
        /// </summary>
        /// <param name="comment">The comment to remove from the post.</param>
        public void RemoveComment(Comment comment)
        {
            CancelEventArgs e = new CancelEventArgs();
            OnRemovingComment(comment, e);
            if (!e.Cancel)
            {
                Comments.Remove(comment);
                DataUpdate();
                OnCommentRemoved(comment);
                comment = null;
            }
        }

        /// <summary>
        /// Approves a Comment for publication.
        /// </summary>
        /// <param name="comment">The Comment to approve</param>
        public void ApproveComment(Comment comment)
        {
            CancelEventArgs e = new CancelEventArgs();
            Comment.OnApproving(comment, e);
            if (!e.Cancel)
            {
                int inx = Comments.IndexOf(comment);
                Comments[inx].IsApproved = true;
                this.DateModified = comment.DateCreated;
                this.DataUpdate();
                Comment.OnApproved(comment);
            }
        }

        /// <summary>
        /// Approves all the comments in a post.  Included to save time on the approval process.
        /// </summary>
        public void ApproveAllComments()
        {
            foreach (Comment comment in Comments)
            {
                ApproveComment(comment);
            }
        }

        #endregion

        #region Base overrides



        /// <summary>
        /// Validates the Post instance.
        /// </summary>
        protected override void ValidationRules()
        {
            AddRule("Title", "Title must be set", String.IsNullOrEmpty(Title));
            AddRule("Content", "Content must be set", String.IsNullOrEmpty(Content));
        }

        /// <summary>
        /// Returns a Training based on the specified id.
        /// </summary>
        protected override Curricula DataSelect(Guid id)
        {
            return TrainService.SelectCurricula(id);
        }

        /// <summary>
        /// Updates the Training.
        /// </summary>
        protected override void DataUpdate()
        {
            TrainService.UpdateCurricula(this);
            Curriculas.Sort();
            AddRelations();
        }

        /// <summary>
        /// Inserts a new Training to the current TrainProvider.
        /// </summary>
        protected override void DataInsert()
        {
            TrainService.InsertCurricula(this);

            if (this.IsNew)
            {
                Curriculas.Add(this);
                Curriculas.Sort();
                AddRelations();
            }
        }

        /// <summary>
        /// Deletes the Post from the current TrainProvider.
        /// </summary>
        protected override void DataDelete()
        {
            TrainService.DeleteCurricula(this);
            if (Curriculas.Contains(this))
            {
                Curriculas.Remove(this);
                Dispose();
                AddRelations();
            }
        }

        /// <summary>
        /// Gets if the Post have been changed.
        /// </summary>
        public override bool IsChanged
        {
            get
            {
                if (base.IsChanged)
                    return true;

                if (Categories.IsChanged || Tags.IsChanged || Fields.IsChanged)
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return Title;
        }

        /// <summary>
        /// Marks the object as being an clean,
        /// which means not dirty.
        /// </summary>
        public override void MarkOld()
        {
            this.Categories.MarkOld();
            this.Tags.MarkOld();
            this.Fields.MarkOld();
            
            base.MarkOld();
        }


        #endregion

        #region IComparable<Training> Members

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the 
        /// objects being compared. The return value has the following meanings: 
        /// Value Meaning Less than zero This object is less than the other parameter.Zero 
        /// This object is equal to other. Greater than zero This object is greater than other.
        /// </returns>
        public int CompareTo(Curricula other)
        {
            return other.DateCreated.CompareTo(this.DateCreated);
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs before a new comment is added.
        /// </summary>
        public static event EventHandler<CancelEventArgs> AddingComment;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnAddingComment(Comment comment, CancelEventArgs e)
        {
            if (AddingComment != null)
            {
                AddingComment(comment, e);
            }
        }

        /// <summary>
        /// Occurs when a comment is added.
        /// </summary>
        public static event EventHandler<EventArgs> CommentAdded;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnCommentAdded(Comment comment)
        {
            if (CommentAdded != null)
            {
                CommentAdded(comment, new EventArgs());
            }
        }

        /// <summary>
        /// Occurs before a new comment is updated.
        /// </summary>
        public static event EventHandler<CancelEventArgs> UpdatingComment;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnUpdatingComment(Comment comment, CancelEventArgs e)
        {
            if (UpdatingComment != null)
            {
                UpdatingComment(comment, e);
            }
        }

        /// <summary>
        /// Occurs when a comment is updated.
        /// </summary>
        public static event EventHandler<EventArgs> CommentUpdated;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnCommentUpdated(Comment comment)
        {
            if (CommentUpdated != null)
            {
                CommentUpdated(comment, new EventArgs());
            }
        }

        /// <summary>
        /// Occurs before comment is removed.
        /// </summary>
        public static event EventHandler<CancelEventArgs> RemovingComment;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnRemovingComment(Comment comment, CancelEventArgs e)
        {
            if (RemovingComment != null)
            {
                RemovingComment(comment, e);
            }
        }

        /// <summary>
        /// Occurs when a comment has been removed.
        /// </summary>
        public static event EventHandler<EventArgs> CommentRemoved;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnCommentRemoved(Comment comment)
        {
            if (CommentRemoved != null)
            {
                CommentRemoved(comment, new EventArgs());
            }
        }



        /// <summary>
        /// Occurs when the post is being served to the output stream.
        /// </summary>
        public static event EventHandler<ServingEventArgs> Serving;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        public static void OnServing(Training training, ServingEventArgs arg)
        {
            if (Serving != null)
            {
                Serving(training, arg);
            }
        }

        /// <summary>
        /// Raises the Serving event
        /// </summary>
        public void OnServing(ServingEventArgs eventArgs)
        {
            if (Serving != null)
            {
                Serving(this, eventArgs);
            }
        }

        #endregion
    }
}
