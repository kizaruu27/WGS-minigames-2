using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    public AIMultiplayer aiMultipayer;
    [SerializeField] float rayHeight, rayDistance;
    public LayerMask playerMask;

    public void AttackInPlayer()
    {
        print("Attack Player");

        Ray ray = new Ray (new Vector3(transform.position.x, transform.position.y - rayHeight, transform.position.z), transform.TransformDirection(Vector3.forward * rayDistance));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, playerMask))
        {
            if (!hit.transform.GetComponent<ShieldHandler>().shieldActivated)
            {
                hit.transform.GetComponent<WaffleHandler>().DecreaseWaffle();
            }
            else
            {
                hit.transform.GetComponent<ShieldHandler>().shieldActivated = false;
            }
        }


    }

    public void OutAttackRange(){
        aiMultipayer.anim.SetBool("NPCwalk", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(new Ray (new Vector3(transform.position.x, transform.position.y - rayHeight, transform.position.z), transform.TransformDirection(Vector3.forward * rayDistance)));
    }
}
