using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.DocumentType.Queries.GetAllDocumentTypesByCompanyId
{
    internal class GetAllDocumentTypesByCompanyIdQuery : IGetAllDocumentTypesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllDocumentTypesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from documentType in _databaseService.DocumentType
                                      where ((documentType.isDeleted == null || documentType.isDeleted == false) && documentType.companyId == companyId)
                                      select new DocumentTypeEntity
                                      {
                                          documentTypeId = documentType.documentTypeId,
                                          name = documentType.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllDocumentTypesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllDocumentTypesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllDocumentTypesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

