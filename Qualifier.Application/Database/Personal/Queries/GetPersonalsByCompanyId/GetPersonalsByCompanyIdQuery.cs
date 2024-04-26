using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Personal.Queries.GetPersonalsByCompanyId
{
    public class GetPersonalsByCompanyIdQuery : IGetPersonalsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetPersonalsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from personal in _databaseService.Personal
                                      where ((personal.isDeleted == null || personal.isDeleted == false) && personal.companyId == companyId)
                                      && (personal.name.ToUpper().Contains(search.ToUpper()))
                                      select new PersonalEntity
                                      {
                                          personalId = personal.personalId,
                                          name = personal.name,
                                          firstName = personal.firstName,
                                          lastName = personal.lastName,
                                          email = personal.email,
                                          position = personal.position,
                                          image = personal.image,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetPersonalsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetPersonalsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetPersonalsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from personal in _databaseService.Personal
                               where ((personal.isDeleted == null || personal.isDeleted == false) && personal.companyId == companyId)
                               && (personal.name.ToUpper().Contains(search.ToUpper()))
                               select new PersonalEntity
                               {
                                   personalId = personal.personalId,
                               }).CountAsync();
            return total;
        }

    }
}

