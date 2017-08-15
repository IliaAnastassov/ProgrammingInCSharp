using System.Runtime.Serialization;

namespace Chapter4_Objective4
{
    [DataContract]
    class PersonDataContract
    {
        private bool isDirty;

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int Age { get; set; }
    }
}
