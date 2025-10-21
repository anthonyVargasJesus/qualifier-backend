using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Standard.Queries.GetStandardsByCompanyId
{
    public class GetStandardsByCompanyIdQuery : IGetStandardsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetStandardsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from standard in _databaseService.Standard
                                      where ((standard.isDeleted == null || standard.isDeleted == false) && standard.companyId == companyId)
                                      && (standard.name.ToUpper().Contains(search.ToUpper()))
                                      select new StandardEntity
                                      {
                                          standardId = standard.standardId,
                                          name = standard.name,
                                          description = standard.description,
                                      })
                                        .Skip(skip).Take(pageSize)
                                        .ToListAsync();

                BaseResponseDto<GetStandardsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetStandardsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetStandardsByCompanyIdDto>>(entities);
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
            var total = await (from standard in _databaseService.Standard
                               where ((standard.isDeleted == null || standard.isDeleted == false) && standard.companyId == companyId)
                               && (standard.name.ToUpper().Contains(search.ToUpper()))
                               select new StandardEntity
                               {
                                   standardId = standard.standardId,
                               }).CountAsync();
            return total;
        }

    }
}

