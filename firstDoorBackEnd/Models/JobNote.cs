using System.ComponentModel.DataAnnotations;

namespace firstDoorBackEnd.Models
{
    public class JobNote
    {
        public int Id { get; set; }
        [MaxLength(500)]public string? Body { get; set; }
        public DateTime DateSaved { get; set; } = DateTime.Now;
        public int SavedJobId { get; set; }
        public SavedJob SavedJob { get; set; } = new SavedJob();
    }
}
