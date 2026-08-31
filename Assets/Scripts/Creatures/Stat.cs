using System;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
   [SerializeField] protected Trait trait;
   
   
   public Stat(Trait trait)
   {
      this.trait = trait;
   }

   public virtual Trait GetTrait()
   {
      return trait;
   }

   public virtual string ToString()
   {
      string formattedString = "";
      formattedString = trait.Name+ ": " + Mathf.Round(trait.Value) + "/" + trait.MaxValue;
      return formattedString;
   }
   
}
