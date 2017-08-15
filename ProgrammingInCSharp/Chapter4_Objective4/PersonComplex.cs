using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Chapter4_Objective4
{
    [Serializable]
    class PersonComplex : ISerializable
    {
        private bool isDirty;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public PersonComplex() { }

        protected PersonComplex(SerializationInfo info, StreamingContext context)
        {
            FirstName = info.GetString("firstname");
            LastName = info.GetString("lastname");
            Age = info.GetInt32("age");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("firstname", FirstName);
            info.AddValue("lastname", LastName);
            info.AddValue("age", Age);
        }
    }
}
