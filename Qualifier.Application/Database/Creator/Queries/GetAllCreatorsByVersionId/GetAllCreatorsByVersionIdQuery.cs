using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Creator.Queries.GetAllCreatorsByVersionId
{
    public class GetAllCreatorsByVersionIdQuery : IGetAllCreatorsByVersionIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetAllCreatorsByVersionIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int versionId)
        {
            try
            {
                var entities = await (from creator in _databaseService.Creator
                                      join bt in _databaseService.Personal on creator.personalId equals bt.personalId into _bt
                                      from personal in _bt.DefaultIfEmpty()
                                      join bt2 in _databaseService.Responsible on creator.responsibleId equals bt2.responsibleId into _bt2
                                      from responsible in _bt2.DefaultIfEmpty()
                                      where ((creator.isDeleted == null || creator.isDeleted == false) && creator.versionId == versionId)
                                      select new CreatorEntity
                                      {
                                          creatorId = creator.creatorId,
                                          personalId = creator.personalId,
                                          responsibleId = creator.responsibleId,
                                          personal = (personal != null) ? new PersonalEntity { name = personal.name, firstName = personal.firstName, lastName = personal.lastName } : null,
                                          responsible = (responsible != null) ? new ResponsibleEntity { name = responsible.name} : null,
                                      })
                .ToListAsync();

                BaseResponseDto<GetAllCreatorsByVersionIdDto> baseResponseDto = new BaseResponseDto<GetAllCreatorsByVersionIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllCreatorsByVersionIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }



    }
}

