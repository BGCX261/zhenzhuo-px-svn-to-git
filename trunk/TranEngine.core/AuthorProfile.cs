#region Using

using System;
using System.Collections.Generic;
using TrainEngine.Core.Providers;
using System.Web.Security;

#endregion

namespace TrainEngine.Core
{
	public class AuthorProfile : BusinessBase<AuthorProfile, string>
	{

		#region Constructors

		public AuthorProfile()
		{

		}

		public AuthorProfile(string username)
		{
			base.Id = username;
            _Points = "0";
            _Scores = "0";
            _ViewCount = 0;
		}

		#endregion

		#region Properties

		private static object _SyncRoot = new object();
		private static List<AuthorProfile> _Profiles;
		/// <summary>
		/// Gets an unsorted list of all pages.
		/// </summary>
		public static List<AuthorProfile> Profiles
		{
			get
			{
				if (_Profiles == null)
				{
					lock (_SyncRoot)
					{
						if (_Profiles == null)
						{
							_Profiles = TrainService.FillProfiles();
						}
					}
				}

				return _Profiles;
			}
		}

		public string UserName
		{
			get { return Id; }
		}
        
        public bool IsTeacher
        {
            get
            {
                return Roles.IsUserInRole(this.UserName, "Teachers");
            }
        }

        public bool IsOrgan
        {
            get
            {
                return Roles.IsUserInRole(this.UserName, "Organs");
            }
        }

        public bool IsAdmin
        {
            get
            {
                return Roles.IsUserInRole(this.UserName, "Administrators");
            }
        }
        public bool IsStudent
        {
            get
            {
                return Roles.IsUserInRole(this.UserName, "Students");
            }
        }
		private bool _IsPrivate;
		public bool IsPrivate
		{
			get { return _IsPrivate; }
			set
			{
				if (value != _IsPrivate) MarkChanged("IsPrivate");
				_IsPrivate = value;
			}
		}
        private bool _isVip;
        public bool IsVip
        {
            get { return _isVip; }
            set
            {
                if (value != _isVip) MarkChanged("IsVip");
                _isVip = value;
            }
        }
        private bool _IsGoldTch;
         public bool IsGoldTch
        {
            get { return _IsGoldTch; }
            set
            {
                if (value != _IsGoldTch) MarkChanged("IsGoldTch");
                _IsGoldTch = value;
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

		private string _DisplayName;
		public string DisplayName
		{
			get { return _DisplayName; }
			set
			{
				if (value != _DisplayName) MarkChanged("DisplayName");
				_DisplayName = value;
			}
		}

		private string _PhotoUrl;
		public string PhotoURL
		{
			get { return _PhotoUrl; }
			set
			{
				if (value != _PhotoUrl) MarkChanged("PhotoURL");
				_PhotoUrl = value;
			}
		}

		private string _CityTown;
		public string CityTown
		{
			get { return _CityTown; }
			set
			{
				if (value != _CityTown) MarkChanged("CityTown");
				_CityTown = value;
			}
		}

		private string _RegionState;
		public string RegionState
		{
			get { return _RegionState; }
			set
			{
				if (value != _RegionState) MarkChanged("RegionState");
				_RegionState = value;
			}
		}

		private string _PhoneMain;
		public string PhoneMain
		{
			get { return _PhoneMain; }
			set
			{
				if (value != _PhoneMain) MarkChanged("PhoneMain");
				_PhoneMain = value;
			}
		}

		private string _PhoneFax;
		public string PhoneFax
		{
			get { return _PhoneFax; }
			set
			{
				if (value != _PhoneFax) MarkChanged("PhoneFax");
				_PhoneFax = value;
			}
		}

		private string _PhoneMobile;
		public string PhoneMobile
		{
			get { return _PhoneMobile; }
			set
			{
				if (value != _PhoneMobile) MarkChanged("PhoneMobile");
				_PhoneMobile = value;
			}
		}

        private string _Pay;
        public string Pay
        {
            get { return _Pay; }
            set
            {
                if (value != _Pay) MarkChanged("Pay");
                _Pay = value;
            }
        }

        private string _fields;
        public string Fields
        {
            get { return _fields; }
            set
            {
                if (value != _fields) MarkChanged("Fields");
                _fields = value;
            }
        }

		private string _Company;
		public string Company
		{
			get { return _Company; }
			set
			{
				if (value != _Company) MarkChanged("Company");
				_Company = value;
			}
		}

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _MSN_QQ;
        public string MSN_QQ
        {
            get { return _MSN_QQ; }
            set
            {
                if (value != _MSN_QQ) MarkChanged("AboutMe");
                _MSN_QQ = value;
            }
        }

		private string _AboutMe;
		public string AboutMe
		{
			get { return _AboutMe; }
			set
			{
				if (value != _AboutMe) MarkChanged("AboutMe");
				_AboutMe = value;
			}
		}

        private string _Description1;
        public string Description1
        {
            get { return _Description1; }
            set
            {
                if (value != _Description1) MarkChanged("Description1");
                _Description1 = value;
            }
        }

        private string _Description2;
        public string Description2
        {
            get { return _Description2; }
            set
            {
                if (value != _Description2) MarkChanged("Description2");
                _Description2 = value;
            }
        }

        private string _WebSit;
        public string WebSit
        {
            get { return _WebSit; }
            set
            {
                if (value != _WebSit) MarkChanged("WebSit");
                _WebSit = value;
            }
        }
		public string RelativeLink
		{
			get { return Utils.RelativeWebRoot + "author/" + Id + ".aspx"; ; }
		}

        private string _Points;
        /// <summary>
        /// Gets or sets the Points of the Curricula.
        /// </summary>
        public string Points
        {
            get { return _Points; }
            set
            {
                if (_Points != value) MarkChanged("Points");
                _Points = value;
            }
        }

        private string _Scores;
        /// <summary>
        /// Gets or sets the Scores of the Curricula.
        /// </summary>
        public string Scores
        {
            get { return _Scores; }
            set
            {
                if (_Scores != value) MarkChanged("Scores");
                _Scores = value;
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
		#endregion

		#region Methods

		public static AuthorProfile GetProfile(string username)
		{
			return Profiles.Find(delegate(AuthorProfile p)
			{
				return p.UserName.Equals(username, StringComparison.OrdinalIgnoreCase);
			});
		}

		public override string ToString()
		{
			return DisplayName;
		}

		#endregion

		#region BusinessBaes overrides

		protected override void ValidationRules()
		{
			base.AddRule("Id", "Id must be set to the username of the user who the profile belongs to", string.IsNullOrEmpty(Id));
		}

		protected override AuthorProfile DataSelect(string id)
		{
			return TrainService.SelectProfile(id);
		}

		protected override void DataUpdate()
		{
			TrainService.UpdateProfile(this);
		}

		protected override void DataInsert()
		{
			TrainService.InsertProfile(this);

			if (IsNew)
				Profiles.Add(this);
		}

		protected override void DataDelete()
		{
			TrainService.DeleteProfile(this);
			if (Profiles.Contains(this))
				Profiles.Remove(this);
		}
        public void UpdateViewCount()
        {
            TrainService.UpdateViewCount(this);
        }
		#endregion

	}
}