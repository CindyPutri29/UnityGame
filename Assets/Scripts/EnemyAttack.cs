using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Weapon weapon;
    public void Hit()
    {
        weapon.MeleeAttack();
    }

}
