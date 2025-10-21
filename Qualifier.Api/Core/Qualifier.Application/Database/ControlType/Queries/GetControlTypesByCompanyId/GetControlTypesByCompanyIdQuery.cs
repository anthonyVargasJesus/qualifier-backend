using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlType.Queries.GetControlTypesByCompanyId
{
    public class GetControlTypesByCompanyIdQuery : IGetControlTypesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlTypesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from controlType in _databaseService.ControlType
                                      where ((controlType.isDeleted == null || controlType.isDeleted == false) && controlType.companyId == companyId)
                                      && (controlType.name.ToUpper().Contains(search.ToUpper()))
                                      select new ControlTypeEntity
                                      {
                                          controlTypeId = controlType.controlTypeId,
                                          name = controlType.name,
                                          description = controlType.description,
                                          abbreviation = controlType.abbreviation,
                                          minimum = controlType.minimum,
                                          maximum = controlType.maximum,
                                          color = controlType.color,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetControlTypesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetControlTypesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetControlTypesByCompanyIdDto>>(entities);
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
            var total = await (from controlType in _databaseService.ControlType
                               where ((controlType.isDeleted == null || controlType.isDeleted == false) && controlType.companyId == companyId)
                               && (controlType.name.ToUpper().Contains(search.ToUpper()))
                               select new ControlTypeEntity
                               {
                                   controlTypeId = controlType.controlTypeId,
                               }).CountAsync();
            return total;
        }

    }
}

