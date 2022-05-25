using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{   
    public float desiredCooldown;
    public float cooldown;

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

        yield return new WaitForSeconds(1.1f);

        GetComponent<PlayerController>().enabled = true;
        
    }
}
