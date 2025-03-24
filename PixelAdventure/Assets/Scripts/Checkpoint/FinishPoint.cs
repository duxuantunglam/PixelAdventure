using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private Animator anim;
    private int playersInTrigger;

    private bool CanFinishLevel()
    {
        if (playersInTrigger == PlayerManager.instance.playerCountWinCondition)
            return true;

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            playersInTrigger++;
            AudioManager.instance.PlaySFX(2);

            anim.SetTrigger("active");
            GameManager.instance.LevelFinished();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
            playersInTrigger--;
    }
}
