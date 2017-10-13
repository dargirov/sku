using System.ComponentModel.DataAnnotations;

namespace Client.Entities
{
    public class NaturalClient : Client
    {
        public NaturalClient()
        {
            Type = ClientTypeEnum.Natural;
        }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string PersonalNo { get; set; }
    }
}
