using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float bulletTime;
    [SerializeField] private float bulletSpeed;
    private void Start()
    {
        Destroy(gameObject, bulletTime);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemie _StickmanHealth = other.gameObject.GetComponent<Enemie>();
            if (_StickmanHealth!=null)
            {           
                _StickmanHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
       
    }
    
}
