using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Version.Queries.GetVersionById
{
    public class GetVersionByIdQuery : IGetVersionByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetVersionByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int versionId)
        {
            try
            {
                var entity = await (from item in _databaseService.Version
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.versionId == versionId)
                                    select new VersionEntity()
                                    {
                                        versionId = item.versionId,
                                        number = item.number,
                                        code = item.code,
                                        name = item.name,
                                        confidentialityLevelId = item.confidentialityLevelId,
                                        documentationId = item.documentationId,
                                        date = item.date,
                                        isCurrent = item.isCurrent,
                                        fileName = item.fileName,
                                        description = item.description,
                                        standardId = item.standardId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetVersionByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

