using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Iss.LiveClassRoom.ServiceLayer.Services;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.DataAccessLayer;
using Iss.LiveClassRoom.Core.Repositories;
using Iss.LiveClassRoom.ServiceLayer;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using Iss.LiveClassRoom.FrontEnd.Models;

namespace Iss.LiveClassRoom.Ioc.App_Start
{

    public class SignalRUnityDependencyResolver : DefaultDependencyResolver {
        private IUnityContainer _container;

        public SignalRUnityDependencyResolver(IUnityContainer container) {
            _container = container;
        }

        public override object GetService(Type serviceType) {
            if (_container.IsRegistered(serviceType)) return _container.Resolve(serviceType);
            else return base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType) {
            if (_container.IsRegistered(serviceType)) return _container.ResolveAll(serviceType);
            else return base.GetServices(serviceType);
        }

    }

    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<SystemContext>(new PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());

            container.RegisterType(typeof(IRepository<>), typeof(GenericRepository<>));
            container.RegisterType(typeof(IService<>), typeof(Service<>));
            container.RegisterType<ICourseService, CourseService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ICommentService, CommentService>();
            container.RegisterType<IVideoService, VideoService>();
            container.RegisterType<ITopicService, TopicService>();
            container.RegisterType<IQuizService, QuizService>();
            container.RegisterType<IFeedService, FeedService>();
            container.RegisterType<IStudentService, StudentService>();
            container.RegisterType<QuizHub, QuizHub>(new ContainerControlledLifetimeManager());
            container.RegisterType<ChatHub, ChatHub>(new ContainerControlledLifetimeManager());
        }
    }
}
