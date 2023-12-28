using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class mousDrag : MonoBehaviour
{
    GameObject hitObject;
    Vector2 position;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        /////  RayÇÃê›íË  /////
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = Vector2.down;
        float maxDistance = 0.1f;
        int layerMask = 1 << LayerMask.NameToLayer("BuckCollider") | 1 << LayerMask.NameToLayer("phrase") | 1 << LayerMask.NameToLayer("Put In");

        RaycastHit2D phrase = default;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, direction, maxDistance, layerMask);

            foreach (RaycastHit2D hit in hits)
            {
                int layer = hit.collider.gameObject.layer;

                if (layer == LayerMask.NameToLayer("phrase"))
                {
                    phrase = hit;
                    break;
                }
            }

            if (phrase != default)
            {
                //Debug.Log("Hit phrase: " + phrase.collider.gameObject.name);
                hitObject = phrase.collider.gameObject;
            }
        }

        if (Input.GetMouseButton(0))
        {
            position.x = mousePosition.x;
            position.y = mousePosition.y;

            if (hitObject != null)
            {
                hitObject.transform.position = position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (hitObject != null)
            {
                bool PutInArea = false;

                phraseControl phraseControl = hitObject.GetComponent<phraseControl>();
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, direction, maxDistance, layerMask);

                ///// PutInÇÃíÜÇ»ÇÁ /////
                foreach (RaycastHit2D hit in hits)
                {
                    int layer = hit.collider.gameObject.layer;

                    if (layer == LayerMask.NameToLayer("Put In"))
                    {
                        hitObject.transform.position = hit.collider.gameObject.transform.position;
                        PutInArea = true;
                    }
                }

                ///// PutInÇÃäOÇ»ÇÁ /////
                if (!PutInArea)
                {
                    hitObject.transform.position = phraseControl.initPosition;
                }

                hitObject = null;
            }
        }
    }
}