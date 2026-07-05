using ReactiveUI;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace RxTickets.Core
{
    internal abstract class BaseViewModel : ReactiveObject
    {
        protected bool IsInDesignMode()
        {
            return System.ComponentModel.DesignerProperties
                .GetIsInDesignMode(new System.Windows.DependencyObject());
        }
    }
}
