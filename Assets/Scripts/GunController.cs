using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[AddComponentMenu("Gun Controller")]
public class GunController : MonoBehaviour
{

    private GameObject lastTarget;

    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAtCursor();
        TargetEnemy();
    }

    void LookAtCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            target = hit.point;
        }
        this.transform.LookAt(target);
    }

    void TargetEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.parent != null && hit.transform.parent.tag.Equals("Enemy"))
            {
                var child = hit.transform.parent.Find("HaloObject").gameObject;
                if(child != null)  
                {
                    if(child != lastTarget)
                    {
                        ActiveHaloLastTarget(false);
                        lastTarget = child;   
                    }
                
                    child.SetActive(true);
                }
                else
                {
                    ActiveHaloLastTarget(false);
                }
            }
            else
            {
                ActiveHaloLastTarget(false);
            }

        }

        void ActiveHaloLastTarget(bool active)
        {
            if (lastTarget != null)
            {
                lastTarget.SetActive(active);
            }
        }
    }
}
