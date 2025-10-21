using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using System.Transactions;

namespace Qualifier.Application.Database.Version.Commands.CreateVersion

{
    public class CreateVersionCommand : ICreateVersionCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CreateVersionCommand(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(CreateVersionDto model)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Notification notification = this.createValidation(model);
                    if (notification.hasErrors())
                        return BaseApplication.getApplicationErrorResponse(notification.errors);

                    var entity = _mapper.Map<VersionEntity>(model);
                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    await _databaseService.Version.AddAsync(entity);
                    await _databaseService.SaveAsync();


                    if (model.versionReferenceId != null)
                    {
                        var defaultSections = await (from section in _databaseService.Section
                                                     where ((section.isDeleted == null || section.isDeleted == false)
                                                     && section.versionId == model.versionReferenceId)
                                                     select new SectionEntity
                                                     {
                                                         sectionId = section.sectionId,
                                                         numeration = section.numeration,
                                                         name = section.name,
                                                         level = section.level,
                                                         parentId = section.parentId,
                                                         description = section.description,
                                                     }).ToListAsync();


                        //ingreso los de primer level
                        const int FIRST_LEVEL = 1;

                        foreach (var defaultSection in defaultSections.Where(x => x.level == FIRST_LEVEL).ToList())
                        {

                            var section = new SectionEntity
                            {
                                numeration = defaultSection.numeration,
                                name = defaultSection.name,
                                level = defaultSection.level,
                                parentId = 0,
                                versionId = entity.versionId,
                                documentationId = entity.documentationId,
                                companyId = entity.companyId,
                                description = defaultSection.description,
                            };

                            section.creationDate = DateTime.UtcNow;
                            section.creationUserId = model.creationUserId;
                            await _databaseService.Section.AddAsync(section);
                            await _databaseService.SaveAsync();
                        }

                        //traigo las secciones de nivel 1
                        var sections = await (from section in _databaseService.Section
                                              where ((section.isDeleted == null || section.isDeleted == false)
                                              && section.versionId == entity.versionId && section.level == FIRST_LEVEL)
                                              select new SectionEntity
                                              {
                                                  sectionId = section.sectionId,
                                                  numeration = section.numeration,
                                                  name = section.name,
                                                  description = (section.description == null) ? "" : section.description,
                                                  level = section.level,
                                                  parentId = section.parentId,
                                              })
                          .OrderBy(x => x.numeration)
                          .ToListAsync();

                        //ingresando las de segundo nivel
                        const int SECOND_LEVEL = 2;
                        foreach (var defaultSection in defaultSections.Where(x => x.level == SECOND_LEVEL).ToList())
                        {
                            var parent = getParent(defaultSections, defaultSection.parentId);

                            var section = new SectionEntity
                            {
                                numeration = defaultSection.numeration,
                                name = defaultSection.name,
                                level = defaultSection.level,
                                parentId = getId(sections, parent.level, parent.name),
                                versionId = entity.versionId,
                                documentationId = entity.documentationId,
                                companyId = entity.companyId,
                                description = defaultSection.description,
                            };

                            section.creationDate = DateTime.UtcNow;
                            section.creationUserId = model.creationUserId;
                            await _databaseService.Section.AddAsync(section);
                            await _databaseService.SaveAsync();

                        }


                    }
                    else
                    {
                        var defaultSections = await (from section in _databaseService.Section
                                                     where ((section.isDeleted == null || section.isDeleted == false)
                                                     && section.documentationId == model.documentationId && section.versionId == null)
                                                     select new SectionEntity
                                                     {
                                                         sectionId = section.sectionId,
                                                         numeration = section.numeration,
                                                         name = section.name,
                                                         level = section.level,
                                                         parentId = section.parentId,
                                                         description = section.description,
                                                     }).ToListAsync();
                        //ingreso los de primer level
                        const int FIRST_LEVEL = 1;

                        foreach (var defaultSection in defaultSections.Where(x => x.level == FIRST_LEVEL).ToList())
                        {

                            var section = new SectionEntity
                            {
                                numeration = defaultSection.numeration,
                                name = defaultSection.name,
                                level = defaultSection.level,
                                parentId = 0,
                                versionId = entity.versionId,
                                documentationId = entity.documentationId,
                                companyId = entity.companyId,
                                description = defaultSection.description,
                            };

                            section.creationDate = DateTime.UtcNow;
                            section.creationUserId = model.creationUserId;
                            await _databaseService.Section.AddAsync(section);
                            await _databaseService.SaveAsync();
                        }


                        //traigo las secciones de nivel 1
                        var sections = await (from section in _databaseService.Section
                                              where ((section.isDeleted == null || section.isDeleted == false)
                                              && section.versionId == entity.versionId && section.level == FIRST_LEVEL)
                                              select new SectionEntity
                                              {
                                                  sectionId = section.sectionId,
                                                  numeration = section.numeration,
                                                  name = section.name,
                                                  description = (section.description == null) ? "" : section.description,
                                                  level = section.level,
                                                  parentId = section.parentId,
                                              })
                          .OrderBy(x => x.numeration)
                          .ToListAsync();

                        //ingresando las de segundo nivel
                        const int SECOND_LEVEL = 2;
                        foreach (var defaultSection in defaultSections.Where(x => x.level == SECOND_LEVEL).ToList())
                        {
                            var parent = getParent(defaultSections, defaultSection.parentId);

                            var section = new SectionEntity
                            {
                                numeration = defaultSection.numeration,
                                name = defaultSection.name,
                                level = defaultSection.level,
                                parentId = getId(sections, parent.level, parent.name),
                                versionId = entity.versionId,
                                documentationId = entity.documentationId,
                                companyId = entity.companyId,
                                description = defaultSection.description,
                            };

                            section.creationDate = DateTime.UtcNow;
                            section.creationUserId = model.creationUserId;
                            await _databaseService.Section.AddAsync(section);
                            await _databaseService.SaveAsync();

                        }

                    }


                 




                    scope.Complete();

                }

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateVersionDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

        private SectionEntity getParent(List<SectionEntity> defaultSections, int idParent)
        {
            SectionEntity entity = new SectionEntity();
            foreach (var defaultSection in defaultSections)
                if (idParent == defaultSection.sectionId)
                    entity = defaultSection;

            return entity;
        }

        private int getId(List<SectionEntity> sections, int level, string name)
        {
            int idSection = 0;
            foreach (var section in sections)
                if (level == section.level && name.ToUpper() == section.name.ToUpper())
                    idSection = section.sectionId;

            return idSection;
        }

    }
}

