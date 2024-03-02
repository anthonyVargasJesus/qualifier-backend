using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByStandardId
{
    internal class GetAllControlGroupsByStandardIdQuery : IGetAllControlGroupsByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllControlGroupsByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId)
        {
            try
            {
                var entities = await (from controlGroup in _databaseService.ControlGroup
                                      where ((controlGroup.isDeleted == null || controlGroup.isDeleted == false) && controlGroup.standardId == standardId)
                                      select new ControlGroupEntity
                                      {
                                          controlGroupId = controlGroup.controlGroupId,
                                          number = controlGroup.number,
                                          name = controlGroup.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllControlGroupsByStandardIdDto> baseResponseDto = new BaseResponseDto<GetAllControlGroupsByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllControlGroupsByStandardIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

