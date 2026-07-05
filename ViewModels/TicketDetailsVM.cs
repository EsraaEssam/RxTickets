using RxTickets.Core;
using RxTickets.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxTickets.ViewModels
{
    internal class TicketDetailsVM: BaseViewModel
    {
        private Ticket _ticket;
        public TicketDetailsVM(Ticket ticket)
        {
            _ticket = ticket;
            Title = _ticket.Title;
            Author = _ticket.Author;
            CreatedOn = _ticket.CreatedDate;

        }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public DateTime CreatedOn { get; private set; }
    }
}
