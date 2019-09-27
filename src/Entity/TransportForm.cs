using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class TransportForm
    {
        public TransportForm()
        {
            Tickets = new List<Ticket>();
        }
        public List<Ticket> Tickets
        {
            get;
            set;
        }

        public FinalcialInformation FinalcialInformation
        {
            get;
            set;
        }

        public Person Dispatcher
        {
            get;
            set;
        }

        public void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
        }
    }
}
