using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
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

        public async Task<Object> Execute(int standardId, int evaluationId, int userId = 0, bool scopeToUser = false)
        {
            try
            {
                // scopeToUser solo lo activa /gap/panel — las pantallas viejas (control-evaluation)
                // siguen mandando el default (false) y ven todos los grupos, como hoy.
                List<int>? assignedGroupIds = null;
                if (scopeToUser)
                {
                    assignedGroupIds = await (from x in _databaseService.UserControlGroup
                                              where (x.isDeleted == null || x.isDeleted == false)
                                              && x.userId == userId && x.standardId == standardId
                                              select x.controlGroupId).ToListAsync();
                }

                var groups = await (from item in _databaseService.ControlGroup
                                    where ((item.isDeleted == null || item.isDeleted == false)
                                    && item.standardId == standardId
                                    && (assignedGroupIds == null || assignedGroupIds.Contains(item.controlGroupId)))
                                    select new ControlGroupEntity
                                    {
                                        controlGroupId = item.controlGroupId,
                                        number = item.number,
                                        name = item.name,
                                    }).OrderBy(x => x.number)
                                    .ToListAsync();

                var controls = await (from item in _databaseService.Control
                                      where ((item.isDeleted == null || item.isDeleted == false) 
                                      && item.standardId == standardId)
                                      select new ControlEntity
                                      {
                                          controlId = item.controlId,
                                          controlGroupId = item.controlGroupId,
                                          number = item.number,
                                          name = item.name,
                                          defaultResponsibleId = item.defaultResponsibleId,
                                      }).OrderBy(x => x.number)
                                  .ToListAsync();

                var evaluations = await (from controlEvaluation in _databaseService.ControlEvaluation
                                         join control in _databaseService.Control on controlEvaluation.control equals control
                                         join maturityLevel in _databaseService.MaturityLevel on controlEvaluation.maturityLevel equals maturityLevel
                                         // LEFT JOIN (no INNER): la app móvil no obliga a elegir responsable y guarda
                                         // 0 cuando el control no tiene uno por defecto configurado; con INNER JOIN esa
                                         // fila desaparecía en silencio de este listado (y del de Angular) aunque
                                         // estuviera guardada en la base de datos.
                                         join responsible in _databaseService.Responsible on controlEvaluation.responsible equals responsible into responsibleJoin
                                         from responsible in responsibleJoin.DefaultIfEmpty()
                                         where ((controlEvaluation.isDeleted == null || controlEvaluation.isDeleted == false)
                                         && controlEvaluation.evaluationId == evaluationId)
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
                                                 value = maturityLevel.value,
                                             },
                                             responsible = responsible == null ? null : new ResponsibleEntity
                                             {
                                                 name = responsible.name,
                                             },
                                         }).ToListAsync();

                var referenceDocumentations = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                                                     where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false)
                                                     && referenceDocumentation.evaluationId == evaluationId)
                                                     select new ReferenceDocumentationEntity
                                                     {
                                                         documentationId = referenceDocumentation.documentationId,
                                                         controlEvaluationId = referenceDocumentation.controlEvaluationId,
                                                         name = referenceDocumentation.name,
                                                         url = referenceDocumentation.url == null ? "" : referenceDocumentation.url,
                                                     }).ToListAsync();

                foreach (var item in evaluations)
                {
                    item.referenceDocumentations = referenceDocumentations.Where(e => e.controlEvaluationId == item.controlEvaluationId).ToList();
                }

                foreach (ControlEntity item in controls)
                    item.setEvaluations(evaluations);

                setControlsWithGroup(groups, controls);

                foreach (var group in groups)
                    foreach (var control in group.controls)
                        foreach (var item in control.controlEvaluations)
                        {
                            setEvaluationState(item);
                            // item.control es una copia separada construida en la proyección de
                            // evaluations (arriba); setControlsWithGroup calculó numerationToShow
                            // sobre la lista "controls", no sobre esa copia, así que hay que
                            // propagarlo acá para que se vea en la pantalla de evaluación.
                            if (item.control != null)
                                item.control.numerationToShow = control.numerationToShow;
                        }

                var legend = CreateLegend(groups);
                var maturityLegend = CreateMaturityLegend(groups);

                var response = new GetControlEvaluationsByProcessResponseDto
                {
                    legend = legend,
                    maturityLegend = maturityLegend,
                    groups = _mapper.Map<List<GetControlEvaluationsByProcessControlGroupDto>>(groups)
                };

                return response;
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

        private void setEvaluationState(ControlEvaluationEntity item)
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

        List<GetControlEvaluationsByProcessLegendDto> CreateLegend(List<ControlGroupEntity> groups)
        {
            int approved = 0;
            int notApproved = 0;
            int pending = 0;

            countStates(groups, ref approved, ref notApproved, ref pending);

            Console.WriteLine($"Cumplidos: {approved}");
            Console.WriteLine($"No cumplidos: {notApproved}");
            Console.WriteLine($"Pendientes: {pending}");

            var cumplidos = new GetControlEvaluationsByProcessLegendDto
            {
                name = "CUMPLIDOS",
                value = approved,
                color = "#4CAF50"
            };

            var noCumplidos = new GetControlEvaluationsByProcessLegendDto
            {
                name = "NO CUMPLIDOS",
                value = notApproved,
                color = "#F44336"
            };

            var pendientes = new GetControlEvaluationsByProcessLegendDto
            {
                name = "PENDIENTES",
                value = pending,
                color = "#FF9800"
            };

            var legend = new List<GetControlEvaluationsByProcessLegendDto>
            {
                cumplidos,
                noCumplidos,
                pendientes
            };

            return legend;
        }

        void countStates(List<ControlGroupEntity> groups,
                 ref int approved,
                 ref int notApproved,
                 ref int pending)
        {
            foreach (var group in groups)
                if (group.controls != null && group.controls.Any())
                    foreach (var control in group.controls)
                    {
                        foreach (var eval in control.controlEvaluations)
                        {
                            if (eval.maturityLevel == null)
                            {
                                pending++;
                            }
                            else
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


                    }
        }



        List<GetControlEvaluationsByProcessMaturityLevelDto>
CreateMaturityLegend(List<ControlGroupEntity> groups)
        {
            var legend = new Dictionary<decimal, GetControlEvaluationsByProcessMaturityLevelDto>();

            CountMaturity(groups, legend);

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

        void CountMaturity(List<ControlGroupEntity> groups,
            Dictionary<decimal, GetControlEvaluationsByProcessMaturityLevelDto> legend)
        {
            foreach (var group in groups)
            {
                    if (group.controls != null && group.controls.Any())
                        foreach (var control in group.controls)
                        {
                        if (control.controlEvaluations != null && control.controlEvaluations.Any())
                            foreach (var eval in control.controlEvaluations)
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
                                    legend[levelValue] = new GetControlEvaluationsByProcessMaturityLevelDto
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

            }
        }


        private const decimal OPTIMIZED_VALUE = 5.00m;
        private const decimal PREDICTABLE_VALUE = 4.00m;
        private const decimal ESTABLISHED_VALUE = 3.00m;
        private const decimal MANAGED_VALUE = 2.00m;
        private const decimal INITIAL_VALUE = 1.00m;
        private const decimal NOT_IMPLEMENTED_VALUE = 1.00m;
        private const decimal NOT_APPLICABLE_VALUE = 0.00m;

        private static readonly HashSet<decimal> FulfilledValues = new()
{
    MANAGED_VALUE,
    ESTABLISHED_VALUE,
    PREDICTABLE_VALUE,
    OPTIMIZED_VALUE
};

        private static readonly HashSet<decimal> NotFulfilledValues = new()
{
    INITIAL_VALUE,
    NOT_IMPLEMENTED_VALUE,
    NOT_APPLICABLE_VALUE
};

        private void SetControlEvaluationState(ControlEvaluationEntity item)
        {
            if (item?.maturityLevel == null)
            {
                item.state = "Pendiente";
                return;
            }

            var maturityValue = item.maturityLevel.value;

            item.state = GetState(maturityValue);
            item.percentage = (item.value / OPTIMIZED_VALUE) * 100;
        }

        private string GetState(decimal maturityValue)
        {
            if (FulfilledValues.Contains(maturityValue))
                return "Cumplido";

            if (NotFulfilledValues.Contains(maturityValue))
                return "No cumplido";

            return "Pendiente";
        }


    }
}
