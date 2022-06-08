using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectable : MonoBehaviour
{
    public static int PowerUpCount;
    public int powerUpUI;
    public int powerUpboolcount;
    TextMeshProUGUI CollectedGoldText;
    public static bool powerUpBool;
    public Image powerBar;
    Color color;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerUps")
        {
            PowerUpCount++;
            powerUpUI++;
            powerUpboolcount++;
            float Pvalue = other.gameObject.GetComponent<PowerValue>().powerValue;
            powerBar.fillAmount += Pvalue;
            color = powerBar.color;
            color.r += 20;
            color.g += 10;
            powerBar.color = color;
//            Debug.Log("PowerUpCollected");
            ////
            Destroy(other.gameObject);
            if (powerUpboolcount % 5 == 0)
            {
                StartCoroutine(powerup());
            }
        }
    }
    IEnumerator powerup()
    {
        powerUpBool = true;
        yield return new WaitForSeconds(1f);
        powerUpBool = false;
        powerUpboolcount = 0;

    }
    
  
}
