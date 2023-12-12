using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousDrag : MonoBehaviour
{
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    //オブジェクトをクリックしてドラッグ状態にある間呼び出される関数（Unityのマウスイベント）
    void OnMouseDrag()
    {
        //マウスカーソル及びオブジェクトのスクリーン座標を取得
        Vector3 objectScreenPoint =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        //スクリーン座標をワールド座標に変換
        Vector3 objectWorldPoint = Camera.main.ScreenToWorldPoint(objectScreenPoint);

        //オブジェクトの座標を変更する
        transform.position = objectWorldPoint;
    }
}
