namespace firstDoorBackEnd.Repositories
{
    internal class ReedResponseDto
    {
        public List<ReedJobDto> results { get; set; } = new();
    }
    

    internal class ReedJobDto
    {
        public string jobTitle { get; set; } = string.Empty;

        public string employerName { get; set; } = string.Empty;

        public string locationName { get; set; } = string.Empty;

        public string jobDescription { get; set; } = string.Empty;

        public string jobUrl { get; set; } = string.Empty; 
    }
}