namespace StepsRecorderAdvanced.Core.Models.Interfaces;

public interface ISharedSettings
{
    public bool RecordClick { get; set; }
    public bool RecordScroll { get; set; }
    public DirectoryInfo TargetDirectory { get; set; }

    public ISharedSettings? CreateFromJSON(string json);
    public string WriteAsJSON();
}