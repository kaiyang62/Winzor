using Microsoft.Extensions.Logging;

namespace Winzor;

public class VolumeService : IDisposable
{
    private readonly ILogger<VolumeService> _logger;
    private IntPtr _volumeHandle;
    private bool _disposed = false;

    public VolumeService(ILogger<VolumeService> logger)
    {
        _logger = logger;
        _volumeHandle = VolumeControlWrapper.CreateVolumeControl();
        if (_volumeHandle == IntPtr.Zero)
        {
            _logger.LogError("Failed to initialize volume control");
            throw new InvalidOperationException("Volume control initialization failed");
        }
        _logger.LogInformation("Volume control initialized successfully");
    }

    public float GetVolume()
    {
        ThrowIfDisposed();
        try
        {
            return VolumeControlWrapper.GetVolume(_volumeHandle) * 100;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting volume");
            return 0;
        }
    }

    public bool IsMuted()
    {
        ThrowIfDisposed();
        try
        {
            return VolumeControlWrapper.IsMuted(_volumeHandle);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking mute status");
            return false;
        }
    }

    public void SetVolume(float volume)
    {
        ThrowIfDisposed();
        try
        {
            VolumeControlWrapper.SetVolume(_volumeHandle, volume);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting volume to {Volume}", volume);
        }
    }

    public void ToggleMute()
    {
        ThrowIfDisposed();
        try
        {
            VolumeControlWrapper.ToggleMute(_volumeHandle);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error toggling mute");
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(VolumeService));
        }
    }

    public void Dispose()
    {
        if (!_disposed && _volumeHandle != IntPtr.Zero)
        {
            VolumeControlWrapper.DestroyVolumeControl(_volumeHandle);
            _volumeHandle = IntPtr.Zero;
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }

    ~VolumeService()
    {
        Dispose();
    }
}