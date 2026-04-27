namespace Histoire.Api.Dtos
{
    public class EventPersonsDto
    {
        required public string Title { get; set; }

        public List<PersonSummaryDto> PeopleInvolved { get; set; } = [];
    }
}
