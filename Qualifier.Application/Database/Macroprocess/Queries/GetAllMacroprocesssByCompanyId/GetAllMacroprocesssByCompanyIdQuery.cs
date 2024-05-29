using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Macroprocess.Queries.GetAllMacroprocesssByCompanyId
{
    internal class GetAllMacroprocesssByCompanyIdQuery : IGetAllMacroprocesssByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllMacroprocesssByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from macroprocess in _databaseService.Macroprocess
                                      where ((macroprocess.isDeleted == null || macroprocess.isDeleted == false) && macroprocess.companyId == companyId)
                                      select new MacroprocessEntity
                                      {
                                          macroprocessId = macroprocess.macroprocessId,
                                          code = macroprocess.code,
                                          name = macroprocess.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllMacroprocesssByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllMacroprocesssByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllMacroprocesssByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

