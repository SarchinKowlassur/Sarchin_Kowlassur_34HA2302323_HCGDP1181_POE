using System;
using UnityEngine;

public class WasteObject : MonoBehaviour
{
   public void OnTriggerEnter(Collider col)
   {
      if (col.gameObject.GetComponent<Creature>())
      {
         col.gameObject.GetComponent<Creature>().AddStat("Waste", 5f);
         col.gameObject.GetComponent<Creature>().SubtractStat("Happiness", 5f);
      }
   }
}
