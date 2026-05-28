namespace firstDoorBackEnd.Models
{
    public record CareerJetResponse(
        string type, 
        int hits, 
        string message,
        int pages, 
        List<Job>? jobs)
    {}
}
