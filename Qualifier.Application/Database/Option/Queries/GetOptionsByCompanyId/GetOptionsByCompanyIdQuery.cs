using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Option.Queries.GetOptionsByCompanyId
{
    public class GetOptionsByCompanyIdQuery : IGetOptionsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetOptionsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from option in _databaseService.Option
                                      where ((option.isDeleted == null || option.isDeleted == false) && option.companyId == companyId)
                                      && (option.name.ToUpper().Contains(search.ToUpper()))
                                      select new OptionEntity
                                      {
                                          optionId = option.optionId,
                                          name = option.name,
                                          image = option.image,
                                          url = option.url,
                                          isMobile = option.isMobile,
                                          companyId = option.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetOptionsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetOptionsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetOptionsByCompanyIdDto>>(entities);
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
            var total = await (from option in _databaseService.Option
                               where ((option.isDeleted == null || option.isDeleted == false) && option.companyId == companyId)
                               && (option.name.ToUpper().Contains(search.ToUpper()))
                               select new OptionEntity
                               {
                                   optionId = option.optionId,
                               }).CountAsync();
            return total;
        }

    }
}

