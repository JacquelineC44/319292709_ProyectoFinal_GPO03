using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public items weaponActual;
    public items itemActual;
    public CapsuleCollider swordCollision;
    public LayerMask enemyMask;
    public float focusAtkImpulse;
    public float combo;
    public bool isAttacking;
    PlayerMotion playerMotion;
    //inventory inventory;
    Animator anim;
    Rigidbody rb;
    bool heavyAtk;
}