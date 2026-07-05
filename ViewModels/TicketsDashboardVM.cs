using DynamicData.Binding;
using ReactiveUI;
using RxTickets.Core;
using RxTickets.DataProvider;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;

namespace RxTickets.ViewModels
{
    internal class TicketsDashboardVM : BaseViewModel
    {

        private readonly TicketsDataProvider _ticketsDataProvider;

        private ObservableCollection<TicketDetailsVM> _tickets;
        public ObservableCollection<TicketDetailsVM> Tickets
        {
            get => _tickets;
            set => this.RaiseAndSetIfChanged(ref _tickets, value);
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => this.RaiseAndSetIfChanged(ref _searchQuery, value);
        }

        private bool _includeClosed;
        public bool IncludeClosed
        {
            get => _includeClosed;
            set => this.RaiseAndSetIfChanged(ref _includeClosed, value);
        }

        internal TicketsDashboardVM()
        {
            Tickets = new ObservableCollection<TicketDetailsVM>();
            _ticketsDataProvider = new TicketsDataProvider();

            var clearSearchSequence = this
                .WhenAnyValue(x => x.SearchQuery)
                .Where(query => string.IsNullOrEmpty(query))
                .Select(_ => string.Empty);

            clearSearchSequence
                .Timestamp()
                .Subscribe(x => Debug.WriteLine($"clearSearchSequence: {x.Value} emitted at {x.Timestamp.ToLocalTime()}"));

            var throttledSearchSequence = this
                .WhenAnyValue(x => x.SearchQuery)
                .Throttle(TimeSpan.FromMilliseconds(200))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                .Where(query => !string.IsNullOrWhiteSpace(query) && query.Length >= 2);

            throttledSearchSequence
                .Timestamp()
                .Subscribe(x => Debug.WriteLine($"throttledSearchSequence: {x.Value} emitted at {x.Timestamp.ToLocalTime()}"));

            var loadAllSequence = Observable.Return(DateTime.Now)
                .Concat(
                    Observable.Interval(TimeSpan.FromSeconds(5))
                    .SelectMany(t => _ticketsDataProvider.GetTimestamp())
                    .DistinctUntilChanged()
                )
                .ObserveOn(RxSchedulers.MainThreadScheduler)
                .Select(_ => SearchQuery);

            loadAllSequence
                .Timestamp() 
                .Subscribe(x => Debug.WriteLine($"loadAllSequence: {x.Value} emitted at {x.Timestamp.ToLocalTime()}"));

            var includeClosed = this.WhenValueChanged(x => x.IncludeClosed).Select(_ => IncludeClosed);

            var includeClosedSequence = Observable.Return(false)
               .Concat(includeClosed);

            var combinedSequence = throttledSearchSequence
                .Merge(loadAllSequence)
                .Merge(clearSearchSequence)
                .CombineLatest(includeClosedSequence,
                    (text, flag) =>
                        new Tuple<string, bool>(text, flag)
                );

            combinedSequence
                .Timestamp() 
                .Subscribe(x => Debug.WriteLine($"combinedSequence: {x.Value} emitted at {x.Timestamp.ToLocalTime()}"));

            var results = combinedSequence
                .SelectMany(t => string.IsNullOrEmpty( t.Item1)? _ticketsDataProvider.GetTicketsFromServer() : _ticketsDataProvider.SearchTicketsFromServer(t.Item1, t.Item2))
                .ObserveOn(RxSchedulers.MainThreadScheduler);

            results
                .Timestamp() 
                .Subscribe(x => Debug.WriteLine($"results: {x.Value} emitted at {x.Timestamp.ToLocalTime()}"));

            results.Subscribe(tickets =>
            {
                Tickets.Clear();
                foreach (var ticket in tickets)
                {
                    Tickets.Add(new TicketDetailsVM(ticket));
                }
            });

            if (IsInDesignMode())
            {

                Tickets.Add(new TicketDetailsVM(new Ticket(1, TicketSeverity.Normal, TicketCategory.Access, "Failed to access service", "John Doe", DateTime.Now, null, TicketStatus.Created)));
                Tickets.Add(new TicketDetailsVM(new Ticket(1, TicketSeverity.Normal, TicketCategory.Access, "Failed to access service", "John Doe", DateTime.Now, null, TicketStatus.Created)));
                Tickets.Add(new TicketDetailsVM(new Ticket(1, TicketSeverity.Normal, TicketCategory.Access, "Failed to access service", "John Doe", DateTime.Now, null, TicketStatus.Created)));
            }
        }

    }
}
