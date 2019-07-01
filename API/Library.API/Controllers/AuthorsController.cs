using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.API.Helpers;
using AutoMapper;
using Library.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private ILibraryRepository _libraryRepository;
        ILogger<AuthorsController> _logger;
        IUrlHelper _urlHelper;
        IPropertyMappingService _propertyService;
        ITypeHelperService _typeHelperService;


        public AuthorsController(
            ILibraryRepository repository,
            ILogger<AuthorsController> logger,
            IUrlHelper urlHelper,
            IPropertyMappingService propertyMappingService,
            ITypeHelperService typeHelperService
            )
        {
            _libraryRepository = repository;
            _logger = logger;
            _urlHelper = urlHelper;
            _propertyService = propertyMappingService;
            _typeHelperService = typeHelperService;
        }
        private string CreateAuthorsResourceUri(AuthorsResourceParameters authorsResourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetAuthors",
                        new
                        {
                            fields = authorsResourceParameters.Fields,
                            orderBy = authorsResourceParameters.OrderBy,
                            genre = authorsResourceParameters.Genre,
                            searchQuery = authorsResourceParameters.SearchQuery,
                            pageNumber = authorsResourceParameters.PageNumber - 1,
                            pageSize = authorsResourceParameters.PageSize
                        }
                    );
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetAuthors",
                       new
                       {
                           fields = authorsResourceParameters.Fields,
                           orderBy = authorsResourceParameters.OrderBy,
                           genre = authorsResourceParameters.Genre,
                           searchQuery = authorsResourceParameters.SearchQuery,
                           pageNumber = authorsResourceParameters.PageNumber + 1,
                           pageSize = authorsResourceParameters.PageSize
                       }
                   );
                default:
                    return _urlHelper.Link("GetAuthors",
                        new
                        {
                            fields = authorsResourceParameters.Fields,
                            orderBy = authorsResourceParameters.OrderBy,
                            genre = authorsResourceParameters.Genre,
                            searchQuery = authorsResourceParameters.SearchQuery,
                            pageNumber = authorsResourceParameters.PageNumber,
                            pageSize = authorsResourceParameters.PageSize
                        }
                    );
            }
        }

        [HttpGet(Name = "GetAuthors")]
        [HttpHead]

        public IActionResult GetAuthors(AuthorsResourceParameters authorResourceParameters, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!_typeHelperService.TypeHasProperties<AuthorDto>(authorResourceParameters.Fields))
            {
                return BadRequest();
            }
            if (!_propertyService.ValidMappingExistsFor<AuthorDto, Author>(authorResourceParameters.OrderBy))
            {
                return BadRequest();
            }

            var authorsFromRepo = _libraryRepository.GetAuthors(authorResourceParameters);

            //generating prev and next link
            var previousLink = authorsFromRepo.HasPrevious ? CreateAuthorsResourceUri(authorResourceParameters, ResourceUriType.PreviousPage) : null;
            var nextLink = authorsFromRepo.HasNext ? CreateAuthorsResourceUri(authorResourceParameters, ResourceUriType.NextPage) : null;



            var authors = Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(authorsFromRepo);
            if (mediaType == "application/vnd.marvin.hateoas+json")
            {
                var paginationMetadata = new
                {
                    totalCount = authorsFromRepo.TotalCount,
                    pageSize = authorsFromRepo.PageSize,
                    currentPaga = authorsFromRepo.CurrentPage,
                    totalPages = authorsFromRepo.TotalPages,
                    previousPageLink = previousLink,
                    nextPageLink = nextLink
                };
                Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
                var links = CreateLinksForAuthors(authorResourceParameters, authorsFromRepo.HasNext, authorsFromRepo.HasPrevious);
                var shapedAuthors = authors.ShapeData(authorResourceParameters.Fields);
                var shapedAuhorsWithLinks = shapedAuthors.Select(author =>
                {
                    var authorAsDictionary = author as IDictionary<string, object>;
                    var authorLink = CreateLinksForAuthor((Guid)authorAsDictionary["Id"], authorResourceParameters.Fields);
                    authorAsDictionary.Add("links", authorLink);
                    return authorAsDictionary;
                });

                var linkedCollectionResource = new
                {
                    value = shapedAuhorsWithLinks,
                    links = links
                };
                return Ok(linkedCollectionResource);
            }
            else
            {
                var paginationMetadata = new
                {
                    totalCount = authorsFromRepo.TotalCount,
                    pageSize = authorsFromRepo.PageSize,
                    currentPaga = authorsFromRepo.CurrentPage,
                    totalPages = authorsFromRepo.TotalPages,
                    previousPageLink = previousLink,
                    nextPageLink = nextLink
                };
                Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
                return Ok(authors.ShapeData(authorResourceParameters.Fields));

            }


        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid id, [FromQuery] string fields)
        {

            if (!_typeHelperService.TypeHasProperties<AuthorDto>(fields))
            {
                return BadRequest();
            }
            var author = Mapper.Map<Author, AuthorDto>(_libraryRepository.GetAuthor(id));
            if (author == null)
            {
                return NotFound();
            }
            var links = CreateLinksForAuthor(id, fields);
            var linkedResourceToReturn = author.ShapeData(fields) as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);

            return Ok(linkedResourceToReturn);

        }
        [HttpPost(Name = "CreateAuthor")]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] { "application/vnd.marvin.author.full+json",
        "application/vnd.marvin.author.full+xml" })]
        [RequestHeaderMatchesMediaType("Accept",
            new[] { "..." })]

        public IActionResult AddAuthor([FromBody] AuthorForCreationDto author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authorToEntity = Mapper.Map<AuthorForCreationDto, Author>(author);
            _libraryRepository.AddAuthor(authorToEntity);
            if (!_libraryRepository.Save())
            {
                throw new Exception("Creating an author failed on save.");
                // return StatusCode(500, "An unexpected fault happened. Try again later.");
            }
            var authorToReturn = Mapper.Map<AuthorDto>(authorToEntity);

            var links = CreateLinksForAuthor(authorToReturn.Id, null);
            var linkedResourceToReturn = authorToReturn.ShapeData(null) as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);


            return CreatedAtRoute("GetAuthor", new { id = linkedResourceToReturn["Id"] }, linkedResourceToReturn);
        }
        [HttpPost(Name = "CreateAuthorWithDateOfDeath")]
        [RequestHeaderMatchesMediaType("Content-type",
            new[] { "application/vnd.marvin.authorwithdateofdeath.full+json",
        "application/vnd.marvin.authorwithdateofdeath.full+xml"})]
        public IActionResult CreateAuthorWithDateOfDeath([FromBody] AuthorForCreationWithDateOfDeathDto author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authorToEntity = Mapper.Map< Author>(author);
            _libraryRepository.AddAuthor(authorToEntity);
            if (!_libraryRepository.Save())
            {
                throw new Exception("Creating an author failed on save.");
                // return StatusCode(500, "An unexpected fault happened. Try again later.");
            }
            var authorToReturn = Mapper.Map<AuthorDto>(authorToEntity);

            var links = CreateLinksForAuthor(authorToReturn.Id, null);
            var linkedResourceToReturn = authorToReturn.ShapeData(null) as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);


            return CreatedAtRoute("GetAuthor", new { id = linkedResourceToReturn["Id"] }, linkedResourceToReturn);
        }
        [HttpPost("{id}")]
        public IActionResult BlockAuthorCreation(Guid id)
        {
            if (_libraryRepository.AuthorExists(id))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }
        [HttpDelete("{id}", Name = "DeleteAuthor")]
        public IActionResult DeleteAuthor(Guid id)
        {
            var authorToDelete = _libraryRepository.GetAuthor(id);
            if (authorToDelete == null)
            {
                return NotFound();
            }
            _libraryRepository.DeleteAuthor(authorToDelete);
            if (!_libraryRepository.Save())
            {
                throw new Exception($"Deleting an author id {id} failed on save.");

            }
            _logger.LogInformation(100, $"Author with {id} was deleted");

            return NoContent();
        }

        private IEnumerable<LinkDto> CreateLinksForAuthor(Guid id, string fields)
        {
            var links = new List<LinkDto>();
            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDto(_urlHelper.Link("GetAuthor", new { id = id }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDto(_urlHelper.Link("GetAuthor", new { id = id, fields = fields }),
                  "self",
                  "GET"));
            }

            links.Add(
              new LinkDto(_urlHelper.Link("DeleteAuthor", new { id = id }),
              "delete_author",
              "DELETE"));

            links.Add(
              new LinkDto(_urlHelper.Link("CreateBookForAuthor", new { authorId = id }),
              "create_book_for_author",
              "POST"));

            links.Add(
               new LinkDto(_urlHelper.Link("GetBooksForAuthor", new { authorId = id }),
               "books",
               "GET"));
            return links;
        }
        private IEnumerable<LinkDto> CreateLinksForAuthors(
           AuthorsResourceParameters authorsResourceParameters,
           bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateAuthorsResourceUri(authorsResourceParameters,
               ResourceUriType.Current)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateAuthorsResourceUri(authorsResourceParameters,
                  ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateAuthorsResourceUri(authorsResourceParameters,
                    ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }

        [HttpOptions]
        public IActionResult GetAuthorsOption() {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
       
    }
}