using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypesByCompanyId
{
    public class GetDocumentTypesByCompanyIdQuery : IGetDocumentTypesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetDocumentTypesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from documentType in _databaseService.DocumentType
                                      where ((documentType.isDeleted == null || documentType.isDeleted == false) && documentType.companyId == companyId)
                                      && (documentType.name.ToUpper().Contains(search.ToUpper()))
                                      select new DocumentTypeEntity
                                      {
                                          documentTypeId = documentType.documentTypeId,
                                          name = documentType.name,
                                          description = documentType.description,
                                          companyId = documentType.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetDocumentTypesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetDocumentTypesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetDocumentTypesByCompanyIdDto>>(entities);
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
            var total = await (from documentType in _databaseService.DocumentType
                               where ((documentType.isDeleted == null || documentType.isDeleted == false) && documentType.companyId == companyId)
                               && (documentType.name.ToUpper().Contains(search.ToUpper()))
                               select new DocumentTypeEntity
                               {
                                   documentTypeId = documentType.documentTypeId,
                               }).CountAsync();
            return total;
        }

    }
}

