using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.ViewModels
{
    [DataContract]
    public class ClientViewModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }
	}
}
