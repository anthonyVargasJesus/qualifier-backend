using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qualifier.Common.Api;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Common.Domain.Helpers;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
using System.Data;
using System.Runtime.InteropServices;


namespace Qualifier.Application.Database.User.Commands.Login
{
    public class LoginUserCommand : ILoginUserCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginUserCommand(IDatabaseService databaseService, IMapper mapper, ILoginService loginService, IConfiguration configuration)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _loginService = loginService;
            _configuration = configuration;
        }
        public async Task<Object> Execute(LoginUserLoginTryDto loginTryDto)
        {
            try
            {
                string noAccessTitle = "Acceso no autorizado";

                var entity = await (from user in _databaseService.User
                            join u2 in _databaseService.UserState on user.userState equals u2
                            join standard in _databaseService.Standard on user.standardId equals standard.standardId
                              where user.email == loginTryDto.email && (user.isDeleted == null || user.isDeleted == false)
                            select new UserEntity
                            {
                                userId = user.userId,
                                name = user.name,
                                firstName = user.firstName,
                                lastName = user.lastName == null ? null : user.lastName,
                                password = user.password,
                                email = user.email,
                                companyId = user.companyId,
                                standardId = user.standardId,
                                standard = new StandardEntity { standardId = standard.standardId, name = standard.name},
                                userState = new UserStateEntity { userStateId = u2.userStateId, name = u2.name, value = u2.value },
                            }).FirstOrDefaultAsync();

                Notification notification = this.loginValidation(loginTryDto, entity);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponseWithTitle(notification.errors, noAccessTitle);

                LoginEntity login =  this.UserToEntity(entity);

                login.password = loginTryDto.password;
                login.roles = await (from roleInUser in _databaseService.RoleInUser

                               join role in _databaseService.Role on roleInUser.role equals role
                               where roleInUser.userId == entity.userId && (roleInUser.isDeleted == null || roleInUser.isDeleted == false)
                               select new RoleEntity
                               {
                                   roleId = role.roleId,
                                   name = role.name,
                                   code = role.code
                               }).ToListAsync();

                //login.setMenus(login.roles);

                Notification domainNotification = _loginService.loginValidation(login, (entity.password == null)?"": entity.password);
                if (domainNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponseWithTitle(domainNotification.errors, noAccessTitle);

                string standardName = "";
                if (entity.standard != null)
                    standardName = entity.standard.name;

                login.token = JwtTokenProvider.GenerateToken(_configuration, login.userId, (login.name == null) ? "" : login.name , getCurrentRole(login.roles), getRolesArray(login.roles), entity.companyId, entity.standardId, standardName);

                return _mapper.Map<LoginUserLoginDto>(login);
            }
            catch (Exception ex)
            {
                throw ex;
             //return BaseApplication.getExceptionErrorResponse();
            }
        }



        public LoginEntity UserToEntity(UserEntity user)
        {
            if (user == null)
                return null;
            var login = new LoginEntity
            {
                userId = user.userId,
                name = user.name + " " + user.firstName,
                email = user.email,
                userState = user.userState,
                //roles = user.roles,
            };
            return login;
        }




        private string getCurrentRole(List<RoleEntity> roles)
        {
            string currentRole = "";
            int i = 0;
            foreach (RoleEntity role in roles)
            {
                if (i > 0)
                    currentRole += ", ";
                currentRole += role.name;
                i++;
            }

            return currentRole;
        }

        private List<int> getRolesArray(List<RoleEntity> roles)
        {
            List<int> rls = new List<int>();
            foreach (RoleEntity role in roles)
                rls.Add(role.roleId);

            return rls;
        }

        private Notification loginValidation(LoginUserLoginTryDto loginTryDto, UserEntity user)
        {
            Notification notification = new Notification();
            loginRequiredValidation(notification, loginTryDto);
            existsValidation(notification, user);
            return notification;
        }

        private void existsValidation(Notification notification, UserEntity user)
        {
            if (user == null)
                notification.addError("El correo no se encuentra registrado");
        }

        private void loginRequiredValidation(Notification notification, LoginUserLoginTryDto loginDto)
        {
            loginDto.requiredValidation(notification);
        }

    }
}
