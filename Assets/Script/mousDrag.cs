using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class mousDrag : MonoBehaviour
{
    public LayerMask PutIn;
    GameObject hitObject;
    bool click = false;
    Vector2 position;
    Vector2 fastPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスの位置をワールド座標に変換
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit)
            {
                print(hit.collider.tag);
            }

            if (hit.collider.tag == "phrase" && hitObject == null)
            {
                hitObject = hit.collider.gameObject;

                if (!click)
                {
                    fastPosition = hitObject.transform.position;
                    click = true;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            // マウスの位置をワールド座標に変換
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // hitObjectのxとyをマウスの位置に合わせる
            position.x = mousePosition.x;
            position.y = mousePosition.y;

            if (hitObject != null)
            {
                // hitObjectの位置を更新
                hitObject.transform.position = position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(hitObject != null)
            {
                hitObject.transform.position = fastPosition;
            }

            hitObject = null;
        }
    }

    private bool putin()
    {
        var hit = Physics2D.Raycast(hitObject.transform.position, Vector2.zero);

        if (hit.transform.tag == "PutIn") return true;

        return false;
    }
}