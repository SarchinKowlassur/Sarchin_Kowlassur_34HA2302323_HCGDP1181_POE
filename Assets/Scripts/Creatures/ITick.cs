using UnityEngine;

public interface ITick
{
    public void Tick();

    public bool CheckThreshold();

    public void SetWarningDelay();
    
    public bool PendingWarning();
}
