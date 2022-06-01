using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    public ScriptableValue waffleValue;
    public AIMultiplayer aiMultipayer;

    public void AttackInPlayer(){
        waffleValue.value--;
        print("Attack Player");
    }

    public void OutAttackRange(){
        aiMultipayer.anim.SetBool("NPCwalk", true);
    }
}
