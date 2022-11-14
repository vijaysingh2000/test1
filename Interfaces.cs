using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oms
{
    public interface IOrderTaskUserControl
    {
        void LoadControl(Guid orderId, string taskcode);
        void SaveControl();
        void RefreshControl();
    }

    public interface IReportUserControl
    {
        void LoadControl();
        void SaveControl();
        void RefreshControl();
        void ExportToExcel(string fileName);
        void UpdateExcel(string fileName);
    }

    public interface IListUserControl
    {
        void LoadControl(string tableName);
        void SaveControl();
        void RefreshControl();
    }
}
