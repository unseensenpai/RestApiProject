using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace RestApiProject.DAL.DataObjects
{
    [NonPersistent]
    public abstract class BaseDataObject : XPLiteObject
    {
        protected BaseDataObject() { }
        protected BaseDataObject(Session session) : base(session) { }
        protected BaseDataObject(Session session, XPClassInfo classInfo) : base(session, classInfo) { }

        [Key(true), Persistent("ID")]
        public virtual int Id { get; set; }
    }
}
