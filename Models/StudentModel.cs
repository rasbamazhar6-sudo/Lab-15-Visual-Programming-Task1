using System.ComponentModel.DataAnnotations;
namespace Students_CRUD_WebAPI.Models
{
    public class StudentModel
    {
        [Key]
        public int ID {  get; set; }
        [Required]

        public string? Name { get; set; }    

        [Required]
        public string? Address { get; set; }
    }
}
