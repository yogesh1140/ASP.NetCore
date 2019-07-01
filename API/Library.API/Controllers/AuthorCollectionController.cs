﻿using AutoMapper;
using Library.API.Entities;
using Library.API.Helpers;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/authorcollections")]
    public class AuthorCollectionController : Controller
    {
        private ILibraryRepository _libraryRepository { get; set; }
        public AuthorCollectionController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpPost()]
        public IActionResult CreateAuthorCollections([FromBody] IEnumerable<AuthorForCreationDto> authorCollection)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            var authorEntities = Mapper.Map<IEnumerable<Author>>(authorCollection);
            foreach (var author in authorEntities)
            {
                _libraryRepository.AddAuthor(author);

            }
            if (!_libraryRepository.Save())
            {
                throw new Exception($"Creating author collection failed on save.");
            }
            var authorCollectionToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            var idsAsString = string.Join(",", authorCollectionToReturn.Select(ac => ac.Id));
            return CreatedAtRoute("GetAuthorCollection", new { ids = idsAsString }, authorCollectionToReturn);
        }


        [HttpGet("{ids}", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]   IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }
            var authorEntities = _libraryRepository.GetAuthors(ids);
            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();

            }
            var authorsToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            return Ok(authorsToReturn);

        }
       
    }
}
