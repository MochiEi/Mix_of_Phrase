using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startSystem : MonoBehaviour
{
    public GameObject startPutIn;
    public bool start = false;

    double time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = startPutIn.transform.position;
        Vector2 direction = Vector2.down;
        float maxDistance = 0.1f;
        int layerMask = 1 << LayerMask.NameToLayer("phrase");
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos, direction, maxDistance, layerMask);

        if (Input.GetMouseButtonUp(0))
        {
            foreach (RaycastHit2D hit in hits)
            {
                string name = hit.collider.gameObject.name;

                if (name == "start")
                {
                    //print("start");
                    start = true;
                }
            }
        }
        
        startFade startFade = this.GetComponent<startFade>();

        if (startFade.next)
        {
            time += 1 * Time.deltaTime;

            if(time > 1)
            {
                SceneManager.LoadScene("Select");
            }
        }
    }
}
