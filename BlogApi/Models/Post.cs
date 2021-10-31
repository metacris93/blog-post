using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApi.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created_at { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Updated_at { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Image { get; set; }

        public override string ToString()
        {
            var str = $"Id: {Id}" +
                $" Title:{Title}" +
                $" Created_at: {Created_at}" +
                $" Updated_at: {Updated_at}" +
                $" Content: {Content}" +
                $" Image File Name: {Image}";
            return str;
        }
    }
}
