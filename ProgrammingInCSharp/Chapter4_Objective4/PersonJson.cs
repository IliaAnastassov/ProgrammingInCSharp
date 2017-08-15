using System.Runtime.Serialization;

namespace Chapter4_Objective4
{
    [DataContract]
    class PersonJson
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }
}
