using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public Transform playerTarget;
    NavMeshAgent agent;
    public Transform[] waypoint;
    public LayerMask playerMask;
    public Animator anim;
    public float distanceScanner;
    public float attackRange;
    bool playerInrange;
    [SerializeField] int randomPointer;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        if (playerInrange == false)
        {
            NPCRoaming();
        }

    }

    void NPCRoaming()
    {
        if (waypoint.Length != 0)
        {
            agent.SetDestination(waypoint[randomPointer].position);
            anim.SetBool("NPCwalk", true);

            if (Vector3.Distance(this.transform.position, waypoint[randomPointer].position) <= 3)
            {
                StartCoroutine(WayPoint());
            }
        }
    }

    IEnumerator WayPoint()
    {
        randomPointer = Random.Range(0, waypoint.Length);
        yield return null;
        // Debug.Log("change pointer");
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceScanner);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
}
