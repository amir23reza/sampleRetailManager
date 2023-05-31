using Caliburn.Micro;
using DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        /*
            DI (Dependancy Injection): When a class, A, needs the functionality of another class, B; we should avoid instanciating the class B within the class A.
            Rather, we should pass the class B from outside of class A (in other words, inject it)

            DIC (Dependancy Injection Container): A class which is responsible to provide instanciation and configuration of objects to other classes. 

            Singleton Design Pattern: it uses a single instance of a class to enable global acess to its members.

            Caliburn comes with a built-in DIC, simple Container.
         */
        private SimpleContainer container_ = new SimpleContainer();
        public Bootstrapper() { 
            Initialize();
        }

        protected override void Configure()
        {
            // base.Configure();
            container_.Instance(container_);

            /**
             * Whenever a WindowManager Interface is requested from another class, a single instance which is created in the configure method and is only created 
             * on the start up of the app is going to be passed to the caller class. The same is true for EventAggregator Interface.
             */

            container_.Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            /**
             * The block of code below is firstly filtering all the classes that their names end with ViewModel, so basically all out view models.
             * Secondly, all the view models are creating a list and each of them will be registered so that a new instance is created on every request.
             * will be explained further ...
             */
            GetType().Assembly.GetTypes()
                .Where(t => t.IsClass)
                .Where(t => t.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(ViewModelType => container_.RegisterPerRequest(ViewModelType, ViewModelType.ToString(), ViewModelType));

            /**
             * a custom class for demonstration and testing will be registered into the container below.
             * The interface and the class implementing it will be passed to PerRequest so that each time another class depends on it, a new instance would be created 
             * by the container.
             */
            container_.PerRequest<ICalculations, Calculations>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container_.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container_.GetAllInstances(service);
        }
    }
}
