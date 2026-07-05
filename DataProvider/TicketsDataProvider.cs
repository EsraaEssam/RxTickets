using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxTickets.DataProvider
{
    internal class TicketsDataProvider
    {
        private List<Ticket> _tickets;

        internal TicketsDataProvider()
        {
            _tickets = new List<Ticket>()
            {
                new Ticket(1,TicketSeverity.Normal,TicketCategory.Access,"Failed to access service","John Doe",DateTime.Now,null,TicketStatus.Created),
                new Ticket(2,TicketSeverity.Normal,TicketCategory.Access,"Failed to access service","Jane Doe",DateTime.Now.AddDays(-5),null,TicketStatus.Closed),
                new Ticket(3,TicketSeverity.Normal,TicketCategory.Access,"Invalid user","John Doe",DateTime.Now.AddDays(-10),null,TicketStatus.Closed),
                new Ticket(4,TicketSeverity.Normal,TicketCategory.Access,"Invalid user","Jane Doe",DateTime.Now.AddDays(-10),null,TicketStatus.Created),
                new Ticket(5,TicketSeverity.Normal,TicketCategory.Access,"Network error","John Doe",DateTime.Now,null,TicketStatus.Created),
                new Ticket(5,TicketSeverity.Normal,TicketCategory.Access,"Network error","Jane Doe",DateTime.Now.AddDays(-10),null,TicketStatus.Closed)
            };

        }

        public async Task<List<Ticket>> GetTicketsFromServer()
        {
            await Task.Delay(1000);
            return _tickets;
        }

        public async Task<List<Ticket>> SearchTicketsFromServer(string searchText, bool includeClosed)
        {
            await Task.Delay(1000);
            
            var AllTicketsMatchingSearch = _tickets.Where(t => t.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase));
            return includeClosed ? AllTicketsMatchingSearch.ToList() : AllTicketsMatchingSearch.Where(t => t.Status != TicketStatus.Closed).ToList();
        }
        public async Task<DateTime> GetTimestamp()
        {
            await Task.Delay(1000);
            return _tickets.Max(t => t.CreatedDate);
        }
        
    }
}
