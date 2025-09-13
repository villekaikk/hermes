using ReactiveUI;

namespace Hermes.Application.ViewModels.Views;

public class ResponseInfoViewModel : ReactiveObject
{

    public string ResponseBody
    {
        get => _responseBody;
        set => this.RaiseAndSetIfChanged(ref _responseBody, value);
    }

    public string ResponseStatusCode
    {
        get => _responseStatusCode;
        set => this.RaiseAndSetIfChanged(ref _responseStatusCode, value);
    }
    
    public string ResponseContentSize
    {
        get => _responseContentSize;
        set => this.RaiseAndSetIfChanged(ref _responseContentSize, value);
    }
    
    public string RequestExecutionTime
    {
        get => _requestExecutionTime;
        set => this.RaiseAndSetIfChanged(ref _requestExecutionTime, value);
    }

    public bool RequestSucceed
    {
        get => _requestSucceed;
        set => this.RaiseAndSetIfChanged(ref _requestSucceed, value);
    }

    public ResponseInfoViewModel()
    {
        
    }

    private string _responseBody = "123asd";
    private string _responseStatusCode = "200 OK";
    private string _responseContentSize = "20 KB";
    private string _requestExecutionTime = "2.69s";
    private bool _requestSucceed = true;
}