using Administration.Entities;

namespace Administration.Bll.Dtos
{
    public class ConfigOptionDto
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string Entity { get; set; }
        public string Value { get; set; }
        public string DisplayValue { get; set; }
        public ConfigOptionTypeEnum Type { get; set; }
        public ConfigOptionCategoryEnum Category { get; set; }
    }
}
