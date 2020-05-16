using Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
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
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public LoginRequestDTO LoginModel { get; set; }
        [Inject]
        public IAuthenticationDataService AuthenticationDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        //[Inject]
        //public ISessionStorageDataService SessionStorage { get; set; }
        public LoginBase()
        {
            this.LoginModel = new LoginRequestDTO();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            //string Token = await SessionStorage.GetItemAsync<string>("Token");
            string localToken = await JSRuntime.InvokeAsync<string>("stateManager.load", "Token");
        }
        public async Task LoginUser()
        {
            var result = await AuthenticationDataService.Login(LoginModel);
            if (result.Successful)
            {
                //await SessionStorage.SetItemAsync("Token", result.Token);
                await JSRuntime.InvokeVoidAsync("stateManager.save", "Token", result.Token);
                NavigationManager.NavigateTo("/Map");
            }
        }
    }
}
