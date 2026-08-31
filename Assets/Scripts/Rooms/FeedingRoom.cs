using System;
using UnityEngine;

public class FeedingRoom : Room
{
    [SerializeField] private Trait trait;
    
    private void Update()
    {
        FeedCreatures();
    }

    private void FeedCreatures()
    {
        if (GetRoomCount() > creatureCap)
        {
            int excessCreatures = GetRoomCount() - creatureCap;
            Debug.LogFormat("The {0} is beyond its capacity, remove {1} creatures", this.gameObject.name, excessCreatures);
            return;
        }

        foreach (Transform child in children)
        {
            if (child.GetComponent<Creature>().GetStat(trait.Name) != null)
            {
                child.GetComponent<Creature>().AddStat(trait.Name, trait.Value * Time.deltaTime);
            }
        }
    }
    
    public override string ToString()
    {
        return trait.Description;
    }
}
