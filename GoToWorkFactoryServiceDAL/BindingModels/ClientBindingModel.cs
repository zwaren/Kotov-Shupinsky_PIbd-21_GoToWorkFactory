using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.BindingModels
{
    [DataContract]
    public class ClientBindingModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }
	}
}
