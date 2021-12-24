using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    LEFT,
    RIGHT
}
public class BasicSweeper : Enemy
{
    [SerializeField] GameObject Player;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    public float JumpCooldown;
    float JumpNowCooldown;
    Direction direction;
    public float JumpPower;
    public float AttackDistance;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        sprite.flipX = direction == 0 ? false : true;
        switch (entityState)
        {
            case EntityState.MOVING:
                Move();
                break;
            case EntityState.HIT:
                break;
            case EntityState.ATTACK:
                break;
            case EntityState.DIE:
                break;
            case EntityState.JUMP:
                break;
            default:
                break;
        }
    }
    void Move()
    {
        RaycastHit2D[] Hits;
        if (direction == Direction.RIGHT)
        {
            Debug.DrawRay(gameObject.transform.position - new Vector3(1, 0, 0), Vector3.down * 1);
            Hits = Physics2D.RaycastAll(gameObject.transform.position - new Vector3(1, 0, 0), Vector3.down, 1);
            foreach (RaycastHit2D ray in Hits)
            {
                if (ray.collider.gameObject.tag.Contains("Platform") && JumpNowCooldown > JumpCooldown) Jump();
            }
        }
        else if (direction == Direction.LEFT)
        {
            Debug.DrawRay(gameObject.transform.position - new Vector3(-1, 0, 0), Vector3.down * 1);
            Hits = Physics2D.RaycastAll(gameObject.transform.position - new Vector3(-1, 0, 0), Vector3.down, 1);
            foreach (RaycastHit2D ray in Hits)
            {
                if (ray.collider.gameObject.tag.Contains("Platform") && JumpNowCooldown > JumpCooldown) Jump();
            }
        }
        if (Mathf.Abs(Player.transform.position.x - this.gameObject.transform.position.x) < 1 && Player.transform.position.y > this.gameObject.transform.position.y)
        {
            if (JumpNowCooldown > JumpCooldown) Jump();
        }
        if (Player.transform.position.x > this.gameObject.transform.position.x)
        {
            rigid.AddForce(new Vector3(Speed * Time.deltaTime, 0, 0), ForceMode2D.Impulse);
            direction = Direction.RIGHT;
        }
        else if (Player.transform.position.x < this.gameObject.transform.position.x)
        {
            rigid.AddForce(new Vector3(-Speed * Time.deltaTime, 0, 0), ForceMode2D.Impulse);
            direction = Direction.LEFT;
        }
        JumpNowCooldown += Time.deltaTime;
    }
    void Jump()
    {
        JumpNowCooldown = 0;
        entityState = EntityState.JUMP;
        if (direction == Direction.RIGHT)
        {
            rigid.AddForce(new Vector3(Speed, JumpPower, 0), ForceMode2D.Impulse);
        }
        else if (direction == Direction.LEFT)
        {
            rigid.AddForce(new Vector3(-Speed, JumpPower, 0), ForceMode2D.Impulse);

        }
    }
    protected override void Attack()
    {
        RaycastHit2D[] Hits = null;
        List<GameObject> Targets = new List<GameObject>();

        Debug.DrawRay(gameObject.transform.position, new Vector3(Player.transform.position.x > gameObject.transform.position.x ? 1 : -1 * AttackDistance, 0, 0));
        Hits = Physics2D.RaycastAll(gameObject.transform.position, new Vector3(Player.transform.position.x > gameObject.transform.position.x ? 1 : -1, 0, 0), AttackDistance);

        foreach (RaycastHit2D hit in Hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                Debug.Log(Targets);
                Debug.Log(hit.collider.gameObject);
                Targets.Add(hit.collider.gameObject);
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Platform") && collision.gameObject.transform.position.y < gameObject.transform.position.y)
        {
            entityState = EntityState.MOVING;
        }
    }
}
