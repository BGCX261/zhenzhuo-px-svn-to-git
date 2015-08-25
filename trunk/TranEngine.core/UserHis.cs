using System;
using System.Collections.Generic;
using System.Text;
using TrainEngine.Core.Providers;

namespace TrainEngine.Core 
{
    public class UserHis : BusinessBase<UserHis, string>
    {
        #region Constructors

		public UserHis()
		{

		}

        public UserHis(string username)
		{
			base.Id = username;
		}

		#endregion

        #region Properties
        private static object _SyncRoot = new object();
        private static List<UserHis> _UserHiss;
        /// <summary>
        /// Gets an unsorted list of all pages.
        /// </summary>
        public static List<UserHis> UserHiss
        {
            get
            {
                if (_UserHiss == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_UserHiss == null)
                        {
                            _UserHiss = TrainService.FillUserHiss();
                        }
                    }
                }

                return _UserHiss;
            }
        }

        public string UserName
        {
            get { return Id; }
        }

        private string _EventType;
        public string EventType
        {
            get { return _EventType; }
            set
            {
                if (value != _EventType) MarkChanged("EventType");
                _EventType = value;
            }
        }


        private string _EventValue;
        public string EventValue
        {
            get { return _EventValue; }
            set
            {
                if (value != _EventValue) MarkChanged("EventValue");
                _EventValue = value;
            }
        }

        private string _EventDes;
        public string EventDes
        {
            get { return _EventDes; }
            set
            {
                if (value != _EventDes) MarkChanged("EventDes");
                _EventDes = value;
            }
        }

        private DateTime _EventDate;
        public DateTime EventDate
        {
            get { return _EventDate; }
            set
            {
                if (value != _EventDate) MarkChanged("EventDate");
                _EventDate = value;
            }
        }
        #endregion

        #region Methods

        public static UserHis GetUserHis(string username)
        {
            return UserHiss.Find(delegate(UserHis u)
            {
                return u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase);
            });
        }
        #endregion

        #region base
        protected override void ValidationRules()
        {
            base.AddRule("Id", "Id must be set to the username of the user who the profile belongs to", string.IsNullOrEmpty(Id));
        }

        protected override UserHis DataSelect(string id)
        {
            return TrainService.SelectUserHis(id);
        }

        protected override void DataUpdate()
        {
            TrainService.UpdateUserHis(this);
        }

        protected override void DataInsert()
        {
            TrainService.InsertUserHis(this);

            if (IsNew)
                UserHiss.Add(this);
        }

        protected override void DataDelete()
        {
            TrainService.DeleteUserHis(this);
            if (UserHiss.Contains(this))
                UserHiss.Remove(this);
        }

        #endregion
    }
}
