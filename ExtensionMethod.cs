using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethod
{
    public static IEnumerator PopObj(Transform obj, float speed,AnimationCurve animCurve, float startWaitTime, float endWaitTime , bool isActive)
    {
        yield return new WaitForSeconds(startWaitTime);

        float lerp = 0;
        Vector3 initScale = obj.localScale;

        do
        {
            lerp += Time.deltaTime * speed;
            obj.localScale = initScale * animCurve.Evaluate(lerp);
            yield return new WaitForEndOfFrame();
        } while (lerp < 1);

        yield return new WaitForSeconds(endWaitTime);

        obj.gameObject.SetActive(isActive);

        if(!isActive)
        {
            obj.localScale = initScale;
        }
    }

    public static IEnumerator FadeScreen(Image img, float initVal, float finalVal, float speed, bool isActive)
    {
        float lerp = 0;
        Color color = img.color;

        do
        {
            lerp += Time.deltaTime * speed;
            color.a = Mathf.Lerp(initVal, finalVal, lerp);
            img.color = color;
            yield return new WaitForEndOfFrame();
        } while (lerp < 1);

        img.gameObject.SetActive(isActive);
    }

    public static IEnumerator FadeScreen(CanvasGroup canvasGrp, float initVal, float finalVal, float speed, bool isActive)
    {
        float lerp = 0;

        do
        {
            lerp += Time.deltaTime * speed;
            canvasGrp.alpha = Mathf.Lerp(initVal, finalVal, lerp);
            yield return new WaitForEndOfFrame();
        } while (lerp < 1);

        canvasGrp.gameObject.SetActive(isActive);
    }

}
