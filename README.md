# RxTickets
This solution includes a WPF application that shows how reactive programming can be used to search and filter data.

This follows the code presented in the [Taming Asynchronous .NET Code 
with Rx](https://www.pluralsight.com/courses/dotnet-code-rx-taming-asynchronous) course on [pluralsight](https://www.pluralsight.com/) while incorporating the [ReactiveUI](https://github.com/reactiveui/ReactiveUI) library to facilitate creating observables in view models.

The TicketsDashboardVM class combines the text entered by the user in the search bar and shows only the tickets with a title containing the text entered by the user, while taking into consideration whethter the user wants to view closed ticket or not.

[ApplicationUIExample](./Assets/SearchExample.png)
