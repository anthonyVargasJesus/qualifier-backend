using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Version.Queries.GetVersionsByDocumentationId
{
    public class GetVersionsByDocumentationIdQuery : IGetVersionsByDocumentationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetVersionsByDocumentationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int documentationId)
        {
            try
            {
                var entities = await (from version in _databaseService.Version
                                      join confidentialityLevel in _databaseService.ConfidentialityLevel on version.confidentialityLevel equals confidentialityLevel
                                      where ((version.isDeleted == null || version.isDeleted == false) && version.documentationId == documentationId)
                                      && (version.name.ToUpper().Contains(search.ToUpper()))
                                      select new VersionEntity
                                      {
                                          versionId = version.versionId,
                                          number = version.number,
                                          code = version.code,
                                          name = version.name,
                                          date = version.date,
                                          isCurrent = version.isCurrent,
                                          fileName = version.fileName,
                                          confidentialityLevel = new ConfidentialityLevelEntity
                                          {
                                              name = confidentialityLevel.name,
                                          },
                                      })
                                      .OrderBy(x => x.number)
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetVersionsByDocumentationIdDto> baseResponseDto = new BaseResponseDto<GetVersionsByDocumentationIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetVersionsByDocumentationIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, documentationId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int documentationId)
        {
            var total = await (from version in _databaseService.Version
                               join confidentialityLevel in _databaseService.ConfidentialityLevel on version.confidentialityLevel equals confidentialityLevel
                               where ((version.isDeleted == null || version.isDeleted == false) && version.documentationId == documentationId)
                               && (version.name.ToUpper().Contains(search.ToUpper()))
                               select new VersionEntity
                               {
                                   versionId = version.versionId,
                               }).CountAsync();
            return total;
        }

    }
}

