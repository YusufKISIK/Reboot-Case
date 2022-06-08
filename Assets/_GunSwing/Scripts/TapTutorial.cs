using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTutorial : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one + (Vector3.one * Mathf.Sin(Time.time) * 0.1f);
    }
}
