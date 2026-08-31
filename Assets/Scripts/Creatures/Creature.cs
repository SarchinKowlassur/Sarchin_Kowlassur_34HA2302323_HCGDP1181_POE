using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    [SerializeField] private string creatureName;
    [SerializeField]
    private List<Stat> stats;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = GetStat("Speed").GetTrait().Value;
    }

    void LateUpdate()
    {
        TickStats();
    }

    private void TickStats()
    {
        foreach (Stat stat in stats)
        {
            if (stat is ITick tick)
            {
                if (stat.GetTrait().Value >= 0)
                {
                    tick.Tick();
                    
                }
            }
        }
    }

    public string GetName()
    {
        return creatureName;
    }

    public Stat GetStat(string statName)
    {
        return stats.Find(stat => stat.GetTrait().Name == statName);
    }

    public void SubtractStat(string statName, float value)
    {
        Stat stat = GetStat(statName);
        if (stat != null)
        {
            stat.GetTrait().Value -= value;
        }

        if (stat.GetTrait().Value < 0)
        {
            stat.GetTrait().Value = 0;
        }
    }

    public void AddStat(string statName, float value)
    {
        Stat stat = GetStat(statName);
        if (stat != null)
        {
            stat.GetTrait().Value += value;
        }

        if (stat.GetTrait().Value > stat.GetTrait().MaxValue)
        {
            stat.GetTrait().Value = stat.GetTrait().MaxValue;
        }
    }

    public List<Stat> GetStats()
    {
        return stats;
    }
}
