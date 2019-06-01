using GoToWorkFactoryServiceDAL.Interfaces;
using GoToWorkFactoryImplementDataBase;
using GoToWorkFactoryImplementDataBase.Implementations;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace GoToWorkFactoryClientView
{
    public class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<FactoryDbContext, FactoryDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientService, ClientServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialService, MaterialServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductService, ProductServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientMainService, ClientMainServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportService, ReportServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IBackUpService, BackUpServiceDB>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
