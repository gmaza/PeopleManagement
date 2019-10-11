using PM.Common.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM.Application.People
{
    public interface IPeopleApplication
    {
        Task<Result<int>> CreatePerson(CreatePersonCommand command);
    }
}
