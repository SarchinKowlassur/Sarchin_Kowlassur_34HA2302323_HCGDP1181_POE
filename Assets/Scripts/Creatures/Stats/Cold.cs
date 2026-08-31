using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cold : Stat, ITick
{
    [SerializeField] private float tickRate;
    [SerializeField] private float warningThreshold;
    private bool delayDamage;
    private bool delayWarning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Cold(Trait trait) : base(trait)
    {
    }
    
    public void Tick()
    {
        trait.Value -= tickRate * Time.deltaTime;
        if (GetComponent<Health>() && trait.Value <= 0)
        {
            trait.Value = 0;
            if (delayDamage)
            {
                return;
            }
            StartCoroutine(DamageRoutine());
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

    public IEnumerator DamageRoutine()
    {
        delayDamage = true;
        yield return new WaitForSeconds(5);
        delayDamage = false;
        GetComponent<Health>().Damage();
    }
}
