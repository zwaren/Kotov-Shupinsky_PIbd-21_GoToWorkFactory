using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class MaterialViewModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Count { get; set; }
	}
}
