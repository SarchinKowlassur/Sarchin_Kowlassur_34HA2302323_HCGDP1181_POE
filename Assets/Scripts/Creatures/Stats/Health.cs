using System.Collections;
using UnityEngine;

public class Health : Stat
{
    private bool delay = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Health(Trait trait) : base(trait)
    {
        
    }

    public void Heal()
    {
        if (delay)
        {
            return;
        }

        StartCoroutine(HealHealth());
    }

    public void Damage()
    {
        trait.Value--;

        if (trait.Value <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject,5);
    }

    public IEnumerator HealHealth()
    {
        delay = true;
        yield return new WaitForSeconds(2f);
         trait.Value++;
         if (trait.Value > trait.MaxValue)
         {
             trait.Value = trait.MaxValue;
         }
         delay = false;
    }

}
