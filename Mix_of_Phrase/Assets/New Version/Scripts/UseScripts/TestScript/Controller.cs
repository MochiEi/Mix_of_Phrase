using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Controller : MonoBehaviour
{
    GameObject player;
    Collider2D hitBox;
    Animator anim;
    Rigidbody2D rb_Pllyer;
    Collider2D[] Box = new Collider2D[5];
    [SerializeField]
    Collider2D[] Ground = new Collider2D[2];
    [SerializeField]
    string[] _SettingTag;
    bool input_A, input_S, input_D, input_Space;///���͂̊m�F
    bool Jump;
    [SerializeField]
    float moveSpeed = 0, jumpPower = 0, MaxvarticalSpeed = 0, MaxSpeed = 0;///�ړ��֘A�A���x����
    Vector2 pos;
    [SerializeField]
    Vector2 direction;
    Vector2[] directionVec = new Vector2[2];

    [SerializeField]
    float DirectionSet;

    [SerializeField]
    float DirectionX;
    [SerializeField]
    float DirectionY;


    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        hitBox = player.GetComponent<Collider2D>();
        rb_Pllyer = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
        pos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            input_A = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input_D = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input_S = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            input_Space = true;
        }
        int Count = hitBox.OverlapCollider(new ContactFilter2D(), Ground);
        if (Count > 0 && Jump)
        {
            Jump = false;
            anim.SetBool("JumpAnim", false);
        }
    }

    private void FixedUpdate()
    {
       
        if (!input_A && !input_D && !input_S)
        {
            anim.SetBool("DownAnim", false);
            anim.SetBool("MoveAnim", false);
            rb_Pllyer.velocity = new Vector2(0, rb_Pllyer.velocity.y);
            pos = player.transform.position;
        }
        if (input_S)
        {
            anim.SetBool("DownAnim", true);
            rb_Pllyer.velocity = new Vector2(0, rb_Pllyer.velocity.y);
            pos = player.transform.position;
        }
        if (input_A)
        {
            anim.SetBool("DownAnim", false);
            anim.SetBool("MoveAnim", true);
            player.transform.localScale = new Vector3(-1, 1);
            rb_Pllyer.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            pos = player.transform.position;
        }
        if (input_D)
        {
            anim.SetBool("DownAnim", false);
            anim.SetBool("MoveAnim", true);
            player.transform.localScale = new Vector3(1, 1);
            rb_Pllyer.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            pos = player.transform.position;
        }
        if (input_Space)
        {
            Jumping();
        }
        ReturnSetting();
    }

    //----------------------------------------------------------//


    /// <summary>
    ///�ϐ�1��Collder2D�̓����蔻���String[]�Ŋm�F�ł���
    /// </summary>
    /// <param name="Coll"></param>
    /// <param name="ListTag"></param>
    /// <returns></returns>
    bool HitFlagment(Collider2D Coll, params string[] ListTag)
    {     
        int HitCount = hitBox.OverlapCollider(new ContactFilter2D(), Ground);
        if (HitCount > 1)
        {
            for (int i = 0; i < HitCount; i++)
            {
                directionVec[i] = (Ground[i].ClosestPoint(transform.position) - (Vector2)transform.position).normalized;
            }

            direction = new Vector2(Mathf.Ceil(directionVec[0].x * 100)/100, Mathf.Ceil(directionVec[1].y * 100)/100);
            if (direction.x >= DirectionX && direction.y <= DirectionY)
            {
                return true;
            }
        }
        else
        {
            direction = (Ground[0].ClosestPoint(transform.position) - (Vector2)transform.position).normalized;
            direction.x = Mathf.Ceil(direction.x * 100) / 100;
        }
        // y������-1�ȉ��A����x������-2���傫���ꍇ�̂ݐi��
        if (direction.x > -2.0f && direction.y <= -DirectionSet)
        {
            if (HitCount > 0)
            {
                foreach (Collider2D Col in Ground)
                {
                    if (TargetTagResarch(Col, ListTag))
                    {
                        Jump = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return false;
    }
    


    //----------------------------------------------------------//
    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }
    //----------------------------------------------------------//
    void Jumping()
    {
        if (HitFlagment(hitBox, _SettingTag))
        {
            anim.SetBool("JumpAnim", true);
            rb_Pllyer.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            pos = player.transform.position;
        }
    }
    //----------------------------------------------------------//

    /// <summary>
    /// bool�̃��Z�b�g�ƍ��W�X�V���s�������̊֐�
    /// </summary>
    void ReturnSetting()
    {
        input_A = false;
        input_D = false;
        input_S = false;
        input_Space = false;
        SettingSpeed();
    }
    //----------------------------------------------------------//
    private void SettingSpeed()
    {
        /// �ړ����x������K�p����///
        float currentHorizontalSpeed = Mathf.Abs(rb_Pllyer.velocity.x);  // ���������̑��x���擾
        if (currentHorizontalSpeed > MaxSpeed)
        {
            /// �ő呬�x�𒴂��Ă���ꍇ�A���x�𐧌�����///
            rb_Pllyer.velocity = new Vector2(Mathf.Sign(rb_Pllyer.velocity.x) * MaxSpeed, rb_Pllyer.velocity.y);
        }

        /// ���������̑��x������ǉ�///
        if (Mathf.Abs(rb_Pllyer.velocity.y) > MaxvarticalSpeed)
        {
            rb_Pllyer.velocity = new Vector2(rb_Pllyer.velocity.x, Mathf.Sign(rb_Pllyer.velocity.y) * MaxvarticalSpeed);
        }
        ///��{�̏������I������Ɉړ��ʂ̓K�p����///
        player.transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("JumpAnim",0.25f);
    }

    void JumpAnim() { anim.SetBool("JumpAnim", false); }

}
