namespace StepsRecorderAdvanced.Core.Models.Extensions;

public static class DirectoryInfoExtension
{
    public static bool HasReadAccess(this DirectoryInfo directoryInfo, bool throwIfFails = false)
    {
        try
        {
            directoryInfo.GetDirectories();//if GetDirectories works then it's accessible
            return true;
        }
        catch (Exception)
        {
            if (throwIfFails)
                throw;
            return false;
        }
    }

    public static bool HasWriteAccess(this DirectoryInfo directoryInfo, bool throwIfFails = false)
    {
        try
        {
            using var _ = File.Create(Path.Combine(directoryInfo.FullName, Path.GetRandomFileName()), 1,
                FileOptions.DeleteOnClose);
            return true;
        }
        catch
        {
            if (throwIfFails)
                throw;
            return false;
        }

    }
}