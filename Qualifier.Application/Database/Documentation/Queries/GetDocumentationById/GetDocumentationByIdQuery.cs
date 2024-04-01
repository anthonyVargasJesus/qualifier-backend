using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Documentation.Queries.GetDocumentationById
{
    public class GetDocumentationByIdQuery : IGetDocumentationByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetDocumentationByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int documentationId)
        {
            try
            {
                var entity = await (from item in _databaseService.Documentation
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.documentationId == documentationId)
                                    select new DocumentationEntity()
                                    {
                                        documentationId = item.documentationId,
                                        name = item.name,
                                        description = item.description,
                                        template = item.template,
                                        standardId = item.standardId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetDocumentationByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

