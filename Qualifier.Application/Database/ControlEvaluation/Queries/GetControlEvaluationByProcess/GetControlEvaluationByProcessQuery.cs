using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess
{
    public class GetControlEvaluationByProcessQuery : IGetControlEvaluationByProcessQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetControlEvaluationByProcessQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId, int evaluationId)
        {
            try
            {
                var groups = await (from item in _databaseService.ControlGroup
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == standardId)
                                    select new ControlGroupEntity
                                    {
                                        controlGroupId = item.controlGroupId,
                                        number = item.number,
                                        name = item.name,
                                    }).OrderBy(x => x.number)
                                          .ToListAsync();

                var controls = await (from item in _databaseService.Control
                                      where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == standardId)
                                      select new ControlEntity
                                      {
                                          controlId = item.controlId,
                                          controlGroupId = item.controlGroupId,
                                          number = item.number,
                                          name = item.name,
                                      }).OrderBy(x => x.number)
                                  .ToListAsync();



                var evaluations = await (from controlEvaluation in _databaseService.ControlEvaluation
                                         join control in _databaseService.Control on controlEvaluation.control equals control
                                         join maturityLevel in _databaseService.MaturityLevel on controlEvaluation.maturityLevel equals maturityLevel
                                         join responsible in _databaseService.Responsible on controlEvaluation.responsible equals responsible
                                         where ((controlEvaluation.isDeleted == null || controlEvaluation.isDeleted == false) && controlEvaluation.evaluationId == evaluationId)
                                         select new ControlEvaluationEntity
                                         {
                                             controlEvaluationId = controlEvaluation.controlEvaluationId,
                                             maturityLevelId = controlEvaluation.maturityLevelId,
                                             value = controlEvaluation.value,
                                             controlId = controlEvaluation.controlId,
                                             responsibleId = controlEvaluation.responsibleId,
                                             justification = controlEvaluation.justification,
                                             improvementActions = controlEvaluation.improvementActions,
                                             control = new ControlEntity
                                             {
                                                 controlId = control.controlId,
                                                 number = control.number,
                                                 name = control.name,
                                             },
                                             maturityLevel = new MaturityLevelEntity
                                             {
                                                 abbreviation = maturityLevel.abbreviation,
                                                 color = maturityLevel.color,
                                             },
                                             responsible = new ResponsibleEntity
                                             {
                                                 name = responsible.name,
                                             },

                                         }).ToListAsync();


                foreach (ControlEntity item in controls)
                    item.setEvaluations(evaluations);

                setControlsWithGroup(groups, controls);

                BaseResponseDto<GetControlEvaluationsByProcessControlGroupDto> baseResponseDto = new BaseResponseDto<GetControlEvaluationsByProcessControlGroupDto>();
                List<GetControlEvaluationsByProcessControlGroupDto> data = _mapper.Map<List<GetControlEvaluationsByProcessControlGroupDto>>(groups);

                baseResponseDto.data = data;
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public void setControlsWithGroup(List<ControlGroupEntity> groups, List<ControlEntity> controls)
        {
            foreach (var group in groups)
            {
                group.controls = controls.Where(x => x.controlGroupId == group.controlGroupId).OrderBy(x => x.number).ToList();
                setNumeration(group.controls, group.number);
            }
               
        }

        private void setNumeration(List<ControlEntity> controls, int parentNumber)
        {
            foreach (ControlEntity item in controls)
               item.numerationToShow = parentNumber.ToString() + "." + item.number.ToString();
        }

    }
}
