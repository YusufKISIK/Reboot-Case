using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    [SerializeField] private float EnemySpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.forward * EnemySpeed * Time.deltaTime);
    }
}
