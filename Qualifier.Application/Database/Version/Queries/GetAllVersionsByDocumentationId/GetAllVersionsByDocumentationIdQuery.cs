using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Version.Queries.GetAllVersionsByDocumentationId
{
    internal class GetAllVersionsByDocumentationIdQuery : IGetAllVersionsByDocumentationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllVersionsByDocumentationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int documentationId)
        {
            try
            {
                var entities = await (from version in _databaseService.Version
                                      where ((version.isDeleted == null || version.isDeleted == false) && version.documentationId == documentationId)
                                      select new VersionEntity
                                      {
                                          versionId = version.versionId,
                                          name = version.name,
                                          number = version.number,
                                      }).OrderBy(e => e.number).ToListAsync();

                BaseResponseDto<GetAllVersionsByDocumentationIdDto> baseResponseDto = new BaseResponseDto<GetAllVersionsByDocumentationIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllVersionsByDocumentationIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

