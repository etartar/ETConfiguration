namespace ETConfiguration.Database.Sample.Dtos
{
    public class UpdateConfigurationDto
    {
        public Guid Id { get; set; }
        public string Section { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
