using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Macroprocess.Queries.GetMacroprocesssByCompanyId
{
    public class GetMacroprocesssByCompanyIdQuery : IGetMacroprocesssByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMacroprocesssByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from macroprocess in _databaseService.Macroprocess
                                      where ((macroprocess.isDeleted == null || macroprocess.isDeleted == false) && macroprocess.companyId == companyId)
                                      && (macroprocess.name.ToUpper().Contains(search.ToUpper()))
                                      select new MacroprocessEntity
                                      {
                                          macroprocessId = macroprocess.macroprocessId,
                                          code = macroprocess.code,
                                          name = macroprocess.name,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetMacroprocesssByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetMacroprocesssByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetMacroprocesssByCompanyIdDto>>(entities);
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
            var total = await (from macroprocess in _databaseService.Macroprocess
                               where ((macroprocess.isDeleted == null || macroprocess.isDeleted == false) && macroprocess.companyId == companyId)
                               && (macroprocess.name.ToUpper().Contains(search.ToUpper()))
                               select new MacroprocessEntity
                               {
                                   macroprocessId = macroprocess.macroprocessId,
                               }).CountAsync();
            return total;
        }

    }
}

