using GoToWorkFactoryServiceDAL.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkFactoryServiceDAL.Interfaces
{
    public interface IReportService
    {
        void createMaterialRequest(ReportBindingModel model);

        void getAdminOrderList(ReportBindingModel model);

        void getClentOrderList(ReportBindingModel model);
    }
}
