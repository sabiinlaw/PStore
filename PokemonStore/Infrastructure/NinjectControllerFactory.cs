using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using PokemonStore.Domain.Entities;
using PokemonStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using Moq;
using PokemonStore.Domain.Concrete;

namespace PokemonStore.Infrastructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>();
            ninjectKernel.Bind<IPokemonRepository>().To<EFPokemonRepository>();
            ninjectKernel.Bind<ICrypt>().To<Crypt>();
          
        }
    }
    
}