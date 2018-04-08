using INSSBOT.CrossCutting.IoC;
using SimpleInjector;

namespace INSSBOT.Services.ConsoleApp
{
    public static class SimpleInjectorInitializer
    {
        public static Container Initialize()
        {
            var container = new Container();

            InitializeContainer(container);

            container.Verify();

            return container;
        }

        private static void InitializeContainer(Container container)
        {
            BootStrapper.RegisterServices(container);
        }
    }
}
