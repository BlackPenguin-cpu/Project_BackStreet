using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityState
{
    MOVING,
    ONDAMAGE,
    ATTACK,
    DIE
}

public abstract class Entity : MonoBehaviour
{
    public float Damage;
    public float MaxHp;
    public float Speed;
    [SerializeField] private float hp;
    public float _hp
    {
        get { return hp; }
        set
        {
            if (value >= MaxHp)
            {
                hp = MaxHp;
                return;
            }
            if (value <= 0)
            {
                entityState = EntityState.DIE;
                Die();
                hp = 0;
                return;
            }
            if (entityState == EntityState.ONDAMAGE)
            {
                return;
            }
            if (value < hp)
            {
                StartCoroutine(Onhit());
                Hit();
            }
            hp = value;

        }
    }

    public EntityState entityState;
    protected abstract void Die();
    protected abstract void Hit();
    //protected abstract void Attack();

    protected virtual void Start()
    {
        hp = MaxHp;
    }
    protected virtual void Update()
    {

    }
    protected virtual IEnumerator Onhit()
    {
        entityState = EntityState.ONDAMAGE;
        yield return new WaitForSeconds(0.5f);
        entityState = EntityState.MOVING;
    }

}
