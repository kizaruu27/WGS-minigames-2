using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    public Transform[] waypoint;
    public LayerMask playerMask;
    public ScriptableValue waffleValue;
    public float range;
    public Animator anim;
    int currentWaypointIndex;
    public float currentWaitingTime;
    public float maxWaitingTime;
    private Rigidbody rb;
    Vector3 target;
    public float distance;
    bool playerInrange;
    float fireRate, nextFire;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();


        currentWaypointIndex = -1;
        currentWaitingTime = 0;
        maxWaitingTime = 0;
        GoToNextPoint();
        playerInrange = false;
        fireRate = 2;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < distance)
        {
            agent.SetDestination(player.position);
            playerInrange = true;
            currentWaitingTime = 0;
        }
        if (playerInrange = false)
        {

            GoToNextPoint();
        }

        if (currentWaitingTime == 0) anim.SetBool("NPCwalk", true);
        if (currentWaitingTime > 0) anim.SetBool("NPCwalk", false);

        CheckingTimer();
        Attack();

    }

    void GoToNextPoint()
    {
        int randomPointer = Random.Range(0, waypoint.Length);
        if (waypoint.Length != 0)
        {
            randomPointer = (randomPointer + 1) % waypoint.Length;
            agent.SetDestination(waypoint[randomPointer].position);

        }
    }

    void CheckingTimer()
    {
        if (agent.remainingDistance < 0.5f)
        {
            if (maxWaitingTime == 0) maxWaitingTime = Random.Range(0, 2);

            if (currentWaitingTime >= maxWaitingTime)
            {
                maxWaitingTime = 0;
                currentWaitingTime = 0;
                GoToNextPoint();
            }
            else currentWaitingTime += Time.deltaTime;
        }
    }

    void Attack()
    {
        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));

        if (Physics.Raycast(theRay, out RaycastHit hit, range, playerMask))
        {
            agent.isStopped = true;
            if (Time.time > nextFire)
            {
                // anim.SetTrigger("Attack");
                waffleValue.value--;
                nextFire = Time.time + fireRate;
                print("Attack Player");
            }
        }
        else { agent.isStopped = false; }

    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }

#endif
}
