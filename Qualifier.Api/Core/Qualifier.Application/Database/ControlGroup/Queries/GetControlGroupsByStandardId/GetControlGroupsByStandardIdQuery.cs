using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByStandardId
{
    public class GetControlGroupsByStandardIdQuery : IGetControlGroupsByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlGroupsByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int standardId)
        {
            try
            {
                var entities = await (from controlGroup in _databaseService.ControlGroup
                                      where ((controlGroup.isDeleted == null || controlGroup.isDeleted == false) && controlGroup.standardId == standardId)
                                      && (controlGroup.name.ToUpper().Contains(search.ToUpper()))
                                      select new ControlGroupEntity
                                      {
                                          controlGroupId = controlGroup.controlGroupId,
                                          number = controlGroup.number,
                                          name = controlGroup.name,
                                          description = controlGroup.description
                                      }).OrderBy(x => x.number)
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetControlGroupsByStandardIdDto> baseResponseDto = new BaseResponseDto<GetControlGroupsByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetControlGroupsByStandardIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, standardId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int standardId)
        {
            var total = await (from controlGroup in _databaseService.ControlGroup
                               where ((controlGroup.isDeleted == null || controlGroup.isDeleted == false) && controlGroup.standardId == standardId)
                               && (controlGroup.name.ToUpper().Contains(search.ToUpper()))
                               select new ControlGroupEntity
                               {
                                   controlGroupId = controlGroup.controlGroupId,
                               }).CountAsync();
            return total;
        }

    }
}

