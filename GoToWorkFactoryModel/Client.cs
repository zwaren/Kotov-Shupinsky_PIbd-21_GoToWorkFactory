using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace GoToWorkFactoryModel
{
    [DataContract]
    public class Client
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
		public string Name { get; set; }

        [DataMember]
        [Required]
		public string Email { get; set; }
	}
}
