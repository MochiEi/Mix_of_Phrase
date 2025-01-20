using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllMaster : MonoBehaviour
{
    // Start is called before the first frame update

void Start()
    {

    }

    void Update()//Update　入力・アニメーション
    {
    }

    void Hit()//当たった時に判定を確認する関数。
    {
        //当たったオブジェクトの情報を取得し配列等に格納する
        //配列から確認して該当のオブジェクトのみListに移動する。
        //foreachでオブジェクトの状態を確認。
        //分岐前にTargetResarchの関数処理を使い判定した後分岐で分ける。
        //分岐のtrueにJumpFlagmentの関数処理を入れる。
        //Break
    }

    bool JumpFlgment()//ジャンプができる状態か確認する関数。
    {
        return true;
        //Hitで通過したオブジェクトを参照し、地面に明確に触れているか確認する。
        //Directionの数値制御によるジャンプの動作改善。
        //壁と床を認知しやすいように組みなおす。
    }

    bool TargetResarch()
    {
        return true;
        //以前から使っている関数を流用
    }

}
