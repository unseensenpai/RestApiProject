using DevExpress.Xpo;
using System;

namespace RestApiProject.DAL.DataObjects.User
{
    [Persistent("USER")]
    public class UserObject : BaseDataObject
    {
        public UserObject() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public UserObject(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        string username;
        [Persistent("NAME"), Size(250)]
        public string Username
        {
            get => username; 
            set => SetPropertyValue(nameof(Username), ref username, value);
        }
    }

}