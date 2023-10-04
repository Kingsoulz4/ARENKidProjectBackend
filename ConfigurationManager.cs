public class ConfigurationManager
{
    private static ConfigurationManager? m_instance = null;
    public IConfiguration? Configuration {get; private set;}


    public static ConfigurationManager? Instance {
        get
        {
            return m_instance;
        }
        
    }

    public static ConfigurationManager Initialize(IConfiguration configuration)
    {
        m_instance = new();
        m_instance.Configuration = configuration;
        return m_instance;
    }

    public string GetUnityDataBuildAbsolutePath()
    {
        string path = "";
        if(Configuration != null)
        {
            path = Configuration!["UnityDataBuildAbsolutePath"]!;
        }
        return path;
    }

    public string GetUnityDataBuildRelativePath()
    {
        string path = "";
        if(Configuration != null)
        {
            path = Configuration!["UnityDataBuildRelativePath"]!;
        }
        return path;
    }
}