using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureBrain : MonoBehaviour
{
    private static float PATROLRANGE = 5f;
        
    private Creature creature;
    private NavMeshAgent agent;
    private Animator anim;
    
    
    public enum CreatureStates {idle, walk, catch_attention, sleep, death}
    [SerializeField] private CreatureStates creatureStates;
    [SerializeField] private float idleTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        creature = this.gameObject.GetComponent<Creature>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        MonitorStats();
        StateHandler();
    }

    public void StateHandler()
    {
        switch (creatureStates)
        {
            case CreatureStates.idle:
                StartCoroutine(IdleState());
                anim.SetBool("isWalking", false);
                anim.SetBool("isSleeping", false);
                anim.SetBool("Catch_Attention", false);
                break;
            case CreatureStates.walk:
                CheckDistanceToDestination();
                anim.SetBool("isWalking", true);
                anim.SetBool("isSleeping", false);
                anim.SetBool("Catch_Attention", false);
                break;
            case CreatureStates.catch_attention:
                anim.SetBool("isWalking", false);
                anim.SetBool("isSleeping", false);
                anim.SetBool("Catch_Attention", true);
                break;
            case CreatureStates.sleep:
                anim.SetBool("isWalking", false);
                anim.SetBool("isSleeping", true);
                anim.SetBool("Catch_Attention", false);
                break;
            case CreatureStates.death:
                anim.SetBool("isWalking", false);
                anim.SetBool("isSleeping", false);
                anim.SetBool("Catch_Attention", false);
                anim.SetTrigger("Death");
                break;
            
        }
    }

    public void CheckDistanceToDestination()
    {
        if (!agent.hasPath)
        {
            return;
        }
        
        agent.isStopped = false;
        
        if (agent.remainingDistance <= 0.05f)
        {
            agent.ResetPath();
            creatureStates = CreatureStates.idle;
        }
    }

    public void MonitorStats()
    {
        List<Stat> stats = creature.GetStats();
        int i = 0;
        foreach (Stat stat in stats)
        {
            if (stat is ITick tick)
            {
                if (tick.CheckThreshold() && !tick.PendingWarning())
                {
                    creatureStates = CreatureStates.catch_attention;
                    UIManager.Instance.DisplayWarningMessage(stat.GetTrait().Description);
                    tick.SetWarningDelay();
                    i++;
                }
                
            }
        }

        if (i == 0 &&  creatureStates == CreatureStates.catch_attention)
        {
            creatureStates = CreatureStates.idle;
        }
        else if (i == stats.Count / 2)
        {
            creatureStates = CreatureStates.sleep;
        }

        if (creature.GetStat("Health"))
        {
            if (creature.GetStat("Health").GetTrait().Value <= 0)
            {
                creatureStates = CreatureStates.death;
            }
        }
    }

    public void SetRandomPosition()
    {
        Vector3 target;
        Vector3 randomDirection = transform.parent.transform.position + Random.insideUnitSphere * PATROLRANGE;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, PATROLRANGE, NavMesh.AllAreas))
        {
            target = hit.position;
            agent.SetDestination(target);
        }
    }

    public IEnumerator IdleState()
    {
        yield return new WaitForSeconds(idleTime);
        SetRandomPosition();
        creatureStates = CreatureStates.walk;
        
    }
}
