using RxTickets.Core;
using RxTickets.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RxTickets
{
    internal class MainWindowVM : BaseViewModel
    {
        public TicketsDashboardVM TicketsDashboardVM { get; set; }
        public DevOptionsVM DevOptionsVM { get; set; }

        internal MainWindowVM() 
        {
            TicketsDashboardVM = new TicketsDashboardVM();
            
            DevOptionsVM = new DevOptionsVM();

            if (IsInDesignMode())
            {
                
            }
        }
    }
}
