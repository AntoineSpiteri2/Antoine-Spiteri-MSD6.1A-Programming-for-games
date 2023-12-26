using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShip : MonoBehaviour
{


    private GameObject childGameObject;



    private void Awake()
    {
        childGameObject = this.gameObject.transform.parent.gameObject;
        Debug.Log(childGameObject);
    }

    void Update()
    {

        gameObject.transform.position = childGameObject.transform.position;
        gameObject.transform.Rotate(0,0,0);
    }
}
