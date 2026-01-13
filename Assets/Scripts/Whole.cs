using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whole : MonoBehaviour
{
    bool bFlip = false;
    void Start() {
        BeatManager.instance.Whole += HandleWhole;
    }

    void Move()
    {
        bFlip = !bFlip;
        float loc = this.gameObject.transform.position.y;

        if(bFlip) this.gameObject.transform.position = new Vector3(0,loc,0);
        else this.gameObject.transform.position = new Vector3(2,loc,0);
    }

    void HandleWhole() {
        Move();
    }
}
