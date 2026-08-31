using System.Collections;
using UnityEngine;

public class Clinic : Room
{
    [SerializeField] private Trait trait;
   

    private void Update()
    {
        HealCreature();
    }

    public void HealCreature()
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
                child.GetComponent<Health>().Heal();
            }
        }
    }

    public override string ToString()
    {
        return trait.Description;
    }
}
