using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using StepsRecorderAdvanced.Core.Models.Interfaces;

namespace StepsRecorderAdvanced.Core.Models;

public class SharedSettings : ObservableObject, ISharedSettings
{
    #region member fields

    private bool recordClick;
    private bool recordScroll;
    private DirectoryInfo targetDirectory;

    #endregion

    #region constructor

    public SharedSettings(ISharedSettingReaderWriter<SharedSettings> sharedSettingReaderWriter)
    {
        var savedSettings = sharedSettingReaderWriter.Read("");
        
    }

    #endregion

    #region properties

    public bool RecordClick
    {
        get => recordClick;
        set => SetProperty(ref recordClick, value);
    }

    public bool RecordScroll
    {
        get => recordScroll;
        set => SetProperty(ref recordScroll, value);
    }

    [JsonConverter(typeof(DirectoryInfoJsonConverter))]
    public DirectoryInfo TargetDirectory
    {
        get => targetDirectory;
        set => SetProperty(ref targetDirectory, value);
    }

    #endregion

    #region interface methods

    public string ActiveSettings() => JsonSerializer.Serialize(this);
    
    #endregion
}