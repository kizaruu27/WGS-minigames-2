using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class M2_NpcController : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] GameObject P_Target = null;
    public float timeToStopPursuit;
    protected float m_TimerSinceLostTarget = 0.0f;
    public M2_TargetScanner playerScanner;
    public GameObject[] waypoint;
    public int randomPointer;
    public M2_TargetHandler playerM2Targets;
    [SerializeField] private float fireRate;
    private float nextFire = 0.0f;
    PhotonView view;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            view = GetComponent<PhotonView>();

            // player target on scene 
            playerM2Targets = FindObjectOfType<M2_TargetHandler>();

            // get way poin
            waypoint = GameObject.FindGameObjectsWithTag("NpcWayPoint");

            // run Attact animation duration
            fireRate = anim.runtimeAnimatorController.animationClips[3].length;
        }
    }

    private void FixedUpdate()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // try to ditect player
            FindTarget();

            // monobehaviour controller
            NpcMonobehaviour(P_Target);
        }
    }

    public void NpcMonobehaviour(GameObject playerTarget)
    {
        if (playerTarget != null)
        {
            // pursuit all player
            StartPursuit(playerTarget.transform.position);
            agent.autoBraking = true;
        }
        else
        {
            // start roaming
            StartCoroutine(NPCRoaming());
            agent.autoBraking = false;
        }
    }

    public void FindTarget()
    {
        foreach (var player in playerM2Targets.Players)
        {
            GameObject target = playerScanner.DetectPlayer(transform, player);

            Debug.Log("catch player: " + target);

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
            agent.SetDestination(waypoint[randomPointer].transform.position);
            anim.SetBool("NPCwalk", true);

            if (Vector3.Distance(this.transform.position, waypoint[randomPointer].transform.position) <= 3f)
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
        }

        if (checkPosition <= playerScanner.attackRange)
            AttactTarget();
    }

    void LockOnTarget(Vector3 player)
    {
        Vector3 dir = player - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, 2f * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void AttactTarget()
    {
        if (Time.time > nextFire)
        {
            anim.SetTrigger("Attack");
            anim.SetBool("NPCwalk", false);

            nextFire = Time.time + fireRate;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        playerScanner.EditorGizmo(transform);
    }
#endif
}
