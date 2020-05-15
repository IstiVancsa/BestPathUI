using Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Models.DTO.Authentication;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestPathUI.Pages.Authentication
{
    public class LoginBase : ComponentBase
    {
        public LoginRequestDTO LoginModel { get; set; }
        [Inject]
        public IAuthenticationDataService AuthenticationDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ISessionStorageDataService SessionStorage { get; set; }
        public LoginBase()
        {
            this.LoginModel = new LoginRequestDTO();
        }
        protected override async Task OnInitializedAsync()
        {
            string Token = await SessionStorage.GetItemAsync<string>("Token");
        }
        public async Task LoginUser()
        {
            var result = await AuthenticationDataService.Login(LoginModel);
            if (result.Successful)
            {
                await SessionStorage.SetItemAsync("Token", result.Token);
                NavigationManager.NavigateTo("/Map");
            }
        }
    }
}
