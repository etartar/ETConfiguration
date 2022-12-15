namespace ETConfiguration.Core.Database.Entities
{
    public class Configuration
    {
        private Configuration()
        {
            Id = Guid.NewGuid();
        }

        private Configuration(string section, string key, string value) : this()
        {
            Section = section;
            Key = key;
            Value = value;
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string Section { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ModifiedDate { get; private set; }

        public static Configuration Create(string section, string key, string value)
        {
            return new Configuration(section, key, value);
        }

        public Configuration Update(string section, string key, string value)
        {
            Section = section;
            Key = key; 
            Value = value;
            ModifiedDate = DateTime.Now;
            return this;
        }
    }
}
