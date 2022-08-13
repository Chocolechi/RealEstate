﻿using AutoMapper;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<List<UserViewModel>> GetAllVmAsync()
        {
            var users = await this.GetAllUsersAsync();
            var usersVm = _mapper.Map<List<UserViewModel>>(users);

            return usersVm;
        }
        public async Task<List<AuthenticationResponse>> GetAllUsersAsync()
        {
            List<AuthenticationResponse> users = await _accountService.GetAllUsers();
            return users;
        }
        public async Task<UserSaveViewModel> GetUserByIdAsync(string id)
        {
            AuthenticationResponse user = await _accountService.GetUserById(id);
            UserSaveViewModel userMap = _mapper.Map<UserSaveViewModel>(user);
            return userMap;
        }
        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest request = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse response = await _accountService.AuthenticationAsync(request);
            return response;
        }
        public async Task<AuthenticationResponse> RegisterAsync(UserSaveViewModel vm, string origin)
        {
            RegisterBasicRequest request = _mapper.Map<RegisterBasicRequest>(vm);
            return await _accountService.RegisterBasicUserAsync(request, origin);
        }

        public async Task<RegisterManagerResponse> RegisterAdminAsync(ManagerSaveViewModel vm)
        {
            RegisterManagerRequest request = _mapper.Map<RegisterManagerRequest>(vm);
            return await _accountService.RegisterAdminUserAsync(request);
        }

        public async Task<RegisterManagerResponse> RegisterDevAsync(ManagerSaveViewModel vm)
        {
            RegisterManagerRequest request = _mapper.Map<RegisterManagerRequest>(vm);
            return await _accountService.RegisterDevUserAsync(request);
        }

        public async Task<UpdateResponse> UpdateUserAsync(UserSaveViewModel vm, string id)
        {
            UpdateRequest req = _mapper.Map<UpdateRequest>(vm);
            return await _accountService.UpdateUserAsync(req, id);
        }

        public async Task<UpdateResponse> UpdateManagerUserAsync(ManagerSaveViewModel vm, string id)
        {
            UpdateRequest req = _mapper.Map<UpdateRequest>(vm);
            return await _accountService.UpdateUserAsync(req, id);
        }

        public async Task<UpdateResponse> ActivedUserAsync(string id)
        {
            return await _accountService.ActivedUserAsync(id);
        }
        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest request = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(request);
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();

        }

        //public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPassViewModel vm, string origin)
        //{
        //    ForgotPasswordRequest request = _mapper.Map<ForgotPasswordRequest>(vm);
        //    return await _accountService.ForgotPasswordAsync(request, origin);
        //}

        

        
    }
}
