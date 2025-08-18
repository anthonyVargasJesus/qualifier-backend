using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationById
{
    public class GetReferenceDocumentationByIdQuery : IGetReferenceDocumentationByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetReferenceDocumentationByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int referenceDocumentationId)
        {
            try
            {
                var entity = await (from item in _databaseService.ReferenceDocumentation
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.referenceDocumentationId == referenceDocumentationId)
                                    select new ReferenceDocumentationEntity()
                                    {
                                        referenceDocumentationId = item.referenceDocumentationId,
                                        name = item.name,
                                        url = item.url,
                                        description = (item.description == null)?"": item.description,
                                        documentationId = item.documentationId,
                                        requirementEvaluationId = item.requirementEvaluationId,
                                        controlEvaluationId = item.controlEvaluationId,
                                        evaluationId = item.evaluationId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetReferenceDocumentationByIdDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

