using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypeById
{
    public class GetDocumentTypeByIdQuery : IGetDocumentTypeByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetDocumentTypeByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int documentTypeId)
        {
            try
            {
                var entity = await (from item in _databaseService.DocumentType
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.documentTypeId == documentTypeId)
                                    select new DocumentTypeEntity()
                                    {
                                        documentTypeId = item.documentTypeId,
                                        name = item.name,
                                        description = item.description,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetDocumentTypeByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

