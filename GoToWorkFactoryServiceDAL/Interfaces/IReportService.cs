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
        void getMaterialDeficitReport(ReportBindingModel model);

        void getOrderListReport(ReportBindingModel model);
    }
}
