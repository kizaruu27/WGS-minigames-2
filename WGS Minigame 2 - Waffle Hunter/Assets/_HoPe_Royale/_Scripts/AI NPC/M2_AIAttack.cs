using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_AIAttack : MonoBehaviour
{
    public M2_AIMultiplayer m2AIMultipayer;
    [SerializeField] float rayHeight, rayDistance;
    public LayerMask playerMask;

    public void AttackInPlayer()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y - rayHeight, transform.position.z), transform.TransformDirection(Vector3.forward * rayDistance));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, playerMask))
        {
            if (!hit.transform.GetComponent<M2_ShieldHandler>().shieldActivated)
            {
                hit.transform.GetComponent<M2_WaffleHandler>().DecreaseWaffle();
                print("Attack Player");
            }
            else
            {
                hit.transform.GetComponent<M2_ShieldHandler>().shieldActivated = false;
                print("Player Shielded");
            }
        }
    }

    public void OutAttackRange()
    {
        m2AIMultipayer.anim.SetBool("NPCwalk", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(new Ray(new Vector3(transform.position.x, transform.position.y - rayHeight, transform.position.z), transform.TransformDirection(Vector3.forward * rayDistance)));
    }
}
