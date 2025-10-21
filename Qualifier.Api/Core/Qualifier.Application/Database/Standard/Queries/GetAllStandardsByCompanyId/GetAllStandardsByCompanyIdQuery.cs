using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Standard.Queries.GetAllStandardsByCompanyId
{
    internal class GetAllStandardsByCompanyIdQuery : IGetAllStandardsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllStandardsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from item in _databaseService.Standard
                                      where ((item.isDeleted == null || item.isDeleted == false) && item.companyId == companyId)
                                      select new StandardEntity
                                      {
                                          standardId = item.standardId,
                                          name = item.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllStandardsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllStandardsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllStandardsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

