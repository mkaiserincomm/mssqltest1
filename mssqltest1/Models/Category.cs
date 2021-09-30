using System.ComponentModel.DataAnnotations.Schema;

namespace mssqltest1.Models
{
    [Table("Categories")]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId {get; set;}
        public string categoryName {get; set;}
        public string description {get; set;}
        public byte[] picture { get; set;} 
    }
}