using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousDrag : MonoBehaviour
{
    public GameObject hitObject;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // マウスの位置をワールド座標に変換
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider.tag == "phrase" && hitObject == null)
            {
                hitObject = hit.collider.gameObject;
            }

            // hitObjectのxとyをマウスの位置に合わせる
            position.x = mousePosition.x;
            position.y = mousePosition.y;

            // hitObjectの位置を更新
            hitObject.transform.position = position;
        }

        if(Input.GetMouseButtonUp(0))
        {
            hitObject = null;        
        }
    }
}
