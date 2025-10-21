using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelsByCompanyId
{
    public class GetMaturityLevelsByCompanyIdQuery : IGetMaturityLevelsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMaturityLevelsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from maturityLevel in _databaseService.MaturityLevel
                                      where ((maturityLevel.isDeleted == null || maturityLevel.isDeleted == false) && maturityLevel.companyId == companyId)
                                      && (maturityLevel.name.ToUpper().Contains(search.ToUpper()))
                                      select new MaturityLevelEntity
                                      {
                                          maturityLevelId = maturityLevel.maturityLevelId,
                                          name = maturityLevel.name,
                                          description = maturityLevel.description,
                                          abbreviation = maturityLevel.abbreviation,
                                          value = maturityLevel.value,
                                          color = maturityLevel.color,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetMaturityLevelsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetMaturityLevelsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetMaturityLevelsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from maturityLevel in _databaseService.MaturityLevel
                               where ((maturityLevel.isDeleted == null || maturityLevel.isDeleted == false) && maturityLevel.companyId == companyId)
                               && (maturityLevel.name.ToUpper().Contains(search.ToUpper()))
                               select new MaturityLevelEntity
                               {
                                   maturityLevelId = maturityLevel.maturityLevelId,
                               }).CountAsync();
            return total;
        }

    }
}

