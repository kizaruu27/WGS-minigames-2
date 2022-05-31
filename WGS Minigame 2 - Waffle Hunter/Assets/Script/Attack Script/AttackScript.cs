using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{   
    public float desiredCooldown;
    public float cooldown;
    public float rayDistance;
    public float rayHeight;
    public LayerMask enemyMask;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            cooldown = 0;
        }

        if (Input.GetMouseButtonDown(0) && cooldown <= 0)
        {
            StartCoroutine(Attack());
        }

    }

    IEnumerator Attack()
    {
        Debug.Log("Attack");
        cooldown = desiredCooldown;
        _anim.SetTrigger("Attack");
        GetComponent<PlayerController>().enabled = false;


        Ray ray = new Ray (new Vector3(transform.position.x, transform.position.y - rayHeight, transform.position.z), transform.TransformDirection(Vector3.forward * rayDistance));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, enemyMask))
        {
            if (!hit.transform.GetComponent<ShieldHandler>().shieldActivated)
            {
                yield return new WaitForSeconds(.5f);
                Debug.Log("Hit Player");
                hit.transform.GetComponent<WaffleHandler>().DecreaseWaffle();
            }
            else
            {
                Debug.Log("Shielded");
                hit.transform.GetComponent<ShieldHandler>().shieldActivated = false;
            }
        }

        yield return new WaitForSeconds(1.1f);

        GetComponent<PlayerController>().enabled = true;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y - rayHeight, transform.position.z), transform.TransformDirection(Vector3.forward * rayDistance));
    }
}
