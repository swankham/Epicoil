namespace Epicoil.Library.Models
{
    public class SessionInfo
    {
        private string _UserId;
        private string _UserPassword;
        private string _UserName;
        private string _PlantID;
        private string _PlantName;
        private string _CompanyID;
        private string _CompanyName;
        private string _UserEmail;
        private string _SessionID;
        private string _Client;
        private string _AppServer;

        public string UserID
        {
            get
            {
                return this._UserId;
            }
            set
            {
                this._UserId = value;
            }
        }

        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        public string UserPassword
        {
            get
            {
                return this._UserPassword;
            }
            set
            {
                this._UserPassword = value;
            }
        }

        public string PlantID
        {
            get
            {
                return this._PlantID;
            }
            set
            {
                this._PlantID = value;
            }
        }

        public string PlantName
        {
            get
            {
                return this._PlantName;
            }
            set
            {
                this._PlantName = value;
            }
        }

        public string CompanyID
        {
            get
            {
                return this._CompanyID;
            }
            set
            {
                this._CompanyID = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return this._CompanyName;
            }
            set
            {
                this._CompanyName = value;
            }
        }

        public string UserEmail
        {
            get
            {
                return this._UserEmail;
            }
            set
            {
                this._UserEmail = value;
            }
        }

        public string SessionID
        {
            get
            {
                return this._SessionID;
            }
            set
            {
                this._SessionID = value;
            }
        }

        public string Client
        {
            get
            {
                return this._Client;
            }
            set
            {
                this._Client = value;
            }
        }

        public string AppServer
        {
            get
            {
                return this._AppServer;
            }
            set
            {
                this._AppServer = value;
            }
        }
    }
}