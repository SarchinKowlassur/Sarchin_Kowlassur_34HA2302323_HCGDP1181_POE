using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : Stat, ITick
{
    [SerializeField] private float tickRate;
    [SerializeField] private float warningThreshold;
    [SerializeField] private GameObject wastePrefab;
    private bool delayWarning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Waste(Trait trait) : base(trait)
    {
        
    }

    public void Tick()
    {
        trait.Value += tickRate * Time.deltaTime;
        if (trait.Value >= trait.MaxValue)
        {
            trait.Value = 0;
            Instantiate(wastePrefab, transform.position, Quaternion.identity);
        }
    }

    public bool CheckThreshold()
    {
        if (trait.Value > warningThreshold)
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
        delayWarning = !delayWarning;
    }
}
