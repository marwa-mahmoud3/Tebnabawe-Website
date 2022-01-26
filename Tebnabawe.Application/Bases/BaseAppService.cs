using AutoMapper;
using System;
using Tebnabawe.Bases.Interfaces;

namespace Tebnabawe.Application.Bases
{
    public class AppServiceBase : IDisposable
    {

        #region Vars
        protected IUnitOfWork TheUnitOfWork { get; set; }
        protected readonly IMapper Mapper; //MapperConfig.Mapper;
       
        #endregion

        #region CTR
        public AppServiceBase(IUnitOfWork theUnitOfWork, IMapper mapper)
        {
            TheUnitOfWork = theUnitOfWork;
            Mapper = mapper;
        }

        public void Dispose()
        {
            TheUnitOfWork.Dispose();
        }
        #endregion
    }
}
