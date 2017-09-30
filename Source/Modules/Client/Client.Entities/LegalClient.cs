using System.ComponentModel.DataAnnotations;

namespace Client.Entities
{
    public class LegalClient : Client
    {
        public LegalClient()
        {
            Type = ClientType.Legal;
        }

        [Required]
        [MaxLength(100)]
        public string Mol { get; set; }

        [Required]
        [MaxLength(100)]
        public string Eik { get; set; }

        [Required]
        public bool HasDds { get; set; }

        [MaxLength(100)]
        public string FirmName { get; set; }
    }
}
