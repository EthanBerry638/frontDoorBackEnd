using System.ComponentModel.DataAnnotations;

namespace firstDoorBackEnd.Models
{
    public class SavedJob
    {
        public int Id { get; set; }
        [Required][MaxLength(100)] public string Title { get; set; } = string.Empty;
        [Required][MaxLength(50)] public string EmployerName { get; set; } = string.Empty;
        [Required][MaxLength(500)] public string Description { get; set; } = string.Empty;
        [Required][MaxLength(50)] public string Location { get; set; } = string.Empty;
        [Required][MaxLength(500)] public string Url { get; set; } = string.Empty;
        public Status Status { get; set; } = 0;
        public DateTime TimeSaved { get; set; } = DateTime.Now;
        public List<JobNote> Notes { get; set; } = new();
    }

    public enum Status
    {
        To_Apply,
        Applied,
        Interviewing,
        Rejected
    }
}
