using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace GoToWorkFactoryModel
{
    [DataContract]
    public class Material
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
		public string Name { get; set; }

        [DataMember]
        public int Count { get; set; }

        [ForeignKey("MaterialId")]
		public virtual List<ProductMaterial> ProductMaterials { get; set; }

        [ForeignKey("MaterialId")]
        public virtual List<Request> Requests { get; set; }
    }
}
