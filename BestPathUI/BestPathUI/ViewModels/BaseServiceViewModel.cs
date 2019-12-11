using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace ViewModels
{
    public abstract class BaseServiceViewModel<T, IServiceInterface> : BaseViewModel
        where T : class, IBaseModel, new()
        where IServiceInterface : class, IRestDataService<T>
    {
        protected IServiceInterface CurrentService;
        public BaseServiceViewModel(IServiceInterface service)
        {
            this.CurrentService = service;
        }
    }
}
