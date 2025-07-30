using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Hermes.Application.ViewModels.Views;

namespace Hermes.UI.Views;

public partial class ResponseInfoView : ReactiveUserControl<ResponseInfoViewModel>
{
    public ResponseInfoView()
    {
        AvaloniaXamlLoader.Load(this);
    }
}