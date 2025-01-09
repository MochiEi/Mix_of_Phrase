using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    ///���蓮��
    [SerializeField] bool t_Enter = false, t_Exit = false, t_Stay = false; ///�����蔻�萧��

    ///��������
    [SerializeField] bool frame_One = false, frame_Chche = false;///�t���[�����쐧��֘A
    [SerializeField] float moveSpeed = 0, jumpPower = 0, MaxvarticalSpeed = 0, MaxSpeed = 0;///�ړ��֘A�A���x����
    [SerializeField] bool input_A = false, input_D = false, input_S = false, input_Space = false;///���͐���֘A
    [SerializeField] Vector2 pos;

    ///�R���|�[�l���g�֘A
    Animator anim;
    Rigidbody2D rb;
    public GameObject Pox;

    //---------------------------------------------//

    Collider2D HitBox;

    Collider2D[] Box = new Collider2D[5];

    Collider2D[] Ground = new Collider2D[1];

    [SerializeField]
    bool jumpFlag;

    [SerializeField]
    string[] Hittag;
    //----------------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        HitBox = Pox.GetComponent<Collider2D>();
        anim = Pox.gameObject.GetComponent<Animator>();
        rb = Pox.gameObject.GetComponent<Rigidbody2D>();
        frame_One = true;
        pos = Pox.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos = Pox.transform.position;
        input_S = false;
        input_A = false;
        input_D = false;
        JumpFlagment(HitBox, jumpFlag);
        if (Input.GetKey(KeyCode.S))
        {
            input_S = true;
        }

        if (Input.GetKey(KeyCode.A) && !input_S)
        {
            input_A = true;
        }
        else if (Input.GetKey(KeyCode.D) && !input_S)
        {
            input_D = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpFlag)
        {
            input_Space = true;
        }

        MoveActions();
    }

    private void FixedUpdate()
    {
        if (!input_A && !input_D && !input_S)
        {
            anim.SetBool("DownAnim", false);
            anim.SetBool("MoveAnim", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (input_S)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (input_A)
        {
            if (rb.velocity.normalized.x > 0f)
                rb.velocity = new Vector2(0f, rb.velocity.y);

            Pox.transform.localScale = new Vector3(-1, 1);
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            pos = Pox.transform.position;
        }
        else if (input_D)
        {
            if (rb.velocity.normalized.x < 0f)
                rb.velocity = new Vector2(0f, rb.velocity.y);

            Pox.transform.localScale = new Vector3(1, 1);
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            pos = Pox.transform.position;
        }

        if (input_Space && jumpFlag)
        {
            jumpFlag = false;
            Invoke(nameof(Delay), 0.3f);
        }

        SettingSpeed();
        input_Space = false;
    }
    private void MoveActions()
    {
        if (jumpFlag)
        {
            if (input_S)
            {
                anim.SetBool("DownAnim", true);
            }
            else if (!input_S)
            {
                anim.SetBool("DownAnim", false);
            }

            if (input_A)
            {
                anim.SetBool("MoveAnim", true);
            }
            else if (input_D)
            {
                anim.SetBool("MoveAnim", true);
            }
            else anim.SetBool("MoveAnim", false);

            if (input_Space && jumpFlag)
            {
                anim.SetBool("JumpAnim", true);
            }
        }
        else if (t_Exit && jumpFlag)
        {
            anim.SetBool("JumpAnim", false);
        }
    }


    private void Delay()
    {

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        pos = this.transform.position;
        // Debug.Log(jump_flag);
    }

    private void SettingSpeed()
    {
        /// �ړ����x������K�p����///
        float currentHorizontalSpeed = Mathf.Abs(rb.velocity.x);  // ���������̑��x���擾
        if (currentHorizontalSpeed > MaxSpeed)
        {
            /// �ő呬�x�𒴂��Ă���ꍇ�A���x�𐧌�����///
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * MaxSpeed, rb.velocity.y);
        }

        /// ���������̑��x������ǉ�///
        if (Mathf.Abs(rb.velocity.y) > MaxvarticalSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * MaxvarticalSpeed);
        }
        ///��{�̏������I������Ɉړ��ʂ̓K�p����///
        Pox.transform.position = pos;
    }
    //---------------------------------------------------------------------------------------//�W�����v���ł��邩�ǂ����̏����A�O��̏����ɔ�ׂ�bool��6�����čs�����߂����������B
    void JumpFlagment(Collider2D HitBox, bool Jump)
    {

        int Count = HitBox.OverlapCollider(new ContactFilter2D(), Ground);
        Debug.Log(Count);
        if (Count > 0)
        {
            foreach (Collider2D Col in Ground)
            {
                if (TargetTagResarch(Col, Hittag))
                {
                    Jump = true;
                }
                else
                {
                    Jump = false;
                }
            }
        }
        else Jump = false;
    }
    //---------------------------------------------------------------------------------------//���������I�u�W�F�N�g�̓����蔻�肪�w�肵��LIST�ɓ����Ă��邩�ǂ����A�����ȏ����g���B
    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }
    //---------------------------------------------------------------------------------------//

}