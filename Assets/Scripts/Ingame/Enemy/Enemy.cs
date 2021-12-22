using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    public float AttackSpeed;
    public float AttackDelay;
    [SerializeField] bool isMelee;

    protected override void Die() { }
    protected override void Hit() { }

    protected abstract void Attack();

    void Start()
    {
        
    }
    protected virtual void Update()
    {
        if (AttackDelay > AttackSpeed)
        {
            Attack();
        }

        AttackDelay += Time.deltaTime;
    }
}
