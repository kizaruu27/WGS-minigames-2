using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackScript : MonoBehaviour
{
    public float desiredCooldown;
    public float cooldown;
    public float rayDistance;
    public float rayHeight;
    public LayerMask enemyMask;
    public bool canAttack;
    public Button AttactButton;

    [Header("Particle Effect Component")]
    [SerializeField] GameObject hitParticle;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();

    }
    private void Start() =>
        AttactButton = GameObject.FindGameObjectWithTag("AttactButton").GetComponent<Button>();

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            cooldown = 0;
            canAttack = true;
        }

        if (Input.GetMouseButtonDown(0) && cooldown <= 0 && !CheckPlatform.isIos && !CheckPlatform.isAndroid && !CheckPlatform.isMobile)
        {
            PlayerAttack();
        }

        if (CheckPlatform.isIos || CheckPlatform.isAndroid || CheckPlatform.isMobile)
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

    public void PlayerAttack()
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
        GetComponent<PlayerControllerV2>().enabled = false;


        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y - rayHeight, transform.position.z), transform.TransformDirection(Vector3.forward * rayDistance));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, enemyMask))
        {

            if (!hit.transform.GetComponent<ShieldHandler>().shieldActivated)
            {
                yield return new WaitForSeconds(.5f);
                Debug.Log("Hit Player: " + hit.collider.gameObject.name);
                // Debug.Log("Hit Player");
                // hit.transform.GetComponent<WaffleHandler>().DecreaseWaffle();

                // ! attact another player
                if (hit.collider.TryGetComponent(out IDemageable otherPlayer))
                    Instantiate(hitParticle, hit.transform.position, Quaternion.identity);
                    otherPlayer.GotAttact();
            }
            else
            {
                Debug.Log("Shielded");
                hit.transform.GetComponent<ShieldHandler>().shieldActivated = false;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y - rayHeight, transform.position.z), transform.TransformDirection(Vector3.forward * rayDistance));
    }

    void ActivateController()
    {
        GetComponent<PlayerControllerV2>().enabled = true;
    }
}
