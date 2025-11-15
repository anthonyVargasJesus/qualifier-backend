using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.OptionInMenuInRole.Commands.DeleteOptionInMenuInRole
{
    public class DeleteOptionInMenuInRoleCommand : IDeleteOptionInMenuInRoleCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IOptionInMenuInRoleRepository _optionInMenuInRoleRepository;
        private readonly IMenuInRoleRepository _menuInRoleRepository;
        public DeleteOptionInMenuInRoleCommand(IDatabaseService databaseService, IOptionInMenuInRoleRepository optionInMenuInRoleRepository, IMenuInRoleRepository menuInRoleRepository)
        {
            _databaseService = databaseService;
            _optionInMenuInRoleRepository = optionInMenuInRoleRepository;
            this._menuInRoleRepository = menuInRoleRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);


                var entity = await (from item in _databaseService.OptionInMenuInRole
                                    where ((item.isDeleted == null || item.isDeleted == false)
                                    && item.optionInMenuInRoleId == id)
                                    select new OptionInMenuInRoleEntity()
                                    {
                                        optionInMenuInRoleId = item.optionInMenuInRoleId,
                                        menuId = item.menuId,
                                        roleId = item.roleId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                await _optionInMenuInRoleRepository.Delete(id, updateUserId);

                if (entity != null)
                {
                    List<OptionInMenuInRoleEntity> optionInMenuInRoles = (from item in _databaseService.OptionInMenuInRole
                                                                          where item.menuId == entity.menuId && item.roleId == entity.roleId 
                                                                          && (item.isDeleted == null || item.isDeleted == false)
                                                                          select new OptionInMenuInRoleEntity()
                                                                          {
                                                                              optionInMenuInRoleId = item.optionInMenuInRoleId,
                                                                          }).ToList();

                    if (optionInMenuInRoles.Count() == 0)
                    {
                        var menuInRole = (from item in _databaseService.MenuInRole
                                          where ((item.isDeleted == null || item.isDeleted == false)
                                          && item.menuId == entity.menuId && item.roleId == entity.roleId
                                          && item.companyId == entity.companyId)
                                          select new MenuInRoleEntity
                                          {
                                              menuInRoleId = item.menuInRoleId,
                                          }).FirstOrDefault();

                        if (menuInRole != null)
                        {
                            await _menuInRoleRepository.Delete(menuInRole.menuInRoleId, updateUserId);
                        }
                           



                    }

                }



   

                BaseResponseCommandDto baseResponseCommandDto = new BaseResponseCommandDto();
                baseResponseCommandDto.response = "¡Registro eliminado!";
                return baseResponseCommandDto;

            }
            catch (Exception ex)
            {
                throw ex;
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.OptionInMenuInRole.CountAsync(item => item.optionInMenuInRoleId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

