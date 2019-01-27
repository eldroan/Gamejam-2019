using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkestScript : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    public void InAttackMoment()
    {
        playerController.InAttackMoment();
    }

    public void EndBlock()
    {
        playerController.EndBlock();
    }

    public void EndAttack()
    {
        playerController.EndAttack();
    }
}
