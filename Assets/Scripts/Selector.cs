using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    private InputSystem_Actions actions;
   
    public void OnDisable()
    {
        actions.Player.Disable();
    }

    private void Start()
    {
        actions = new InputSystem_Actions();
        actions.Player.Click.performed += OnClick;
        actions.Player.Enable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
       
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log(hit.transform.name);
            
            if (hit.transform.gameObject.GetComponent<Selectable>())
            {
                hit.transform.gameObject.GetComponent<Selectable>().SelectCreature();
                UIManager.Instance.SetCurrentlySelected(hit.transform.gameObject);
            }
            else if (hit.transform.gameObject.GetComponentInParent<Room>())
            {
                if (GameManager.Instance.GetSelectedCreature() != null)
                {
                    hit.transform.gameObject.GetComponentInParent<Room>().MoveCreatureToRoom();
                }
                UIManager.Instance.SetCurrentlySelected(hit.transform.parent.gameObject);
                
            }
            else if (hit.transform.gameObject.GetComponent<WasteObject>())
            {
                Debug.Log("Hit waste");
                Destroy(hit.transform.gameObject);
            }
           
        }
        else
        {
            GameManager.Instance.SetSelectedCreature(null);
            UIManager.Instance.SetCurrentlySelected(null);
            Debug.Log("Nothing Here");
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }
}
