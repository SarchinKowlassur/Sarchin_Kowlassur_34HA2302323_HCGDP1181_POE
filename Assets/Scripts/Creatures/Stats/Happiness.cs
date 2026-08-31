using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Happiness : Stat, ITick
{
    [SerializeField] private float tickRate;
    [SerializeField] private float warningThreshold;
    private bool delayWarning;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Happiness(Trait trait) : base(trait)
    {
        
    }

    public void Tick()
    {
        trait.Value -= tickRate * Time.deltaTime;
        if (GetComponent<Health>() && trait.Value <= 0)
        {
            trait.Value += trait.MaxValue / 2;
            GetComponent<Health>().Damage();
        }
    }

    public bool CheckThreshold()
    {
        if (trait.Value < warningThreshold)
        {
            return true;
        }
        return false;
    }

    public void SetWarningDelay()
    {
        delayWarning = true;
        StartCoroutine(DelayWarning());
    }

    public bool PendingWarning()
    {
        return delayWarning;
    }

    public IEnumerator DelayWarning()
    {
        yield return new WaitForSeconds(3);
        delayWarning = false;
    }
}
