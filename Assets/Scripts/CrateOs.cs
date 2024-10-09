using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateOs : MonoBehaviour
{

    readonly float[] rot = {0, 90, 180, 270};

    // Update is called once per frame
    void Start()
    {
            Vector3 TorqueDir = new(rot[Random.Range(0, rot.Length)], rot[Random.Range(0, rot.Length)], rot[Random.Range(0, rot.Length)]);
            transform.rotation = Quaternion.Euler(TorqueDir);
    }
}
