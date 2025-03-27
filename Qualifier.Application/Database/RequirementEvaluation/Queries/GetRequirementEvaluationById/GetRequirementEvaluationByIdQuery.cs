using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationById
{
    public class GetRequirementEvaluationByIdQuery : IGetRequirementEvaluationByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRequirementEvaluationByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int requirementEvaluationId)
        {
            try
            {
                var entity = await (from item in _databaseService.RequirementEvaluation
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.requirementEvaluationId == requirementEvaluationId)
                                    select new RequirementEvaluationEntity()
                                    {
                                        requirementEvaluationId = item.requirementEvaluationId,
                                        evaluationId = item.evaluationId,
                                        requirementId = item.requirementId,
                                        maturityLevelId = item.maturityLevelId,
                                        value = item.value,
                                        responsibleId = item.responsibleId,
                                        justification = item.justification == null ? "" : item.justification,
                                        improvementActions = item.improvementActions == null ? "" : item.improvementActions,
                                        standardId = item.standardId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();


                var referenceDocumentations = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                                                     where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false) && referenceDocumentation.requirementEvaluationId == requirementEvaluationId)
                                                     select new ReferenceDocumentationEntity
                                                     {
                                                         documentationId = referenceDocumentation.documentationId,
                                                     }).ToListAsync();

                var arrayReferenceDocumentations = new List<int>();

                foreach (var referenceDocumentation in referenceDocumentations)
                    arrayReferenceDocumentations.Add(referenceDocumentation.documentationId);

                if (entity != null)
                    entity.arrayReferenceDocumentations = arrayReferenceDocumentations;

                return _mapper.Map<GetRequirementEvaluationByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

