using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Breach.Queries.GetBreachById
{
    public class GetBreachByIdQuery : IGetBreachByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetBreachByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int breachId)
        {
            try
            {
                var entity = await (from item in _databaseService.Breach
                                    join bt2 in _databaseService.Control on item.controlId equals bt2.controlId into _bt2
                                    from control in _bt2.DefaultIfEmpty()
                                    join bt in _databaseService.Requirement on item.requirementId equals bt.requirementId into _bt
                                    from requirement in _bt.DefaultIfEmpty()
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.breachId == breachId)
                                    select new BreachEntity()
                                    {
                                        breachId = item.breachId,
                                        evaluationId = item.evaluationId,
                                        standardId = item.standardId,
                                        type = item.type,
                                        requirementId = item.requirementId,
                                        controlId = item.controlId,
                                        title = item.title,
                                        description = item.description,
                                        breachSeverityId = item.breachSeverityId,
                                        breachStatusId = item.breachStatusId,
                                        responsibleId = item.responsibleId,
                                        evidenceDescription = item.evidenceDescription,
                                        numerationToShow = item.numerationToShow,
                                        control = (control != null) ? new ControlEntity
                                        {
                                            controlId = control.controlId,
                                            name = control.name,
                                        } : null,
                                        requirement = (requirement != null) ? new RequirementEntity
                                        {
                                            requirementId = requirement.requirementId,
                                            name = requirement.name,
                                        } : null,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetBreachByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

