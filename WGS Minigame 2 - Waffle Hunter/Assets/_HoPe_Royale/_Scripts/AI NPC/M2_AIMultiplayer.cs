using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class M2_AIMultiplayer : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    public Transform[] waypoint;
    public LayerMask playerMask;
    public Animator anim;
    public float range;
    int currentWaypointIndex;
    public float currentWaitingTime;
    public float maxWaitingTime;
    private Rigidbody rb;
    Vector3 target;
    public float distance;
    public float attackRange, turnSpeed;
    bool playerInrange;
    float fireRate, nextFire;
    public M2_AIAttack m2AIAttack;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        anim = GetComponentInChildren<Animator>();

        UpdateWaypoint();
    }

    private void UpdateWaypoint()
    {
        currentWaitingTime = 0;
        maxWaitingTime = 0;
        currentWaypointIndex = -1;
        GoToNextPoint();
        playerInrange = false;
        fireRate = 1;
        nextFire = Time.time;
    }

    void Update()
    {
        UpdateTarget();

        if (playerInrange == false)
        {

            GoToNextPoint();
        }

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
            if (maxWaitingTime == 0)
            { maxWaitingTime = Random.Range(0, 2); }

            if (currentWaitingTime >= maxWaitingTime)
            {
                maxWaitingTime = 0;
                currentWaitingTime = 0;
                GoToNextPoint();
            }
            else
            { currentWaitingTime += Time.deltaTime; }
        }
    }

    void Attack()
    {
        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));

        if (Physics.Raycast(theRay, out RaycastHit hit, range, playerMask))
        {
            if (Time.time > nextFire)
            {
                anim.SetTrigger("Attack");
                nextFire = Time.time + fireRate;
            }
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void UpdateTarget()
    {

        currentWaitingTime = 0;
        maxWaitingTime = 0;

        GameObject[] NextPlayer = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        foreach (GameObject nextPlay in NextPlayer)
        {
            float distanceToTarget = Vector3.Distance(transform.position, nextPlay.transform.position);
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearestTarget = nextPlay;
            }
        }

        if (nearestTarget != null && shortestDistance <= distance)
        {
            player = nearestTarget.transform;
            agent.SetDestination(player.position);
            playerInrange = true;
            currentWaitingTime = 0;
            LockOnTarget();
        }
        else
        {
            player = null;
            CheckingTimer();
            if (currentWaitingTime == 0) anim.SetBool("NPCwalk", true);
            if (currentWaitingTime > 0) anim.SetBool("NPCwalk", false);
        }

        if (Vector3.Distance(transform.position, player.position) <= attackRange) agent.isStopped = true;
        else agent.isStopped = false;

    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

#endif
}
