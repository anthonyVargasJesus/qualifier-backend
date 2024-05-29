using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Subprocess.Queries.GetSubprocessById
{
    public class GetSubprocessByIdQuery : IGetSubprocessByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSubprocessByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int subprocessId)
        {
            try
            {
                var entity = await (from item in _databaseService.Subprocess
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.subprocessId == subprocessId)
                                    select new SubprocessEntity()
                                    {
                                        subprocessId = item.subprocessId,
                                        code = item.code,
                                        name = item.name,
                                        macroprocessId = item.macroprocessId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetSubprocessByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

