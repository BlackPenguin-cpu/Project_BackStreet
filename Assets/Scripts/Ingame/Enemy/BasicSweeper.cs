using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSweeper : Enemy
{
    [SerializeField] GameObject Player;
    Rigidbody2D rigid;
    public float JumpCooldown;
    float JumpNowCooldown;
    public float JumpPower;
    public float AttackDistance;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Move();
    }
    void Move()
    {
        if (Mathf.Abs(Player.transform.position.x - this.gameObject.transform.position.x) < 1 && Player.transform.position.y > this.gameObject.transform.position.y)
        {
            Jump();
        }
        if (Player.transform.position.x > this.gameObject.transform.position.x)
        {
            rigid.AddForce(new Vector3(Speed, 0, 0), ForceMode2D.Impulse);
        }
        else if (Player.transform.position.x < this.gameObject.transform.position.x)
        {
            rigid.AddForce(new Vector3(-Speed, 0, 0), ForceMode2D.Impulse);
        }
        JumpNowCooldown += Time.deltaTime;
    }
    void Jump()
    {
        if (JumpNowCooldown > 0)
        {
            JumpNowCooldown = 0;
            rigid.AddForce(new Vector3(0, JumpPower, 0), ForceMode2D.Impulse);
        }

    }
    protected override void Attack()
    { 
        RaycastHit2D[] Hits;
        List<GameObject> Targets = null;

        Debug.DrawRay(gameObject.transform.position, new Vector3(Player.transform.position.x > gameObject.transform.position.x ? 1 : -1 * AttackDistance, 0, 0));
        Hits = Physics2D.RaycastAll(gameObject.transform.position, new Vector3(Player.transform.position.x > gameObject.transform.position.x ? 1 : -1, 0, 0), AttackDistance);

        int num = 0;
        foreach (RaycastHit2D hit in Hits)
        {
            Targets[num] = hit.collider.gameObject;
            num++;
        }

        if (Targets.Contains(Player))
        {
            Player playerComponent = Player.GetComponent<Player>();
            playerComponent._hp -= Damage;
        }

    }
    protected override void Die()
    {
        StartCoroutine(DieEvent());
    }
    IEnumerator DieEvent()
    {
        entityState = EntityState.DIE;
        Debug.Log(gameObject + "ÀÌ Á×À½");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    protected override void Hit()
    {
    }
}
