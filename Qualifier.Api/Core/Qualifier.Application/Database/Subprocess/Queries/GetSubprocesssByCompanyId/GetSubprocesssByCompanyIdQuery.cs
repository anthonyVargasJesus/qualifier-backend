using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Subprocess.Queries.GetSubprocesssByCompanyId
{
    public class GetSubprocesssByCompanyIdQuery : IGetSubprocesssByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSubprocesssByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from subprocess in _databaseService.Subprocess
                                      join macroprocess in _databaseService.Macroprocess on subprocess.macroprocess equals macroprocess
                                      where ((subprocess.isDeleted == null || subprocess.isDeleted == false) && subprocess.companyId == companyId)
                                      && (subprocess.name.ToUpper().Contains(search.ToUpper()))
                                      select new SubprocessEntity
                                      {
                                          subprocessId = subprocess.subprocessId,
                                          code = subprocess.code,
                                          name = subprocess.name,
                                          macroprocessId = subprocess.macroprocessId,
                                          macroprocess = new MacroprocessEntity
                                          {
                                              code = macroprocess.code,
                                              name = macroprocess.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetSubprocesssByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetSubprocesssByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetSubprocesssByCompanyIdDto>>(entities);
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
            var total = await (from subprocess in _databaseService.Subprocess
                               join macroprocess in _databaseService.Macroprocess on subprocess.macroprocess equals macroprocess
                               where ((subprocess.isDeleted == null || subprocess.isDeleted == false) && subprocess.companyId == companyId)
                               && (subprocess.name.ToUpper().Contains(search.ToUpper()))
                               select new SubprocessEntity
                               {
                                   subprocessId = subprocess.subprocessId,
                               }).CountAsync();
            return total;
        }

    }
}

