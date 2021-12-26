using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerAnimationState
{
    Idle,
    Move,
    Jump,
    Attack,

}


public class Player : MonoBehaviour
{
    public float Maxhp;
    public float Speed;
    public float JumpPower;
    public bool movable = true;
    public Npc npc;
    Animator anim;
    [SerializeField] private float Hp;

    Rigidbody2D rigid;

    public float _hp
    {
        get { return Hp; }
        set
        {
            if (value > Maxhp)
            {
                Hp = Maxhp;
            }
            else
            {
                Hp = value;
            }
        }
    }
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (movable)
        {
            Move();
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(new Vector3(Speed * Time.deltaTime, 0, 0), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(new Vector3(-Speed * Time.deltaTime, 0, 0), ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //이따 수정해주셈
            rigid.AddForce(new Vector3(0, JumpPower, 0), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.F))
        {
            npc.Action();
        }
    }
}
