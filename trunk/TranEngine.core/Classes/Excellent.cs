using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using TrainEngine.Core.Providers;

namespace TrainEngine.Core.Classes
{
    public class Excellent : BusinessBase<Excellent, Guid>, IComparable<Excellent>
    {
        #region Constructor

		/// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
		/// </summary>
		public Excellent()
		{
			Id = Guid.NewGuid();
            _Ress = new StateList<Res>();
            //TempAddRess = new Dictionary<Res,string>();
		}

        private string _Title;
        /// <summary>
        /// Gets or sets the Title or the object.
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

        private string _Author;
        /// <summary>
        /// Gets or sets the Title or the object.
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

        private string _Description;
        /// <summary>
        /// Gets or sets the Description of the object.
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value) MarkChanged("Description");
                _Description = value;
            }
        }

        private string _CityTown;
        public string CityTown
        {
            get { return _CityTown; }
            set
            {
                if (_CityTown != value) MarkChanged("CityTown");
                _CityTown = value;
            }
        }

        private string _Teacher;
        public string Teacher
        {
            get { return _Teacher; }
            set
            {
                if (_Teacher != value) MarkChanged("Teacher");
                _Teacher = value;
            }
        }

        private DateTime _TrainingDate;
        public DateTime TrainingDate
        {
            get { return _TrainingDate; }
            set
            {
                if (_TrainingDate != value) MarkChanged("TrainingDate");
                _TrainingDate = value;
            }
        }

        private Guid _MastPic;
        public Guid MastPic
        {
            get { return _MastPic; }
            set
            {
                if (_MastPic != value) MarkChanged("MastPic");
                _MastPic = value;
            }
        }
        private bool _IsPublished;
        public bool IsPublished
        {
            get { return _IsPublished; }
            set
            {
                if (_IsPublished != value) MarkChanged("IsPublished");
                _IsPublished = value;
            }
        }
        private string _NoMess;
        public string NoMess
        {
            get { return _NoMess; }
            set
            {
                if (value != _NoMess) MarkChanged("NoMess");
                _NoMess = value;
            }
        }
        private StateList<Res> _Ress;
        /// <summary>
        /// An unsorted List of CurriculaInfos.
        /// </summary>
        public StateList<Res> Ress
        {
            get { return _Ress; }
        }

        //public Dictionary<Res, string> TempAddRess;
        private Excellent _Prev;
        /// <summary>
        /// Gets the previous post relative to this one based on time.
        /// <remarks>
        /// If this Excellent is the oldest, then it returns null.
        /// </remarks>
        /// </summary>
        public Excellent Previous
        {
            get { return _Prev; }
        }

        private Excellent _Next;
        /// <summary>
        /// Gets the next Excellent relative to this one based on time.
        /// <remarks>
        /// If this post is the newest, then it returns null.
        /// </remarks>
        /// </summary>
        public Excellent Next
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
            get { return new Uri(Utils.AbsoluteWebRoot.ToString() + "Excellent.aspx?id=" + Id.ToString()); }
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
                    return Utils.RelativeWebRoot + "Excellent/" + DateCreated.ToString("yyyy/MM/dd/", CultureInfo.InvariantCulture) + slug;

                return Utils.RelativeWebRoot + "Excellent/" + slug;
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
                return Utils.RelativeWebRoot + "Excellent/feed/" + Utils.RemoveIllegalCharacters(this.Title) + TrainSettings.Instance.FileExtension;
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
        /// <summary>
        /// Gets the full title with Parent names included
        /// </summary>
        public string CompleteTitle()
        {

            return _Title;

        }
        #region Methods

        private static object _SyncRoot = new object();
        private static List<Excellent> _Excellents;

        /// <summary>
        /// A sorted collection of all Trainings in the Web.
        /// Sorted by date.
        /// </summary>
        public static List<Excellent> Excellents
        {
            get
            {
                if (_Excellents == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Excellents == null)
                        {
                            _Excellents = TrainService.FillExcellents();
                            _Excellents.TrimExcess();
                            AddRelations();
                        }
                    }
                }

                return _Excellents;
            }
        }

        /// <summary>
        /// Sets the Previous and Next properties to all posts.
        /// </summary>
        private static void AddRelations()
        {
            for (int i = 0; i < _Excellents.Count; i++)
            {
                _Excellents[i]._Next = null;
                _Excellents[i]._Prev = null;
                if (i > 0)
                    _Excellents[i]._Next = _Excellents[i - 1];

                if (i < _Excellents.Count - 1)
                    _Excellents[i]._Prev = _Excellents[i + 1];
            }
        }

        /// <summary>
        /// Returns all Curriculas in the specified category
        /// </summary>
        public static List<Excellent> GetExcellentsByRes(Guid resId)
        {
            Res cat = Res.GetRes(resId);
            List<Excellent> col = _Excellents.FindAll(p => p.Ress.Contains(cat));
            col.Sort();
            col.TrimExcess();
            return col;
        }

        
        /// <summary>
        /// Returs a Curricula based on the specified id.
        /// </summary>
        public static Excellent GetExcellent(Guid id)
        {
            return Excellents.Find(p => p.Id == id);
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
            foreach (Excellent training in Excellents)
            {
                if (Utils.RemoveIllegalCharacters(training.Title).Equals(legal, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }


        /// <summary>
        /// Force reload of all posts
        /// </summary>
        public static void Reload()
        {
            _Excellents = TrainService.FillExcellents();
            _Excellents.Sort();
            AddRelations();
        }
        /// <summary>
        /// Adds a CurriculaInfo to the collection and saves the Curricula.
        /// </summary>
        /// <param name="crl">The comment to add to the post.</param>
        public void AddRes(Res crl)
        {
            //Res.Ress.Add(crl);
            Ress.Add(crl);
            //DataUpdate();
            //Ress.MarkOld();
        }
        /// <summary>
        /// Adds a CurriculaInfo to the collection and saves the Curricula.
        /// </summary>
        /// <param name="crl">The comment to add to the post.</param>
        public void RemoveRes(Res crl)
        {
            Ress.Remove(crl);
            //Res.Ress.Remove(crl);
            //DataUpdate();
            //Ress.MarkOld();
        }
        

        #endregion

        protected override void ValidationRules()
        {
            AddRule("Title", "Title must be set", string.IsNullOrEmpty(Title));
        }

        /// <summary>
        /// Retrieves the object from the data store and populates it.
        /// </summary>
        /// <param name="id">The unique identifier of the object.</param>
        /// <returns>
        /// True if the object exists and is being populated successfully
        /// </returns>
        protected override Excellent DataSelect(Guid id)
        {
            return TrainService.SelectExcellent(id);
        }

        /// <summary>
        /// Updates the object in its data store.
        /// </summary>
        protected override void DataUpdate()
        {
            if (IsChanged)
                TrainService.UpdateExcellent(this);
        }

        /// <summary>
        /// Inserts a new object to the data store.
        /// </summary>
        protected override void DataInsert()
        {
            TrainService.InsertExcellent(this);
                if (this.IsNew)
                {
                    Excellents.Add(this);
                    Excellents.Sort();
                    AddRelations();
                }
                
        }

        /// <summary>
        /// Deletes the object from the data store.
        /// </summary>
        protected override void DataDelete()
        {
            if (IsDeleted)
            {

                TrainService.DeleteExcellent(this);
            }
            if (Excellents.Contains(this))
                Excellents.Remove(this);

            Dispose();
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

                if (Ress.IsChanged)
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
            return CompleteTitle();
        }

        #region IComparable<Excellent> 成员

        public int CompareTo(Excellent other)
        {
            return this.CompleteTitle().CompareTo(other.CompleteTitle());
        }

        #endregion
    }
}
