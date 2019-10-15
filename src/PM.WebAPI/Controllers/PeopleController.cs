﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM.Application.People;
using PM.Common.CommonModels;

namespace PM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleApplication _peopleApplication;

        public PeopleController(IPeopleApplication peopleApplication)
        {
            this._peopleApplication = peopleApplication;
        }

        // GET: api/People
        [HttpGet]
        public async Task<FilterResponse<IEnumerable<PeopleListItem>>> Get([FromQuery]string searchWord, 
                                                                        [FromQuery]int index = 0,
                                                                        [FromQuery]int nitems = 10,
                                                                        [FromQuery] string ordering = "ID")
        {
            var filter = new FilterModel<string>
            {
                Filter = searchWord,
                PageRequest =
                {
                    Index = index,
                    ShowPerPage = nitems,
                    SortingColumn = ordering
                }
            };
            
            return await _peopleApplication.Filter(filter);
        }

        // GET: api/People/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<PersonDetails> GetAsync(int id)
        {
            return await _peopleApplication.Get(id);
        }

        // POST: api/People
        [HttpPost]
        public async Task<Result<int>> Post([FromBody] CreatePersonCommand value)
        {
            return await _peopleApplication.CreatePerson(value);
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<Result> PutAsync(int id, [FromBody] UpdatePersonCommand value)
        {
            return await _peopleApplication.Update(id, value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _peopleApplication.Delete(id);
        }

        [HttpPost("connect/{id}/{targetID}")]
        public async Task Connect(int id, int targetID)
        {
            await _peopleApplication.MakeRelation(id, targetID, Domain.People.RelationTypes.COLLEAGUE);
        }
    }
}
