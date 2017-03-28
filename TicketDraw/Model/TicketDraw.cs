using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CenterSpace.Free;

namespace TicketDraw.Model
{
    public class TicketDrawer
    {
        private List<int> _tickets;
        private MersenneTwister randGen = new MersenneTwister();

        public List<int> Tickets
        {
            get { return _tickets; }
            set { _tickets = value; }
        }

        public int DrawNext()
        {
            if (_tickets == null || _tickets.Count < 1)
                return -1;
            return _tickets[randGen.Next(_tickets.Count - 1)];
        }
    }
}
