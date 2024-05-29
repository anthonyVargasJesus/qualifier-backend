using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using System.Configuration;
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
                //using (TransactionScope scope = new TransactionScope())
                //{
                    Notification notification = this.createValidation(model);
                    if (notification.hasErrors())
                        return BaseApplication.getApplicationErrorResponse(notification.errors);

                    var entity = _mapper.Map<VersionEntity>(model);
                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    await _databaseService.Version.AddAsync(entity);
                    await _databaseService.SaveAsync();


                    var documentation = await (from item in _databaseService.Documentation
                                               where ((item.isDeleted == null || item.isDeleted == false) && item.documentationId == model.documentationId)
                                               select new DocumentationEntity()
                                               {
                                                   documentTypeId = item.documentTypeId,
                                               }).FirstOrDefaultAsync();


                    int documentTypeId = 0;
                    if (documentation != null)
                    {
                        documentTypeId = documentation.documentTypeId;
                        var defaultSections = await (from defaultSection in _databaseService.DefaultSection
                                              where ((defaultSection.isDeleted == null || defaultSection.isDeleted == false) && defaultSection.documentTypeId == documentTypeId)
                                              select new DefaultSectionEntity
                                              {
                                                  defaultSectionId = defaultSection.defaultSectionId,
                                                  numeration = defaultSection.numeration,
                                                  name = defaultSection.name,
                                                  level = defaultSection.level,
                                                  parentId = defaultSection.parentId,
                                                  description = defaultSection.description,
                                              }).ToListAsync();


                    //ingreso los de primer level
                    const int FIRST_LEVEL = 1;

                    foreach (var defaultSection in defaultSections.Where(x=>x.level == FIRST_LEVEL).ToList())
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

                //    scope.Complete();

                //}

                    return model;
            }
            catch (Exception EX)
            {
                throw EX;
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateVersionDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

        private DefaultSectionEntity getParent(List<DefaultSectionEntity> defaultSections, int idParent)
        {
            DefaultSectionEntity entity = new DefaultSectionEntity();
            foreach (var defaultSection in defaultSections)
                if (idParent == defaultSection.defaultSectionId)
                    entity = defaultSection;

            return entity;
        }

        private int getId(List<SectionEntity> sections,int level, string name)
        {
            int idSection = 0;
            foreach (var section in sections)
                if (level == section.level && name.ToUpper() == section.name.ToUpper())
                    idSection = section.sectionId;
            
            return idSection;
        }

    }
}

