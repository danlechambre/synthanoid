using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CompletionUIBehaviour : MonoBehaviour
{
#pragma warning disable CS0649
    [SerializeField]
    private TextMeshProUGUI[] textMeshes;
#pragma warning restore CS0649

    [SerializeField]
    private float fadeTime = 0.04f;

    private void Start()
    {
        StartCoroutine(FadeIn(textMeshes));
    }

    IEnumerator FadeIn(TextMeshProUGUI[] textMeshes)
    {
        foreach (var tm in textMeshes)
        {
            while (tm.color.a < 1)
            {
                Color c = tm.color;
                c = new Color(tm.color.r, tm.color.g, tm.color.b, tm.color.a + fadeTime);
                tm.color = c;
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
