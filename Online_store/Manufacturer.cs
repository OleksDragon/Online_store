using System;

namespace Online_store
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; } = string.Empty;

        public Manufacturer(int manufacturerId, string name)
        {
            ManufacturerId = manufacturerId;
            Name = name;
        }
    }
}

