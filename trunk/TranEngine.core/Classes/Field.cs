using System;
using System.Collections.Generic;
using System.Text;
using TrainEngine.Core.Providers;

namespace TrainEngine.Core.Classes
{
    public class Field : BusinessBase<Field, Guid>, IComparable<Field>
    {
        #region Constructor

		/// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
		/// </summary>
		public Field()
		{
			Id = Guid.NewGuid();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="description"></param>
        public Field(string name, string description)
		{
			this.Id = Guid.NewGuid();
            this._FieldName = name;
			this._Description = description;
		}

		#endregion

		#region Properties

		private string _FieldName;
		/// <summary>
		/// Gets or sets the Title or the object.
		/// </summary>
        public string FieldName
		{
            get { return _FieldName; }
			set
			{
                if (_FieldName != value) MarkChanged("Title");
                _FieldName = value;
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

		/// <summary>
        /// Get all Curriculas in this category.
		/// </summary>
        public List<Curricula> Curriculas
		{
            get { return Curricula.GetCurriculasByField(this.Id); }
		}

        /// <summary>
        /// Get all Curriculas in this category.
        /// </summary>
        public List<Training> Trainings
        {
            get { return Training.GetTrainingsByField(this.Id); }
        }

        /// <summary>
        /// Gets the relative link to the page displaying all posts for this Training.
        /// </summary>
        public string RelativeLink
        {
            get
            {
                return Utils.RelativeWebRoot + "Training/" + Utils.RemoveIllegalCharacters(this.FieldName) + TrainSettings.Instance.FileExtension;
            }
        }

        /// <summary>
        /// Gets the absolute link to the page displaying all posts for this category.
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
                return Utils.RelativeWebRoot + "Training/feed/" + Utils.RemoveIllegalCharacters(this.FieldName) + TrainSettings.Instance.FileExtension;
            }
        }

        /// <summary>
        /// Gets the absolute link to the feed for this category's posts.
        /// </summary>
        public Uri FeedAbsoluteLink
        {
            get { return Utils.ConvertToAbsolute(FeedRelativeLink); }
        }

		/// <summary>
		/// Gets the full title with Parent names included
		/// </summary>
		public string CompleteTitle()
		{

            return _FieldName;
			
		}

		/// <summary>
		/// Returns a category based on the specified id.
		/// </summary>
        public static Field GetField(Guid id)
		{
            foreach (Field field in Fields)
			{
				if (field.Id == id)
					return field;
			}

			return null;
		}

		private static object _SyncRoot = new object();
        private static List<Field> _Fields;
		/// <summary>
		/// Gets an unsorted list of all Categories.
		/// </summary>
        public static List<Field> Fields
		{
			get
			{
                if (_Fields == null)
				{
					lock (_SyncRoot)
					{
                        if (_Fields == null)
						{
                            _Fields = TrainService.FillFields();
                            _Fields.Sort();
						}
					}
				}

                return _Fields;
			}
		}

		#endregion

        #region Base overrides

        /// <summary>
        /// Reinforces the business rules by adding additional rules to the
        /// broken rules collection.
        /// </summary>
        protected override void ValidationRules()
        {
            AddRule("FieldName", "FieldName must be set", string.IsNullOrEmpty(FieldName));
        }

        /// <summary>
        /// Retrieves the object from the data store and populates it.
        /// </summary>
        /// <param name="id">The unique identifier of the object.</param>
        /// <returns>
        /// True if the object exists and is being populated successfully
        /// </returns>
        protected override Field DataSelect(Guid id)
        {
            return TrainService.SelectField(id);
        }

        /// <summary>
        /// Updates the object in its data store.
        /// </summary>
        protected override void DataUpdate()
        {
            if (IsChanged)
                TrainService.UpdateField(this);
        }

        /// <summary>
        /// Inserts a new object to the data store.
        /// </summary>
        protected override void DataInsert()
        {
            if (IsNew)
                TrainService.InsertField(this);
        }

        /// <summary>
        /// Deletes the object from the data store.
        /// </summary>
        protected override void DataDelete()
        {
            if (IsDeleted)
            {

                TrainService.DeleteField(this);
            }
            if (Fields.Contains(this))
                Fields.Remove(this);

            Dispose();
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

        #endregion

        #region IComparable<Category> Members

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. 
        /// The return value has the following meanings: Value Meaning Less than zero This object is 
        /// less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other.
        /// </returns>
        public int CompareTo(Field other)
        {
            return this.CompleteTitle().CompareTo(other.CompleteTitle());
        }

        #endregion

       
    }
}
