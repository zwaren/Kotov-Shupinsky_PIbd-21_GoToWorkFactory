using System.Runtime.Serialization;

namespace GoToWorkFactoryServiceDAL.BindingModels
{
    [DataContract]
    public class MaterialBindingModel
	{
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Count { get; set; }
	}
}
