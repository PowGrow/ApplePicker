using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private float _bottom = -6f;


    private void Update()
    {
        if (transform.position.y < _bottom)
        {
            Messenger.Broadcast(GameEvent.APPLE_DROPED);
            Destroy(this.gameObject);
        }
            
    }
}
