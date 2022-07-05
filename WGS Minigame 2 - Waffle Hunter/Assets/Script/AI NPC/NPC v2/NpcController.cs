using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    public PlayerControllerV2 player { get { return P_Target; } }
    [SerializeField] protected PlayerControllerV2 P_Target = null;
    public TargetScanner playerScanner;
    public Transform[] waypoint;
    [SerializeField] int randomPointer;


    private void FixedUpdate()
    {
        FindTarget();
    }

    public void FindTarget()
    {
        PlayerControllerV2 target = playerScanner.DetectPlayer(transform, P_Target == null);

        Debug.Log(target);

        if (target != null)
        {
            P_Target = target;

            float checkPosition = Vector3.Distance(transform.position, target.transform.position);

            StartPursuit(target.transform.position);

            if (checkPosition < playerScanner.attackRange)
            {
                Debug.Log("Start Attact");
            }
        }
        else
        {
            StartCoroutine(NPCRoaming());
        }
    }

    void GetWayPoint()
    {
        randomPointer = Random.Range(0, waypoint.Length);
    }

    IEnumerator NPCRoaming()
    {
        yield return new WaitForSeconds(.1f);

        if (waypoint.Length != 0)
        {
            agent.SetDestination(waypoint[randomPointer].position);
            anim.SetBool("NPCwalk", true);

            if (Vector3.Distance(this.transform.position, waypoint[randomPointer].position) <= 3)
            {
                GetWayPoint();
            }
        }
    }

    public void StartPursuit(Vector3 playerTarget)
    {
        agent.SetDestination(playerTarget);
        anim.SetBool("NPCwalk", true);
    }


#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        playerScanner.EditorGizmo(transform);
    }
#endif
}
