using Histoire.Api.Dtos;
using Histoire.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Histoire.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère tous les événements avec les personnes impliquées dans chaque événement.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetEvent")]
        public async Task<ActionResult<List<EventDto>>> GetEvent()
        {
            // EfCore ne charge pas automatiquement les relations, il faut lui dire explicitement avec .Include
            var events = await _context.Events
                .Include(Event => Event.PeopleInvolved)
                .ToListAsync();

            // Transforme une liste d'Event en une liste d'EventDto
            var eventsDto = events.Select(e => new EventDto
            {
                Title = e.Title,
                Description = e.Description,
                Location = e.Location,
                Type = e.Type,
                DateStart = e.DateStart,
                DateEnd = e.DateEnd,
                PeopleInvolved = e.PeopleInvolved.Select(p => new PersonSummaryDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Biography = p.Biography,
                    DateofBirth = p.DateofBirth,
                    DateofDeath = p.DateofDeath,
                    PlaceofBirth = p.PlaceofBirth,
                    PlaceofDeath = p.PlaceofDeath,
                    ImageUrl = p.ImageUrl
                }).ToList()
            }).ToList();

            return eventsDto;
        }

        /// <summary>
        /// Récupère un événement spécifique par son ID, incluant les personnes impliquées dans cet événement.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEventId(int id)
        {
            var events = await _context.Events.Where(e => e.Id == id).Include(Event => Event.PeopleInvolved).FirstOrDefaultAsync();

            if (events == null) return NotFound();

            var eventDto = new EventDto
            {
                Id = events.Id,
                Title = events.Title,
                Description = events.Description,
                Location = events.Location,
                Type = events.Type,
                DateStart = events.DateStart,
                DateEnd = events.DateEnd,
                PeopleInvolved = events.PeopleInvolved.Select(p => new PersonSummaryDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Biography = p.Biography,
                    DateofBirth = p.DateofBirth,
                    DateofDeath = p.DateofDeath,
                    PlaceofBirth = p.PlaceofBirth,
                    PlaceofDeath = p.PlaceofDeath,
                    ImageUrl = p.ImageUrl
                }).ToList()
            };

            return eventDto;
        }

        /// <summary>
        /// Récupère les personnes impliquées dans un événement spécifique par son ID, incluant le titre de l'événement.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/events")]
        public async Task<ActionResult<EventPersonsDto>> GetEventPersons(int id)
        {
            var EventPerson = await _context.Events.Where(e => e.Id == id).Include(Event => Event.PeopleInvolved).FirstOrDefaultAsync();

            if (EventPerson == null) return NotFound();

            var EventPersonsDto = new EventPersonsDto
            {
                Title = EventPerson.Title,
                PeopleInvolved = EventPerson.PeopleInvolved.Select(p => new PersonSummaryDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Biography = p.Biography,
                    DateofBirth = p.DateofBirth,
                    DateofDeath = p.DateofDeath,
                    PlaceofBirth = p.PlaceofBirth,
                    PlaceofDeath = p.PlaceofDeath,
                    ImageUrl = p.ImageUrl
                }).ToList()
            };

            return EventPersonsDto;
        }
    }
}



// TODO : ajouter un système d'header 
