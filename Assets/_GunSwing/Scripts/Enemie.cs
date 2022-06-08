using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public float StickmanHealth;
    public Animator enemyAnimator;
    public static bool GameStarted = false;
    public bool enemyAllive = true;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    

    // Update is called once per frame
    void Update()
    {
        if (GameStarted == true && enemyAllive == true)
        {
            Debug.Log("GAME ONNNNNNNNNNN");
            enemyAnimator.Play("Running");
            transform.Translate(-transform.forward * enemySpeed * Time.deltaTime); 
        }

        if (PlayerMovement.enemyWins == true)
        {
            Debug.Log("Game FAILEDASFJKMASJMFALSMFAKSLDMASLD");
            enemyAnimator.Play("Swing Dancing");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    
    public void TakeDamage(float amount)
    {
        StickmanHealth -= amount;
        enemyAnimator.Play("Hit");
       // Debug.Log("Mermi yedimmmmmmmmmmmmm sol yanımdaaaaannnnnnnnnn");
        if (StickmanHealth<=0)
        {
            Debug.Log("EnemyDown !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            enemyAllive = false;
            enemyAnimator.Play("Dying");
            gameObject.GetComponent<BoxCollider>().enabled = false;
            skinnedMeshRenderer.material.color = Color.black;
        }
    }
}
