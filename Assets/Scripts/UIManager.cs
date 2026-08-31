using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private TextMeshProUGUI displayName;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject warningPrefab;
    private GameObject currentlySelected;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance.GetSelectedCreature() != null)
        {
            displayPanel.SetActive(true);
        }
        else
        {
            displayPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DisplayInfo();
    }

    public void DisplayInfo()
    {
        if (currentlySelected != null)
        {
            if (currentlySelected.GetComponent<Creature>())
            {
                displayPanel.SetActive(true);
                displayName.text = currentlySelected.GetComponent<Creature>().GetName();
                string formattedInfo = "";
                List<Stat> selectedCreatureStats = currentlySelected.GetComponent<Creature>().GetStats();
                foreach (Stat stat in selectedCreatureStats)
                {
                    formattedInfo += stat.ToString();
                    formattedInfo += "\n";
                }
                displayText.text = formattedInfo;
            }
            else if (currentlySelected.GetComponent<Room>())
            {
                displayPanel.SetActive(true);
                displayName.text = currentlySelected.GetComponent<Room>().GetRoomName();
                string formattedInfo = "";
                displayText.text = currentlySelected.GetComponent<Room>().ToString();
            }
            else
            {
                displayPanel.SetActive(false);
                displayName.text = "";
                displayText.text = "";
                currentlySelected = null;
            }
            
            
        }
        else
        {
            displayPanel.SetActive(false);
        }
    }

    public void DisplayWarningMessage(string warningMessage)
    {
        GameObject warningGameObject = Instantiate(warningPrefab, warningPanel.transform);
        warningGameObject.transform.SetParent(warningPanel.transform);
        TextMeshProUGUI warningText = warningGameObject.GetComponentInChildren<TextMeshProUGUI>();
        warningText.text = warningMessage;
        Destroy(warningGameObject, 3f);
    }

    public void SetCurrentlySelected(GameObject selected)
    {
        currentlySelected = selected;
    }
}
