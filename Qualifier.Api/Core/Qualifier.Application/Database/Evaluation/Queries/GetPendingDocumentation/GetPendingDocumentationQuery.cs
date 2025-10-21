using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Evaluation.Queries.GetPendingDocumentation
{
    public class GetPendingDocumentationQuery : IGetPendingDocumentationQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetPendingDocumentationQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int skip, int pageSize, string search, int standardId, int evaluationId)
        {
            try
            {

                var allControlGroups = await (from controlGroup in _databaseService.ControlGroup
                                      where ((controlGroup.isDeleted == null || controlGroup.isDeleted == false) && controlGroup.standardId == standardId)
                                      select new ControlGroupEntity
                                      {
                                          controlGroupId = controlGroup.controlGroupId,
                                          number = controlGroup.number,
                                      }).ToListAsync();

                var allControls = await (from control in _databaseService.Control
                                      where ((control.isDeleted == null || control.isDeleted == false) && control.controlGroup.standardId == standardId)
                                      select new ControlEntity
                                      {
                                          controlId = control.controlId,
                                          controlGroupId = control.controlGroupId,
                                          number = control.number,
                                      }).OrderBy(x => x.number)
                                      .ToListAsync();


                var allDocumentation = await (from documentation in _databaseService.Documentation
                                              where ((documentation.isDeleted == null || documentation.isDeleted == false)
                                              && documentation.standardId == standardId)
                                              select new DocumentationEntity
                                              {
                                                  documentationId = documentation.documentationId,
                                                  name = documentation.name,
                                              }).OrderBy(e => e.name).ToListAsync();

                var allRequirements = await (from requirement in _databaseService.Requirement
                                             where ((requirement.isDeleted == null || requirement.isDeleted == false) && requirement.standardId == standardId)
                                             select new RequirementEntity
                                             {
                                                 requirementId = requirement.requirementId,
                                                 numeration = requirement.numeration,
                                                 name = requirement.name,
                                                 description = requirement.description,
                                                 level = requirement.level,
                                                 parentId = requirement.parentId,
                                             }).ToListAsync();

                var referenceDocumentations = await (from referenceDocumentation in _databaseService.ReferenceDocumentation

                                                     join bt in _databaseService.RequirementEvaluation on referenceDocumentation.requirementEvaluationId equals bt.requirementEvaluationId into _bt
                                                     from requirementEvaluation in _bt.DefaultIfEmpty()
                                                     join req in _databaseService.Requirement on requirementEvaluation.requirementId equals req.requirementId into _req
                                                     from requirement in _req.DefaultIfEmpty()

                                                     join bt2 in _databaseService.ControlEvaluation on referenceDocumentation.controlEvaluationId equals bt2.controlEvaluationId into _bt2
                                                     from controlEvaluation in _bt2.DefaultIfEmpty()
                                                     join con in _databaseService.Control on controlEvaluation.controlId equals con.controlId into _con
                                                     from control in _con.DefaultIfEmpty()

                                                     where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false)
                                                     && referenceDocumentation.evaluationId == evaluationId)
                                                     select new ReferenceDocumentationEntity
                                                     {
                                                         documentationId = referenceDocumentation.documentationId,
                                                         requirementEvaluationId = referenceDocumentation.requirementEvaluationId,
                                                         name = referenceDocumentation.name,
                                                         requirement = (requirement == null) ? null : new RequirementEntity
                                                         {
                                                             requirementId = requirement.requirementId,
                                                             numeration = requirement.numeration,
                                                             name = requirement.name,
                                                             level = requirement.level,
                                                         },
                                                         control = (control == null) ? null : new ControlEntity
                                                         {
                                                             controlId = control.controlId,
                                                             number = control.number,
                                                             name = control.name,
                                                         },
                                                     }).ToListAsync();

                var versions = await (from version in _databaseService.Version
                                      where ((version.isDeleted == null || version.isDeleted == false)
                                      && version.standardId == standardId && version.isCurrent)
                                      select new VersionEntity
                                      {
                                          versionId = version.versionId,
                                          documentationId = version.documentationId,
                                          name = version.name,
                                      }).ToListAsync();

                var standard = new StandardEntity();
                standard.setRequirementsWithChildren(allRequirements);
                standard.setControlsWithChildren(allControlGroups, allControls);

                foreach (var document in allDocumentation)
                {
                    List<RequirementEntity> requirements = new List<RequirementEntity>();
                    List<ControlEntity> controls = new List<ControlEntity>();
                    foreach (var referenceDocumentation in referenceDocumentations)
                    {
                        if (document.documentationId == referenceDocumentation.documentationId)
                        {
                            if (referenceDocumentation.requirement != null)
                            {
                                referenceDocumentation.requirement.numerationToShow = standard.getNumerationToShow(referenceDocumentation.requirement.requirementId);
                                requirements.Add(referenceDocumentation.requirement);
                            } 

                            if (referenceDocumentation.control != null)
                            {
                                referenceDocumentation.control.numerationToShow = standard.getControlNumerationToShow(referenceDocumentation.control.controlId);
                                controls.Add(referenceDocumentation.control);
                            }
                               
                        }
                    }
                    document.requirements = requirements;
                    document.controls = controls;
                }

                List<DocumentationEntity> temporalDocumentation = new List<DocumentationEntity>();
                foreach (var item in allDocumentation)
                {
                    if (item.requirements.Count>0 || item.controls.Count>0)
                        temporalDocumentation.Add(item);
                }

                List<DocumentationEntity> temporalDocumentation2 = new List<DocumentationEntity>();
                foreach (var item in temporalDocumentation)
                {
                    if (versions.Where(e => e.documentationId == item.documentationId).Count() == 0)
                    temporalDocumentation2.Add(item);
                }

                int total = temporalDocumentation2.Count();

                temporalDocumentation2 = temporalDocumentation2
                                        .Where(e => e.name.ToUpper().Contains(search.ToUpper())).ToList();
                temporalDocumentation2 = temporalDocumentation2
                                       .Skip(skip).Take(pageSize).ToList();

                BaseResponseDto<GetPendingDocumentationDto> baseResponseDto = new BaseResponseDto<GetPendingDocumentationDto>();
                baseResponseDto.data = _mapper.Map<List<GetPendingDocumentationDto>>(temporalDocumentation2);
                baseResponseDto.pagination = Pagination.GetPagination(total, pageSize);
                return baseResponseDto;

            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}
