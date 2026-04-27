namespace Histoire.Api.Dtos
{
    public class EventSummaryDto
    {
        public int Id { get; set; }
        required public string Title { get; set; }
        public string? Description { get; set; }
        required public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? Location { get; set; }
        required public string Type { get; set; }
    }
}
