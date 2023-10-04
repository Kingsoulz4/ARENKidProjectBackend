using System.Diagnostics;

public class AssetBuilder
{
    public static void ImportAssetAndBuildAssetBundle()
    {
        Process? proc = null;
        try
        {
            string batDir = string.Format("./");
            proc = new Process();
            proc.StartInfo.WorkingDirectory = batDir;
            proc.StartInfo.FileName = "CommandBuildAssetBundle.bat";
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();
            proc.WaitForExit();
            Console.WriteLine("Bat file executed !!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace!.ToString());
        }
    }
}