using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    public ScriptableValue waffleValue;
    public AIMultiplayer aiMultipayer;
    WaffleHandler waffleHandler;

    private void Awake() 
    {
        waffleHandler = FindObjectOfType<WaffleHandler>();
    }

    public void AttackInPlayer()
    {
        print("Attack Player");
        waffleHandler.DecreaseWaffle();

    }

    public void OutAttackRange(){
        aiMultipayer.anim.SetBool("NPCwalk", true);
    }
}
