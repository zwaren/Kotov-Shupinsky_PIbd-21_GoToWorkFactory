using GoToWorkFactoryServiceDAL.BindingModels;

namespace GoToWorkFactoryServiceDAL.Interfaces
{
    public interface IReportService
    {
        void createMaterialRequest(ReportBindingModel model);

        void getAdminOrderList(ReportBindingModel model);

        void getClentOrderList(ReportBindingModel model, int clientId);
    }
}
