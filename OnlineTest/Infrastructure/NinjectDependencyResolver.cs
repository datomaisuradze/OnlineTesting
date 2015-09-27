//using Ninject;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace OnlineTest.Infrastructure
//{
//    public class NinjectDependencyResolver : IDependencyResolver
//    {
//        public IKernel kernel;

//        public NinjectDependencyResolver(IKernel kernel)
//        {
//            this.kernel = kernel;
//            AddBindings();
//        }

//        public object GetService(Type service)
//        {
//            return kernel.TryGet(service);
//        }

//        public IEnumerable<object> GetServices(Type service)
//        {
//            return kernel.GetAll(service);
//        }

//        public void AddBindings()
//        {
//            kernel.Bind<IQuestionService>().To<QuestionService>();
//            kernel.Bind<IUserService>().To<UserService>();
//        }
//    }
//}