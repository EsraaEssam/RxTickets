using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxTickets.DataProvider
{
    public enum TicketSeverity
    {
        Low = 1,
        Normal = 0,
        High = 2,
        Critical = 3
    }

    public enum TicketCategory
    {
        Hardware = 0,
        Software = 1,
        Access = 2
    }

    public enum TicketStatus
    {
        Created = 0,
        InProcess = 1,
        Resolved = 2,
        Closed = 3
    }

    internal class Ticket
    {
        internal Ticket(int ticketId, TicketSeverity severity, TicketCategory category, string title, string author, DateTime createdDate, DateTime? resolvedDate, TicketStatus status)
        {
            TicketId = ticketId;
            Severity = severity;
            Category = category;
            Title = title;
            Author = author;
            CreatedDate = createdDate;
            ResolvedDate = resolvedDate;
            Status = status;
        }

        public int TicketId { get; set; } // TicketId (Primary key)
        public TicketSeverity Severity { get; set; } // Severity
        public TicketCategory Category { get; set; } // Category
        public string Title { get; set; } // Title
        public string Author { get; set; } // Author (length: 50)
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public System.DateTime? ResolvedDate { get; set; } // ResolvedDate
        public TicketStatus Status { get; set; } // Status
    }
}
