using Histoire.Api.Dtos;
using Histoire.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Histoire.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Récupère toutes les personnes avec les événements associés à chaque personne.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPerson")]
        public async Task<ActionResult<List<PersonDto>>> GetPerson()
        {
            // EfCore ne charge pas automatiquement les relations, il faut lui dire explicitement avec .Include
            var persons = await _context.Persons
                .Include(Person => Person.Events)
                .ToListAsync();

            // Transforme une liste de Person en une liste de PersonDto
            var personsDto = persons.Select(p => new PersonDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateofBirth = p.DateofBirth,
                DateofDeath = p.DateofDeath,
                Biography = p.Biography,
                PlaceofBirth = p.PlaceofBirth,
                PlaceofDeath = p.PlaceofDeath,
                ImageUrl = p.ImageUrl,
                Events = p.Events.Select(e => new EventSummaryDto
                {
                    Title = e.Title,
                    Description = e.Description,
                    Location = e.Location,
                    Type = e.Type,
                    DateStart = e.DateStart,
                    DateEnd = e.DateEnd,
                }).ToList()
            }).ToList();

            return personsDto;
        }

        /// <summary>
        /// Récupère une personne spécifique par son ID, incluant les événements associés à cette personne.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetPersonId(int id)
        {
            var person = await _context.Persons.Where(p => p.Id == id).Include(Person => Person.Events).FirstOrDefaultAsync();

            if (person == null) return NotFound();

            var personDto = new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateofBirth = person.DateofBirth,
                DateofDeath = person.DateofDeath,
                Biography = person.Biography,
                PlaceofBirth = person.PlaceofBirth,
                PlaceofDeath = person.PlaceofDeath,
                ImageUrl = person.ImageUrl,
                Events = person.Events.Select(e => new EventSummaryDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Location = e.Location,
                    Type = e.Type,
                    DateStart = e.DateStart,
                    DateEnd = e.DateEnd,
                }).ToList()
            };

            return personDto;
        }

        /// <summary>
        /// Récupère les événements associés à une personne spécifique par son ID, incluant les détails de chaque événement.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/events")]
        public async Task<ActionResult<PersonEventsDto>> GetPersonEvents(int id)
        {
            var personEvent = await _context.Persons.Where(p => p.Id == id).Include(Person => Person.Events).FirstOrDefaultAsync();

            if (personEvent == null) return NotFound();

            var PersonEventsDto = new PersonEventsDto
            {
                FirstName = personEvent.FirstName,
                LastName = personEvent.LastName,
                Events = personEvent.Events.Select(e => new EventSummaryDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Location = e.Location,
                    Type = e.Type,
                    DateStart = e.DateStart,
                    DateEnd = e.DateEnd,
                }).ToList()
            };

            return PersonEventsDto;
        }
    }
}
