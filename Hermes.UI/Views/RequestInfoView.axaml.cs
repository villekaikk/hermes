using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Hermes.Application.ViewModels.Views;

namespace Hermes.UI.Views;

public partial class RequestInfoView : ReactiveUserControl<RequestInfoViewModel>
{
    public RequestInfoView()
    {
        InitializeComponent();
    }
}