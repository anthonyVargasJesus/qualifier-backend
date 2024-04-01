using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationById
{
    public class GetControlEvaluationByIdQuery : IGetControlEvaluationByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlEvaluationByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int controlEvaluationId)
        {
            try
            {
                var entity = await (from item in _databaseService.ControlEvaluation
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.controlEvaluationId == controlEvaluationId)
                                    select new ControlEvaluationEntity()
                                    {
                                        controlEvaluationId = item.controlEvaluationId,
                                        evaluationId = item.evaluationId,
                                        controlId = item.controlId,
                                        maturityLevelId = item.maturityLevelId,
                                        value = item.value,
                                        responsibleId = item.responsibleId,
                                        justification = item.justification,
                                        improvementActions = item.improvementActions,
                                        controlDescription = (item.controlDescription == null) ? "" : item.controlDescription,
                                        controlType = (item.controlType == null)?  "": item.controlType,
                                        standardId = item.standardId,
                                    }).FirstOrDefaultAsync();

                var referenceDocumentations = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                                                     where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false) && referenceDocumentation.controlEvaluationId == controlEvaluationId)
                                                     select new ReferenceDocumentationEntity
                                                     {
                                                         documentationId = referenceDocumentation.documentationId,
                                                     }).ToListAsync();

                var arrayReferenceDocumentations = new List<int>();

                foreach (var referenceDocumentation in referenceDocumentations)
                    arrayReferenceDocumentations.Add(referenceDocumentation.documentationId);

                if (entity != null)
                    entity.arrayReferenceDocumentations = arrayReferenceDocumentations;

                return _mapper.Map<GetControlEvaluationByIdDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

