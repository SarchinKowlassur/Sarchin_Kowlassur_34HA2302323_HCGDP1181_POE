using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public abstract class Room : MonoBehaviour
{
    [SerializeField] protected string roomName;
    [SerializeField] protected Transform movePoint;
    [SerializeField] protected List<Transform> children = new List<Transform>();
    [SerializeField] protected int creatureCap;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Creature>())
            {
                children.Add(child);
            }
            
        }
    }

    public virtual void LateUpdate()
    {
        children = GetChildrenInRoom();
    }

    public virtual void MoveCreatureToRoom()
    {
        GameObject currentCreature = GameManager.Instance.GetSelectedCreature();
        currentCreature.transform.SetParent(this.gameObject.transform);
        currentCreature.GetComponent<NavMeshAgent>().ResetPath();
        currentCreature.GetComponent<NavMeshAgent>().Warp(movePoint.position);
    }
    
    public virtual List<Transform> GetChildrenInRoom()
    {
        List<Transform> creatures  = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Creature>())
            {
                creatures.Add(child);
            }
            
        }
        return creatures;
    }

    public virtual string GetRoomName()
    {
        return roomName;
    }

    public virtual string ToString()
    {
        return "This room does nothing";
    }

    public virtual int GetRoomCount()
    {
        return children.Count;
    }
    
}
