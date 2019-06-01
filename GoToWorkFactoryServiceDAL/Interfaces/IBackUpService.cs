using GoToWorkFactoryServiceDAL.BindingModels;

namespace GoToWorkFactoryServiceDAL.Interfaces
{
    public interface IBackUpService
    {
        void BackUpAdmin();
        void BackUpClent(ClientBindingModel client);
    }
}
