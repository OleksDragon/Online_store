using System;

namespace Online_store
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public Category(int categoryId, string? name)
        {
            CategoryId = categoryId;
            Name = name;
        }

        public Category(int categoryId, string? name, string? description)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
        }
    }
}


