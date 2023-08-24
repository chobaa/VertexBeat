using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimManager : MonoBehaviour
{
    //Triangle
    [SerializeField] Animator Triangle_112_Anim = null;
    [SerializeField] Animator Triangle_121_Anim = null;
    [SerializeField] Animator Triangle_211_Anim = null;
    [SerializeField] Animator Triangle_05152_Anim = null;
    //Square
    [SerializeField] Animator Square_1111_Anim = null;
    [SerializeField] Animator Square_051511_Anim = null;
    //Pentagon
    [SerializeField] Animator Pentagon_1105105_Anim = null;
    [SerializeField] Animator Hexagon_1105050505_Anim = null;
    [SerializeField] Animator Octagon_0505050505050505_Anim = null;

    string FadeOut = "FadeOut";
    string Bigger = "Bigger";

    public void FadeOut_Animation(int beforeShape)
    {
        if (beforeShape == 31)
        {
            Triangle_112_Anim.SetTrigger(FadeOut);
        }
        else if (beforeShape == 32)
        {
            Triangle_121_Anim.SetTrigger(FadeOut);
        }
        else if (beforeShape == 33)
        {
            Triangle_211_Anim.SetTrigger(FadeOut);
        }
        else if (beforeShape == 34)
        {
            Triangle_05152_Anim.SetTrigger(FadeOut);
        }
        else if (beforeShape == 41)
        {
            Square_1111_Anim.SetTrigger(FadeOut);
        }
        else if (beforeShape == 42)
        {
            Square_051511_Anim.SetTrigger(FadeOut);
        }
        else if (beforeShape == 51)
        {
            Pentagon_1105105_Anim.SetTrigger(FadeOut);
        }
        else if (beforeShape == 61)
        {
            Hexagon_1105050505_Anim.SetTrigger(FadeOut);
        }
        else if (beforeShape == 81)
        {
            Octagon_0505050505050505_Anim.SetTrigger(FadeOut);
        }
    }

    public void Bigger_Animation(int currentShape)
    {
        if (currentShape == 31)
        {
            Triangle_112_Anim.SetTrigger(Bigger);
        }
        else if (currentShape == 32)
        {
            Triangle_121_Anim.SetTrigger(Bigger);
        }
        else if (currentShape == 33)
        {
            Triangle_211_Anim.SetTrigger(Bigger);
        }
        else if (currentShape == 34)
        {
            Triangle_05152_Anim.SetTrigger(Bigger);
        }
        else if (currentShape == 41)
        {
            Square_1111_Anim.SetTrigger(Bigger);
        }
        else if (currentShape == 42)
        {
            Square_051511_Anim.SetTrigger(Bigger);
        }
        else if (currentShape == 51)
        {
            Pentagon_1105105_Anim.SetTrigger(Bigger);
        }
        else if (currentShape == 61)
        {
            Hexagon_1105050505_Anim.SetTrigger(Bigger);
        }
        else if (currentShape == 81)
        {
            Octagon_0505050505050505_Anim.SetTrigger(Bigger);
        }
    }

    public IEnumerator Fadeout(double duration, Image beforeImage, float start, float end)
    {
        var runTime = 0.0f;

        Color fadeColor = beforeImage.color;

        while (runTime < (float)duration)
        {
            runTime += Time.deltaTime;

            fadeColor.a = Mathf.Lerp(start, end, runTime);

            yield return null;
        }
    }
    public IEnumerator FadeIn(double duration, Image currentImage, float start, float end)
    {
        var runTime = 0.0f;

        Color fadeColor = currentImage.color;

        while (runTime < (float)duration)
        {
            runTime += Time.deltaTime;

            fadeColor.a = Mathf.Lerp(start, end, runTime);

            yield return null;
        }
    }
}
