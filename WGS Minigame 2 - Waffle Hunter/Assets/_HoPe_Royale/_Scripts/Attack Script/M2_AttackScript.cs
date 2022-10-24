using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M2_AttackScript : MonoBehaviour
{
    public float desiredCooldown;
    public float cooldown;
    public bool canAttack;
    public Button AttactButton;

    [Header("Particle Effect Component")]
    [SerializeField] GameObject hitParticle;

    [Header("Transition Value")]
    public float attackTransition;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();

    }
    private void Start()
    {
        AttactButton = GameObject.FindGameObjectWithTag("AttactButton").GetComponent<Button>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            cooldown = 0;
            canAttack = true;
        }
        AttactButton.onClick.AddListener(AttactForMobile);
    }

    public void AttactForMobile()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
            Invoke("ActivateController", 1.5f);
        }
    }

    IEnumerator Attack()
    {
        Debug.Log("Attack");
        cooldown = desiredCooldown;
        canAttack = false;
        _anim.SetTrigger("Attack");
        GetComponent<M2_PlayerControllerV2>().enabled = false;
        yield return new WaitForSeconds(attackTransition);
        ActivateController();
    }

    void ActivateController()
    {
        GetComponent<M2_PlayerControllerV2>().enabled = true;
    }
}
