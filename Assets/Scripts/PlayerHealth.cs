using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int shell = 3;

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
            //if you have less than three health ganing a shell will bring you to 4 health
            shell = shell < 3 ? 4 : shell + 1;
            //DO SOMETHING COOL
            Destroy(collision.gameObject);
        }

        ShellCanvas shellCanvas = GameObject.FindObjectOfType<ShellCanvas>();
        //update the shell grid. if the health is less then send 0 (no partial shells)
        shellCanvas.shells = shell - 3 > 0 ? shell - 3 : 0;

        if(shellCanvas.shells >= 9)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
