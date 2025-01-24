using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class ControllTest : MonoBehaviour
{
    //------------------------------------------//オブジェクト格納関連
    [SerializeField]
    string[] TagResarchFor;
    [SerializeField]
    Collider2D[] AddList;
    [SerializeField]
    List<Collider2D> InObject;

    //-----------------------------------------//Player変数
    GameObject Player;
    Collider2D PlayerCollder;
    Rigidbody2D rb_Player;
    Vector2 pos;
    Animator anim;
    //-----------------------------------------//当たり判定の方向を取得する変数の一覧
    [SerializeField]
    List<Vector2> DirectionList;
    Vector2 Direction;
    float direction_x;
    float direction_y;
    public float Float_y;
    //-----------------------------------------//入力・bool関連

    bool input_A, input_S, input_D, input_Space;
    bool Jump;

    //-----------------------------------------//制御関連
    [SerializeField]
    float moveSpeed = 3f, jumpPower = 150f, MaxvarticalSpeed = 4.7f, MaxSpeed = 2f;

    //-----------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        anim = this.GetComponent<Animator>();
        rb_Player = Player.GetComponent<Rigidbody2D>();
        PlayerCollder = Player.GetComponent<Collider2D>();
        InObject = new List<Collider2D>();
    }

    //-----------------------------------------------------------------------------------------------------------//
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            DirectionList[i] = Vector2.zero;
        }
        JumpFlagController(PlayerCollder,AddList,TagResarchFor);
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
        if (Input.GetKeyDown(KeyCode.Space)&&Jump)
        {
            input_Space = true;
        }
        if (DirectionList[4].y == -1)
        {
            Jump = true;
            anim.SetBool("JumpAnim", false);
        }
        else
        {
            Jump = false;
        }
    }

    private void FixedUpdate()
    {
        if (!input_A && !input_D && !input_S)
        {
            anim.SetBool("DownAnim", false);
            anim.SetBool("MoveAnim", false);
            rb_Player.velocity = new Vector2(0, rb_Player.velocity.y);
            pos = Player.transform.position;
        }
        if (input_S)
        {
            anim.SetBool("DownAnim", true);
            rb_Player.velocity = new Vector2(0, rb_Player.velocity.y);
            pos = Player.transform.position;
        }
        if (input_A)
        {
            anim.SetBool("DownAnim", false);
            anim.SetBool("MoveAnim", true);
            Player.transform.localScale = new Vector3(-1, 1);
            rb_Player.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            pos = Player.transform.position;
        }
        if (input_D)
        {
            anim.SetBool("DownAnim", false);
            anim.SetBool("MoveAnim", true);
            Player.transform.localScale = new Vector3(1, 1);
            rb_Player.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            pos = Player.transform.position;
        }
        if (input_Space)
        {
            anim.SetBool("JumpAnim", true);
            rb_Player.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            pos = Player.transform.position;
        }
        ReturnSetting(rb_Player, Player.transform.position);
    }

    //-----------------------------------------------------------------------------------------------------------//ジャンプ処理関数まとめ

    void JumpFlagController(Collider2D Hit, Collider2D[] AddObject, params string[] List)
    {
        int count = Hit.OverlapCollider(new ContactFilter2D(), AddObject);
        //Debug.Log("AddObject || "+AddObject.Length+ " ||");
        if (count != 0)//当たっているオブジェクトが0でなかったら
        {
            for (int i = 0; i < count; i++)
            {
                if (TargetTagResarch(AddObject[i],TagResarchFor) )
                {
                    ObjectCount(Hit,AddObject,AddObject[i] ,InObject);
                }
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------//対応したオブジェクトを配列に入れる。
    void ObjectCount(Collider2D Hit , Collider2D[]AddObject,Collider2D AddObjList , List<Collider2D> ListIn)
    {
        int count = Hit.OverlapCollider(new ContactFilter2D(), AddObject);
        //Debug.Log("AddObject || " + AddObject.Length + " ||");
        if (count != 0)//当たっているオブジェクトが0でなかったら
        {
            for (int i = 0; i < count; i++)
            {
                if (InObject.Count > 4)
                {
                    InObject.RemoveAt(0);
                }
                if (TargetTagResarch(AddObject[i], TagResarchFor))
                {
                    //Debug.Log("AddOf");
                    InObject.Add(AddObjList);
                }
            }
            DirectionForce(InObject,DirectionList);
        }
    }

    void DirectionForce(List<Collider2D> Coll2d,List<Vector2>Direction)
    {
        for (int i = 0; i < Coll2d.Count; i++)
        {
            if (Coll2d[i] != null)
            {
                Vector2 pos = (Coll2d[i].ClosestPoint(transform.position) - (Vector2)transform.position).normalized;
                direction_x = pos.x;
                direction_y = pos.y;
                direction_x = Mathf.Ceil(direction_x * 100) / 100;
                direction_y = Mathf.Ceil(direction_y * 100) / 100;
                Vector2 vector = new Vector2(direction_x, direction_y);
                if (vector == new Vector2 (0.75f,0.68f))
                {
                    vector = Direction[0];
                }
                Direction[i] = vector;
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------//速度の処理、boolのリセットをする

    void ReturnSetting(Rigidbody2D rb, Vector2 position)
    {
        input_A = false;
        input_D = false;
        input_S = false;
        input_Space = false;
        SettingSpeed(rb,position);
    }
    //----------------------------------------------------------//
    private void SettingSpeed(Rigidbody2D rb ,Vector2 position)
    {
        /// 移動速度制限を適用する///
        float currentHorizontalSpeed = Mathf.Abs(rb.velocity.x);  // 水平方向の速度を取得
        if (currentHorizontalSpeed > MaxSpeed)
        {
            /// 最大速度を超えている場合、速度を制限する///
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * MaxSpeed, rb.velocity.y);
        }

        /// 垂直方向の速度制限を追加///
        if (Mathf.Abs(rb.velocity.y) > MaxvarticalSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * MaxvarticalSpeed);
        }
        ///基本の処理を終えた後に移動量の適用処理///
        position = pos;
    }


    //-----------------------------------------------------------------------------------------------------------//



    //-----------------------------------------------------------------------------------------------------------//

    public static bool TargetTagResarch(Collider2D collder2d, params string[] tags)
    {
        return tags.Any(tag => collder2d.CompareTag(tag));
    }

    //-----------------------------------------------------------------------------------------------------------//




}
