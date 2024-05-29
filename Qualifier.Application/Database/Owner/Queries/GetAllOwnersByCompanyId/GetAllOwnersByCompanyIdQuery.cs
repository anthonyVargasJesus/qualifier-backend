using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Owner.Queries.GetAllOwnersByCompanyId
{
    internal class GetAllOwnersByCompanyIdQuery : IGetAllOwnersByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllOwnersByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from owner in _databaseService.Owner
                                      where ((owner.isDeleted == null || owner.isDeleted == false) && owner.companyId == companyId)
                                      select new OwnerEntity
                                      {
                                          ownerId = owner.ownerId,
                                          code = owner.code,
                                          name = owner.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllOwnersByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllOwnersByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllOwnersByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

