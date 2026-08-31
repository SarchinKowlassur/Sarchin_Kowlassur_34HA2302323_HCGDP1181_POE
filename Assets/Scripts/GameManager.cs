using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject selectedCreature;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSelectedCreature(GameObject selectedCreature)
    {
        if (selectedCreature != null)
        {
            this.selectedCreature = selectedCreature;
        }
        else
        {
            this.selectedCreature = null;
        }
        
    }

    public GameObject GetSelectedCreature()
    {
        return selectedCreature;
    }

    public void MoveSelectedCreature()
    {
        
    }
}
