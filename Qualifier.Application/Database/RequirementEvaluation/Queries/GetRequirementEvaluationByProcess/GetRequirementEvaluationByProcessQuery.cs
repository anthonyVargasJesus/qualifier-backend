using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Domain.Entities;


namespace Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess
{
    public class GetRequirementEvaluationByProcessQuery : IGetRequirementEvaluationByProcessQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetRequirementEvaluationByProcessQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId, int evaluationId, string search)
        {
            try
            {
                var entities = await (from requirement in _databaseService.Requirement
                                      join parent in _databaseService.Requirement
                                      on requirement.parentId equals parent.requirementId into parentJoin
                                      from parent in parentJoin.DefaultIfEmpty() 
                                      where ((requirement.isDeleted == null || requirement.isDeleted == false)
                                      && requirement.standardId == standardId
                                      && requirement.isEvaluable)
                                      select new RequirementEntity
                                      {
                                          requirementId = requirement.requirementId,
                                          numeration = requirement.numeration,
                                          name = requirement.name,
                                          description = requirement.description,
                                          level = requirement.level,
                                          parentId = requirement.parentId,
                                          letter = (requirement.letter == null) ? "" : requirement.letter,
                                          requirement = parent == null ? null : new RequirementEntity
                                          {
                                              numeration = parent.numeration,
                                              name = parent.name,
                                              level = requirement.level,
                                              letter = (requirement.letter == null) ? "" : requirement.letter,
                                          }
                                      }).ToListAsync();

                var standardEntity = new StandardEntity();
                standardEntity.setRequirementsWithChildren(entities);

                var evaluations = await (from requirementEvaluation in _databaseService.RequirementEvaluation
                                         join requirement in _databaseService.Requirement on requirementEvaluation.requirement equals requirement
                                         join maturityLevel in _databaseService.MaturityLevel on requirementEvaluation.maturityLevel equals maturityLevel
                                         join responsible in _databaseService.Responsible on requirementEvaluation.responsible equals responsible
                                         where ((requirementEvaluation.isDeleted == null || requirementEvaluation.isDeleted == false) 
                                         && requirementEvaluation.evaluationId == evaluationId)
                                         select new RequirementEvaluationEntity
                                         {
                                             requirementEvaluationId = requirementEvaluation.requirementEvaluationId,
                                             maturityLevelId = requirementEvaluation.maturityLevelId,
                                             value = requirementEvaluation.value,
                                             requirementId = requirementEvaluation.requirementId,
                                             responsibleId = requirementEvaluation.responsibleId,
                                             justification = requirementEvaluation.justification == null ? "" : requirementEvaluation.justification,
                                             improvementActions = requirementEvaluation.improvementActions == null ? "" : requirementEvaluation.improvementActions,
                                             requirement = new RequirementEntity
                                             {
                                                 requirementId = requirement.requirementId,
                                                 name = requirement.name,
                                             },
                                             maturityLevel = new MaturityLevelEntity
                                             {
                                                 abbreviation = maturityLevel.abbreviation,
                                                 color = maturityLevel.color,
                                                 value = maturityLevel.value,
                                             },
                                             responsible = new ResponsibleEntity
                                             {
                                                 name = responsible.name,
                                             },
                                             auditorStatus = requirementEvaluation.auditorStatus,
                                         }).ToListAsync();

                var allDocumentation = await (from documentation in _databaseService.Documentation
                                              where ((documentation.isDeleted == null || documentation.isDeleted == false)
                                              && documentation.standardId == standardId)
                                              select new DocumentationEntity
                                              {
                                                  documentationId = documentation.documentationId,
                                                  name = documentation.name,
                                              }).ToListAsync();

                var referenceDocumentations = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                                                     where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false)
                                                     && referenceDocumentation.evaluationId == evaluationId)
                                                     select new ReferenceDocumentationEntity
                                                     {
                                                         documentationId = referenceDocumentation.documentationId,
                                                         requirementEvaluationId = referenceDocumentation.requirementEvaluationId,
                                                         name = referenceDocumentation.name,
                                                         url = referenceDocumentation.url == null ? "" : referenceDocumentation.url,
                                                         creationDate = referenceDocumentation.creationDate,
                                                     }).ToListAsync();

                foreach (var item in evaluations)
                {
                    item.referenceDocumentations = referenceDocumentations.Where(e => e.requirementEvaluationId == item.requirementEvaluationId).OrderBy(x => x.creationDate).ToList();
                    setEvaluationState(item);
                    setAuditorStatus(item);
                }

                standardEntity.setEvaluationsToRequirements(evaluations);

                var legend = CreateLegend(standardEntity.requirements);
                var maturityLegend = CreateMaturityLegend(standardEntity.requirements);

                var response = new GetRequirementEvaluationsByProcessResponseDto
                {
                    legend = legend,
                    maturityLegend = maturityLegend,
                    requirements = _mapper.Map<List<GetRequirementEvaluationsByProcessRequirementDto>>(standardEntity.requirements)
                };

                //BaseResponseDto<GetRequirementEvaluationsByProcessRequirementDto> baseResponseDto = new BaseResponseDto<GetRequirementEvaluationsByProcessRequirementDto>();
                //baseResponseDto.data = _mapper.Map<List<GetRequirementEvaluationsByProcessRequirementDto>>(standardEntity.requirements);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }


        private static string BuildNumerationToShow(RequirementEntity node)
        {
            if (node == null) return "";

            // Si no tiene padre, devuelve solo su numerationToShow actual
            if (node.requirement == null)
                return node.numerationToShow;

            // Si tiene padre, concatena el del padre con el suyo
            return $"{BuildNumerationToShow(node.requirement)}.{node.numerationToShow}";
        }

        private void setEvaluationState(RequirementEvaluationEntity item)
        {
            const decimal OPTIMIZED_VALUE = 5.00m;
            const decimal PREDICTABLE_VALUE = 4.00m;
            const decimal ESTABLISHED_VALUE = 3.00m;
            const decimal MANAGED_VALUE = 2.00m;
            const decimal INITIAL_VALUE = 1.00m;
            const decimal NOT_IMPLEMENTED_VALUE = 1.00m;
            const decimal NOT_APPLICABLE_VALUE = 0.00m;

            var state = "Pendiente";

            if (item.maturityLevel != null)
            {
                if (item.maturityLevel.value == MANAGED_VALUE || item.maturityLevel.value == ESTABLISHED_VALUE
           || item.maturityLevel.value == PREDICTABLE_VALUE || item.maturityLevel.value == OPTIMIZED_VALUE)
                {
                    state = "Cumplido";
                }
                else if (item.maturityLevel.value == NOT_IMPLEMENTED_VALUE || item.maturityLevel.value == INITIAL_VALUE
           || item.maturityLevel.value == NOT_APPLICABLE_VALUE)
                {
                    state = "No cumplido";

                }

                item.percentage = (item.value / OPTIMIZED_VALUE) * 100;

            }

            item.state = state;

        }

        private void setAuditorStatus(RequirementEvaluationEntity item)
        {
            const int PENDING_AUDITOR_STATUS_VALUE = 1;
            const int ACCEPTED_AUDITOR_STATUS_VALUE = 2;
            const int REJECTED_AUDITOR_STATUS_VALUE = 3;

            if (item.auditorStatus == PENDING_AUDITOR_STATUS_VALUE)
                item.auditorStatusText = "En revisión";
            else if (item.auditorStatus == ACCEPTED_AUDITOR_STATUS_VALUE)
                item.auditorStatusText = "Validado";
            else if (item.auditorStatus == REJECTED_AUDITOR_STATUS_VALUE)
                item.auditorStatusText = "Rechazado";
            else
                item.auditorStatusText = "Estado desconocido";
        }



        List<GetRequirementEvaluationsByProcessLegendDto> CreateLegend(List<RequirementEntity> requirements)
        {
            int approved = 0;
            int notApproved = 0;
            int pending = 0;

            CountStates(requirements, ref approved, ref notApproved, ref pending);

            Console.WriteLine($"Cumplidos: {approved}");
            Console.WriteLine($"No cumplidos: {notApproved}");
            Console.WriteLine($"Pendientes: {pending}");

            var cumplidos = new GetRequirementEvaluationsByProcessLegendDto
            {
                name = "CUMPLIDOS",
                value = approved,
                color = "#4CAF50"
            };

            var noCumplidos = new GetRequirementEvaluationsByProcessLegendDto
            {
                name = "NO CUMPLIDOS",
                value = notApproved,
                color = "#F44336"
            };

            var pendientes = new GetRequirementEvaluationsByProcessLegendDto
            {
                name = "PENDIENTES",
                value = pending,
                color = "#FF9800"
            };

            var legend = new List<GetRequirementEvaluationsByProcessLegendDto>
            {
                cumplidos,
                noCumplidos,
                pendientes
            };

            return legend;
        }

        void CountStates(List<RequirementEntity> requirements,
                         ref int approved,
                         ref int notApproved,
                         ref int pending)
        {
            foreach (var req in requirements)
            {
                // 🔎 Recorremos todas las evaluaciones de este requirement
                if (req.requirementEvaluations != null && req.requirementEvaluations.Any())
                {
                    foreach (var eval in req.requirementEvaluations)
                    {
                        switch (eval.state)
                        {
                            case "Cumplido":
                                approved++;
                                break;
                            case "No cumplido":
                                notApproved++;
                                break;
                            case "Pendiente":
                                pending++;
                                break;
                        }
                    }
                }

                // 👶 Si el requirement tiene hijos, seguir contando
                if (req.children != null && req.children.Any())
                {
                    CountStates(req.children, ref approved, ref notApproved, ref pending);
                }
            }
        }

        List<GetRequirementEvaluationsByProcessMaturityLevelDto>
     CreateMaturityLegend(List<RequirementEntity> requirements)
        {
            var legend = new Dictionary<decimal, GetRequirementEvaluationsByProcessMaturityLevelDto>();

            CountMaturity(requirements, legend);

            // Calcular valores porcentuales (value)
            int totalAll = legend.Values.Sum(x => x.total);
            foreach (var item in legend.Values)
            {
                item.value = totalAll > 0
                    ? Math.Round((decimal)item.total / totalAll * 100, 2)
                    : 0;
            }

            // 📌 Ordenar: primero todos los que tengan value > 0, y al final el 0 (Pendiente)
            return legend.Values
                         .OrderBy(x => x.abbreviation == "PENDIENTES" ? decimal.MaxValue :
                                       legend.First(l => l.Value == x).Key) // usamos la key real
                         .ToList();
        }

        void CountMaturity(List<RequirementEntity> requirements,
            Dictionary<decimal, GetRequirementEvaluationsByProcessMaturityLevelDto> legend)
        {
            foreach (var req in requirements)
            {
                if (req.requirementEvaluations != null && req.requirementEvaluations.Any())
                {
                    foreach (var eval in req.requirementEvaluations)
                    {
                        decimal levelValue;
                        string abbreviation;
                        string color;

                        if (eval.maturityLevel == null)
                        {
                            // 📌 Caso PENDIENTE
                            levelValue = 0;
                            abbreviation = "PENDIENTES";
                            color = "#FF9800"; // gris
                        }
                        else
                        {
                            levelValue = eval.maturityLevel.value;
                            abbreviation = eval.maturityLevel.abbreviation!;
                            color = eval.maturityLevel.color!;
                        }

                        if (!legend.ContainsKey(levelValue))
                        {
                            legend[levelValue] = new GetRequirementEvaluationsByProcessMaturityLevelDto
                            {
                                abbreviation = abbreviation,
                                color = color,
                                total = 0,
                                value = 0
                            };
                        }

                        legend[levelValue].total++;
                    }
                }

                if (req.children != null && req.children.Any())
                    CountMaturity(req.children, legend);
            }
        }



        void Search(List<RequirementEntity> requirements)
        {
            foreach (var requirement in requirements)
            {
                if (requirement.children != null && requirement.children.Any())
                    Search(requirement.children);
                
            }
        }




    }
}
