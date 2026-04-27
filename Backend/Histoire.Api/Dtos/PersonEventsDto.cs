namespace Histoire.Api.Dtos
{
    public class PersonEventsDto
    {
        required public string FirstName { get; set; }
        public string? LastName { get; set; }

        public List<EventSummaryDto> Events { get; set; } = [];
    }
}
