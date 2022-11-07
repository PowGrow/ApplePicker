using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var currentScore = PlayerPrefs.GetInt("score");
        currentScore++;
        PlayerPrefs.SetInt("score", currentScore);
        Destroy(collision.gameObject);
    }

    private void Update()
    {
        if(Time.timeScale == 1)
        {
            var mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector2(mousePosition2D.x, this.transform.position.y);
        }

    }
}
