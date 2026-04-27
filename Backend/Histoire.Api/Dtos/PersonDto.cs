namespace Histoire.Api.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        required public DateOnly DateofBirth { get; set; }

        public DateOnly? DateofDeath { get; set; }

        public string? PlaceofBirth { get; set; }

        public string? PlaceofDeath { get; set; }

        public string? Biography { get; set; }

        public string? ImageUrl { get; set; }

        public List<EventSummaryDto> Events { get; set; } = [];
    }
}
