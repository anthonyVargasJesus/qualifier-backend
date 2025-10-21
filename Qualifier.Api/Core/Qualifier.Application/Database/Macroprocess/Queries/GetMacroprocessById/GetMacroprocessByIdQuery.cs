using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Macroprocess.Queries.GetMacroprocessById
{
    public class GetMacroprocessByIdQuery : IGetMacroprocessByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMacroprocessByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int macroprocessId)
        {
            try
            {
                var entity = await (from item in _databaseService.Macroprocess
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.macroprocessId == macroprocessId)
                                    select new MacroprocessEntity()
                                    {
                                        macroprocessId = item.macroprocessId,
                                        code = item.code,
                                        name = item.name,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetMacroprocessByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

