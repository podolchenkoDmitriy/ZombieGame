using System.Collections;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CheckDamage());
    }
    IEnumerator CheckDamage()
    {
        while (true)
        {
            if (ZombieController.eating)
            {
                ZombieController.eating = false;
                UIInstance.instance.SetOfLives();
            }
            yield return new WaitForSeconds(0.2f);
        }
       
    }
    
}
