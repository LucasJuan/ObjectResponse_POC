using ObjectResponse_POC.Model;

namespace ObjectResponse_POC.Service;

public class HomeService
{
    private readonly Home _home;
    public HomeService(Home home) { 
    _home = home;
    }

    public Home GetHome()
    {
        _home.Title = "Test";
       _home.Name = "Lucas";
        
        return _home;
    }
}
