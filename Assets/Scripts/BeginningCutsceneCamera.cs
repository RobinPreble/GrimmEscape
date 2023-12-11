using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningCutsceneCamera : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Rotate").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.Translate((Vector3.right * 3) * Time.deltaTime);
    }
}
