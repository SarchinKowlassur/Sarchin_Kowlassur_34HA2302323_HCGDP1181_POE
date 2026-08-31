using System;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] private GameObject selectIndicator;
    
    private void Update()
    {
        SelectedUI();
    }

    public void SelectCreature()
    {
        GameManager.Instance.SetSelectedCreature(this.gameObject);
    }

    private void SelectedUI()
    {
        if (GameManager.Instance.GetSelectedCreature() == this.gameObject)
        {
            selectIndicator.SetActive(true);
        }
        else
        {
            selectIndicator.SetActive(false);
        }
    }
    
}
