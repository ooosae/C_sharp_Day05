namespace d05.Nasa;

public class NasaClient1 : INasaClient<string, int>
{
    private readonly string _apiKey;

    public NasaClient1(string apiKey)
    {
        _apiKey = apiKey;
    }

    public string GetApiKey() => _apiKey;
    
    public int GetAsync(string input)
    {
        return input.Length;
    }
}

public class NasaClient2 : INasaClient<int, bool>
{
    private readonly string _apiKey;

    public NasaClient2(string apiKey)
    {
        _apiKey = apiKey;
    }

    public string GetApiKey() => _apiKey;
    
    public bool GetAsync(int input)
    {
        return input > 0;
    }
}

public class NasaClient3 : INasaClient<double, string>
{
    private readonly string _apiKey;

    public NasaClient3(string apiKey)
    {
        _apiKey = apiKey;
    }

    public string GetApiKey() => _apiKey;
    
    public string GetAsync(double input)
    {
        return input.ToString("F2");
    }
}