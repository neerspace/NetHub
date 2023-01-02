namespace NetHub.Core.Enums;

public enum DeviceStatus
{
    None,

    /// <summary>
    /// Banned due to strange activity,
    /// or because it's one from the following list:
    /// 127.0.0.1, 0.0.0.0 etc...
    /// </summary>
    Banned,
}