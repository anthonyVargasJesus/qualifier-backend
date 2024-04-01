using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Documentation.Queries.GetAllDocumentationsByStandardId
{
    internal class GetAllDocumentationsByStandardIdQuery : IGetAllDocumentationsByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllDocumentationsByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId)
        {
            try
            {
                var entities = await (from documentation in _databaseService.Documentation
                                      where ((documentation.isDeleted == null || documentation.isDeleted == false) && documentation.standardId == standardId)
                                      select new DocumentationEntity
                                      {
                                          documentationId = documentation.documentationId,
                                          name = documentation.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllDocumentationsByStandardIdDto> baseResponseDto = new BaseResponseDto<GetAllDocumentationsByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllDocumentationsByStandardIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

