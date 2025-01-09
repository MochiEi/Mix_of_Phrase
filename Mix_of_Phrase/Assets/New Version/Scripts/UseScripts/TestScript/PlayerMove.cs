using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMove : MonoBehaviour
{
    GameObject Player;
    Collider2D HitBox;
    [SerializeField]
    Collider2D[] Box = new Collider2D[5];

    Collider2D[] Ground = new Collider2D[1];

    [SerializeField]
    bool jumpFlag;

    [SerializeField]
    string[] Hittag;

    Animator anim;
    Rigidbody2D rb;
    [SerializeField] Vector2 pos;
    [SerializeField] float moveSpeed = 0, jumpPower = 0, MaxvarticalSpeed = 0, MaxSpeed = 0;///�ړ��֘A�A���x����
    [SerializeField] bool input_A = false, input_D = false, input_S = false, input_Space = false;///���͐���֘A

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Pox");
        HitBox = this.GetComponent<Collider2D>();
        anim = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input_A = false;
        input_D = false;
        input_S = false;
        JumpFlagment(HitBox,jumpFlag);
        if (Input.GetKey(KeyCode.S))
        {
            input_S = Input.GetKey(KeyCode.S);
        }

        if (Input.GetKey(KeyCode.A))
        {
            input_A = Input.GetKey(KeyCode.A);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            input_D = Input.GetKey(KeyCode.D);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
         input_Space = Input.GetKeyDown(KeyCode.Space);
        }
    }

    //---------------------------------------------------------------------------------------//�W�����v���ł��邩�ǂ����̏����A�O��̏����ɔ�ׂ�bool��6�����čs�����߂����������B
    void JumpFlagment(Collider2D HitBox,bool Jump)
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
