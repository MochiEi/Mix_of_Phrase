using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Controller : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool move = false;   //自分が動いているかどうか(他のオブジェクトが参照するためpublic必須)
    public bool Rmove = false;  //自分の右のオブジェクトが動いているかどうか
    public bool Lmove = false;  //自分の左のオブジェクトが動いているかどうか
    public bool POXmove=false;  //POXが自分に触れているかどうか

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Rmove == true || Lmove == true || POXmove == true)      //誰かが自分を押してたら
        {
            move = true;                                            //自分は動いている
        }

        if (Rmove == false && Lmove == false && POXmove == false)   //誰も押してなかったら
        {
            move = false;                                           //自分は動いていない
        }

        if (move ==true)                                            //自分が動いてたら
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; //回転のみ固定
        }

        if(move ==false)                                            //自分が動いてなかったら
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;    //X軸の移動と回転を固定
        }
    }


    void OnCollisionExit2D(Collision2D collision)       //オブジェクトが離れたら
    {
        rb.velocity = new Vector2(0, rb.velocity.y);    //横移動(x)の値を0にする(これいる？)
    }


    private void OnTriggerExit2D(Collider2D collision)      //何らかのTriggerが離れたら
    {
        if (collision.gameObject.CompareTag("POX_Side"))    //POX_SideというTag持ちだったら
        {
            POXmove = false;                                //POXが自分を押していないよね
        }
    }


    private void OnTriggerStay2D(Collider2D collision)                                      //ぶつかっているTriggerのオブジェクト
    {
        if (collision.gameObject.CompareTag("POX_Side"))                                    //POX_Sideだったら
        {
            POXmove = true;                                                                 //POXが自分を押している
        }

        if (collision.gameObject.CompareTag("Box_Right"))                                   //Box_RightのTagを持ったTriggerだったら
        {
            var parentScript = collision.transform.parent.GetComponent<Box_Controller>();   //そのオブジェクトの親オブジェクトのスクリプトを取得
            bool moveother = parentScript.move;                                             //move変数を取得しmoveotherとして宣言
            Transform parentTransform = collision.transform.parent;                         //親オブジェクトのtransformを取得
            Rigidbody2D rbother = parentTransform.GetComponent<Rigidbody2D>();              //親オブジェクトのrigidbodyを取得しrbotherとして宣言
            Vector2 speedother = rbother.velocity;                                          //rbotherの速度情報を取得しspeedotherとして宣言

            if (moveother == true)                                                          //自分を押してたら
            {
                Rmove = true;                                                               //右のオブジェクトが自分を押しとるやん
            }
            if (speedother.x == 0)                                                          //自分を押してない(動いてない)だったら
            {
                Rmove = false;                                                              //右のオブジェクトが自分を押しとらんやんけ
            }
        }

        if (collision.gameObject.CompareTag("Box_Left"))                                    //Box_LeftのTagを持ったTriggerだったら
        {
            var parentScript = collision.transform.parent.GetComponent<Box_Controller>();   //そのオブジェクトの親オブジェクトのスクリプトを取得
            bool moveother = parentScript.move;                                             //move変数を取得しmoveotherとして宣言
            Transform parentTransform = collision.transform.parent;                         //親オブジェクトのtransformを取得
            Rigidbody2D rbother = parentTransform.GetComponent<Rigidbody2D>();              //親オブジェクトのrigidbodyを取得しrbotherとして宣言
            Vector2 speedother = rbother.velocity;                                          //rbotherの速度情報を取得しspeedotherとして宣言

            if (moveother == true)                                                          //自分を押してたら
            {
                Lmove = true;                                                               //左のオブジェクトが自分を押しとるやん
            }
            if (speedother.x == 0)                                                          //自分を押してない(動いてない)だったら
            {
                Lmove = false;                                                              //左のオブジェクトが自分を押しとらんやんけ
            }
        }
    }
}
