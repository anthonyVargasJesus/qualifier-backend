using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Personal.Queries.GetPersonalById
{
    public class GetPersonalByIdQuery : IGetPersonalByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetPersonalByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int personalId)
        {
            try
            {
                var entity = await (from item in _databaseService.Personal
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.personalId == personalId)
                                    select new PersonalEntity()
                                    {
                                        personalId = item.personalId,
                                        name = item.name,
                                        middleName = item.middleName,
                                        firstName = item.firstName,
                                        lastName = item.lastName,
                                        email = item.email,
                                        phone = item.phone,
                                        position = item.position,
                                        image = item.image,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetPersonalByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

