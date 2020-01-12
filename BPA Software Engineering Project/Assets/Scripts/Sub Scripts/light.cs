using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public Transform Sub;

    private void Update()
    {
        transform.position = new Vector3(Sub.transform.position.x, Sub.transform.position.y, 0);
    }
}
