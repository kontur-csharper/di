using Autofac;

namespace HomeExercise
{
    public interface IConsoleCloudClient
    {
        public void HandleSettingsFromConsole(string[] args, ContainerBuilder builder);
    }
}