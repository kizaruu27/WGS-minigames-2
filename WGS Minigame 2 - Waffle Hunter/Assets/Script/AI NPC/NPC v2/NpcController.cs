using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] GameObject P_Target = null;
    public float timeToStopPursuit;
    protected float m_TimerSinceLostTarget = 0.0f;
    public TargetScanner playerScanner;
    public Transform[] waypoint;
    [SerializeField] int randomPointer;

    public List<GameObject> Players;

    void Start()
    {
        foreach (var Player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Players.Add(Player);
        }
    }

    private void FixedUpdate()
    {
        FindTarget();

        NpcMonobehaviour(P_Target);
    }

    public void NpcMonobehaviour(GameObject playerTarget)
    {
        if (playerTarget != null)
        {
            StartPursuit(playerTarget.transform.position);
            agent.autoBraking = true;
            agent.stoppingDistance = playerScanner.attackRange;
        }
        else
        {
            StartCoroutine(NPCRoaming());
            agent.autoBraking = false;
            agent.stoppingDistance = 2;
        }
    }

    public void FindTarget()
    {
        foreach (var player in Players)
        {
            GameObject target = playerScanner.DetectPlayer(transform, player);

            if (P_Target == null)
            {
                if (target != null)
                {
                    P_Target = target;
                }
            }
            else
            {
                if (target == null)
                {
                    m_TimerSinceLostTarget += Time.deltaTime;

                    if (m_TimerSinceLostTarget >= timeToStopPursuit)
                    {
                        Vector3 toTarget = P_Target.transform.position - transform.position;

                        if (toTarget.sqrMagnitude > playerScanner.detectionRadius * playerScanner.detectionRadius)
                        {
                            //the target move out of range, reset the target
                            P_Target = null;
                        }
                    }
                }
                else
                {
                    if (target != P_Target)
                    {
                        P_Target = target;
                    }

                    m_TimerSinceLostTarget = 0.0f;
                }
            }
        }
    }

    void GetWayPoint()
    {
        randomPointer = Random.Range(0, waypoint.Length);
    }

    IEnumerator NPCRoaming()
    {
        yield return new WaitForSeconds(.5f);

        if (waypoint.Length != 0)
        {
            agent.SetDestination(waypoint[randomPointer].position);
            anim.SetBool("NPCwalk", true);

            if (Vector3.Distance(this.transform.position, waypoint[randomPointer].position) <= 3f)
            {
                GetWayPoint();
            }
        }
    }

    public void StartPursuit(Vector3 playerTarget)
    {
        float checkPosition = Vector3.Distance(transform.position, playerTarget);

        if (checkPosition > playerScanner.attackRange)
        {
            agent.SetDestination(playerTarget);
            anim.SetBool("NPCwalk", true);
        }
        else
        {
            LockOnTarget(playerTarget);
            anim.SetBool("NPCwalk", false);
        }
    }

    void LockOnTarget(Vector3 player)
    {
        Vector3 dir = player - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, 4f * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        playerScanner.EditorGizmo(transform);
    }
#endif
}
