using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Generator : MonoBehaviour
{
    public GameObject Boxprefab;   //ここに生成したいアイテムとか入れよう(public変数1つにつき1つまで)

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && Directlyabove()) //  Enterキーを押したとき
        {
            Vector3 spawnPosition = transform.position + Vector3.up; // 真上の位置を計算
            Instantiate(Boxprefab, spawnPosition, Quaternion.identity); // プレハブを生成
        }
    }

    private bool Directlyabove()    //POX君の真上を計算
    {
        RaycastHit2D POXLup = Physics2D.Raycast(new Vector2(transform.position.x - 0.39f, transform.position.y + 0.55f), Vector2.up, 0.8f);
        RaycastHit2D POXRup = Physics2D.Raycast(new Vector2(transform.position.x + 0.39f, transform.position.y + 0.55f), Vector2.up, 0.8f);
        return POXLup.collider == null && POXRup.collider == null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector2(transform.position.x - 0.39f, transform.position.y + 0.55f), new Vector2(0, 0.8f));
        Gizmos.DrawRay(new Vector2(transform.position.x + 0.39f, transform.position.y + 0.55f), new Vector2(0, 0.8f));
    }

}
