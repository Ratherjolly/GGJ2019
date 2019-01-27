using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int shell = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SUPER basic proximity trigger
        if (collision.CompareTag("ENEMYATTACK"))
        {
            shell -= 1;
            CameraEffects.instance.Shake(8, 0.2F);
        }
        if (collision.CompareTag("SHELL"))
        {
            shell -= 1;
            //DO SOMETHING COOL
            Destroy(collision.gameObject);
        }
    }
}
