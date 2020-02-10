using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStars : MonoBehaviour
{
    private void Awake()
    {
        int starsCount = FindObjectsOfType<FallingStars>().Length;

        if (starsCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
