using System;
using System.Runtime.Serialization;

namespace GoToWorkFactoryModel
{
    [DataContract]
    public class Request
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public int MaterialId { get; set; }

        [DataMember]
        public DateTime ImplementDate { get; set; }

        [DataMember]
        public virtual Material Material { get; set; }
    }
}
