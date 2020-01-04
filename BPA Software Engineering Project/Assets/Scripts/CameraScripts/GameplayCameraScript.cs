using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCameraScript : MonoBehaviour
{
    // gameobject to follow
    [SerializeField]
    private Transform follow;

    // every frame follow the player

    void LateUpdate() {
        // WORKS FINE JUST TURNED OFF UNTIL BACKGROUNDS ARE ADDED
         transform.position = new Vector3(follow.position.x, follow.position.y, transform.position.z);
    }
}
