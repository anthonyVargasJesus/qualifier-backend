using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Personal.Queries.GetAllPersonalsByCompanyId
{
    internal class GetAllPersonalsByCompanyIdQuery : IGetAllPersonalsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllPersonalsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from personal in _databaseService.Personal
                                      where ((personal.isDeleted == null || personal.isDeleted == false) && personal.companyId == companyId)
                                      select new PersonalEntity
                                      {
                                          personalId = personal.personalId,
                                          name = personal.name,
                                          firstName = personal.firstName,
                                          lastName = personal.lastName,
                                      }).ToListAsync();

                BaseResponseDto<GetAllPersonalsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllPersonalsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllPersonalsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

