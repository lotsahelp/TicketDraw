using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TicketDraw.Model;

namespace TicketDraw.ViewModel
{
    public class Presenter : ObservableObject
    {
        private readonly TicketDrawer _ticketDrawer = new TicketDrawer();
        private int? _lowestTicket, _highestTicket;
        private ObservableCollection<int> _possibleTickets = new ObservableCollection<int>();
        private ObservableCollection<int> _drawnTickets = new ObservableCollection<int>();
        private ObservableCollection<int> _remainingTickets = new ObservableCollection<int>();
        private readonly ObservableCollection<string> _history = new ObservableCollection<string>();

        private string _errorMsg;

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = ErrorMsg;
                RaisePropertyChangedEvent("ErrorMsg");
                RaisePropertyChangedEvent("ErrorMsgVisible");
            }
        }

        public Visibility ErrorMsgVisible
        {
            get
            {
                if (string.IsNullOrEmpty(_errorMsg))
                    return Visibility.Collapsed;
                return Visibility.Visible;
            }
        }

        public int? LowestTicket
        {
            get { return _lowestTicket; }
            set
            {
                _lowestTicket = value;
                RaisePropertyChangedEvent("LowestTicket");
            }
        }

        public int? HighestTicket
        {
            get { return _highestTicket; }
            set
            {
                _highestTicket = value;
                RaisePropertyChangedEvent("HighestTicket");
            }
        }

        public ObservableCollection<int> PossibleTickets
        {
            get { return _possibleTickets; }
            set
            {
                _possibleTickets = value;
                RaisePropertyChangedEvent("PossibleTickets");
            }
        }

        public ObservableCollection<int> DrawnTickets
        {
            get { return _drawnTickets; }
            set
            {
                _drawnTickets = value;
                RaisePropertyChangedEvent("DrawnTickets");
            }
        }

        public ObservableCollection<int> RemainingTickets
        {
            get { return _remainingTickets; }
            set
            {
                _remainingTickets = value;
                RaisePropertyChangedEvent("RemainingTickets");
            }
        }

        public IEnumerable<string> History
        {
            get { return _history; }
        }

        public ICommand ConvertTextCommand
        {
            get { return new DelegateCommand(DrawNext); }
        }

        public ICommand ResetCommand
        {
            get { return new DelegateCommand(Reset); }
        }

        private void DrawNext()
        {
            if (!(_lowestTicket.HasValue && _highestTicket.HasValue))
            {
                MessageBox.Show("Please enter a value for Lowest and Highest ticket.");
                return;
            }
            if (_lowestTicket > _highestTicket)
            {
                MessageBox.Show("Highest ticket should be higher than Lowest ticket.");
                return;
            }
            if (_drawnTickets.Count == 0)
            {
                for (int i = 0; i < (_highestTicket.Value + 1 - _lowestTicket.Value); i++)
                {
                    _possibleTickets.Add(_lowestTicket.Value + i);
                    _remainingTickets.Add(_lowestTicket.Value + i);
                }
            }
            _ticketDrawer.Tickets = new List<int>((IEnumerable<int>)_remainingTickets);
            var winner = _ticketDrawer.DrawNext();
            if (winner >= 0)
            {
                _remainingTickets.Remove(winner);
                _drawnTickets.Insert(0, winner);
            }
            else
            {
                MessageBox.Show("No more tickets.");
            }
        }

        private void Reset()
        {
            _possibleTickets.Clear();
            _drawnTickets.Clear();
            _remainingTickets.Clear();
            _lowestTicket = null;
            RaisePropertyChangedEvent("LowestTicket");
            _highestTicket = null;
            RaisePropertyChangedEvent("HighestTicket");
        }

        private void AddToHistory(string item)
        {
            //if (!_history.Contains(item))
            _history.Add(item);
        }
    }
}
