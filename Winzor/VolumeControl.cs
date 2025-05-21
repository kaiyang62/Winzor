using System.Runtime.InteropServices;

namespace Winzor;

public class VolumeControlWrapper
{
    [DllImport("VolumeControl.dll")]
    public static extern IntPtr CreateVolumeControl();
    
    [DllImport("VolumeControl.dll")]
    public static extern void DestroyVolumeControl(IntPtr handle);
    
    [DllImport("VolumeControl.dll")]
    public static extern float GetVolume(IntPtr handle);
    
    [DllImport("VolumeControl.dll")]
    public static extern bool IsMuted(IntPtr handle);
    
    [DllImport("VolumeControl.dll")]
    public static extern void SetVolume(IntPtr handle, float volume);
    
    [DllImport("VolumeControl.dll")]
    public static extern void Mute(IntPtr handle);
    
    [DllImport("VolumeControl.dll")]
    public static extern void Unmute(IntPtr handle);
    
    [DllImport("VolumeControl.dll")]
    public static extern void ToggleMute(IntPtr handle);
}
