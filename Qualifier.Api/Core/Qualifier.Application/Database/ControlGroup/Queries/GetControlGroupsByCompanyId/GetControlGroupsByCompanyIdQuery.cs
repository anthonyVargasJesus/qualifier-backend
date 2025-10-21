using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByCompanyId
{
    public class GetControlGroupsByCompanyIdQuery : IGetControlGroupsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlGroupsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from controlGroup in _databaseService.ControlGroup
                                      where ((controlGroup.isDeleted == null || controlGroup.isDeleted == false) && controlGroup.companyId == companyId)
                                      && (controlGroup.name.ToUpper().Contains(search.ToUpper()))
                                      select new ControlGroupEntity
                                      {
                                          controlGroupId = controlGroup.controlGroupId,
                                          number = controlGroup.number,
                                          name = controlGroup.name,
                                      }).OrderBy(x => x.number)
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetControlGroupsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetControlGroupsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetControlGroupsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from controlGroup in _databaseService.ControlGroup
                               where ((controlGroup.isDeleted == null || controlGroup.isDeleted == false) && controlGroup.companyId == companyId)
                               && (controlGroup.name.ToUpper().Contains(search.ToUpper()))
                               select new ControlGroupEntity
                               {
                                   controlGroupId = controlGroup.controlGroupId,
                               }).CountAsync();
            return total;
        }

    }
}

