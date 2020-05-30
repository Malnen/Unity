using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHide
{
    public static IEnumerator show(GameObject gameObject, Vector3 targetPosition)
    {
        float time = 0.1f;
        float elaspsedTime = 0;

        Vector3 startPosition = gameObject.transform.localPosition;
        Vector3 target1 = new Vector3(targetPosition.x, targetPosition.y - 0.5f, targetPosition.z);
        Vector3 target2 = new Vector3(targetPosition.x, targetPosition.y + 0.5f, targetPosition.z);
        Vector3 scale1 = new Vector3(0.8f, 1, 1);
        Vector3 scale2 = new Vector3(0.9f, 0.8f, 1);

        while (time > elaspsedTime)
        {
            gameObject.transform.localPosition = Vector3.Lerp(startPosition, target1, (elaspsedTime / time));
            gameObject.transform.localScale = Vector3.Lerp(Vector3.one, scale1, (elaspsedTime / time));
            elaspsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.localPosition = target1;
        elaspsedTime = 0;

        while (time > elaspsedTime)
        {
            gameObject.transform.localPosition = Vector3.Lerp(target1, target2, (elaspsedTime / time));
            gameObject.transform.localScale = Vector3.Lerp(scale1, scale2, (elaspsedTime / time));
            elaspsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.localPosition = target2;
        elaspsedTime = 0;

        while (time > elaspsedTime)
        {
            gameObject.transform.localPosition = Vector3.Lerp(target2, targetPosition, (elaspsedTime / time));
            gameObject.transform.localScale = Vector3.Lerp(scale2, Vector3.one, (elaspsedTime / time));
            elaspsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.localPosition = targetPosition;
        yield return 0;
    }

    public static IEnumerator hide(GameObject gameObject)
    {
        float time = 0.1f;
        float elaspsedTime = 0;
        Vector3 startPosition = gameObject.transform.localPosition;
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y + 10, startPosition.z);

        Vector3 target1 = new Vector3(startPosition.x, startPosition.y - 1, startPosition.z);
        Vector3 target2 = new Vector3(startPosition.x, startPosition.y + 1, startPosition.z);
        Vector3 scale1 = new Vector3(0.8f, 1, 1);
        Vector3 scale2 = new Vector3(0.9f, 0.8f, 1);

        while (time > elaspsedTime)
        {
            gameObject.transform.localPosition = Vector3.Lerp(startPosition, target1, (elaspsedTime / time));
            gameObject.transform.localScale = Vector3.Lerp(Vector3.one, scale1, (elaspsedTime / time));
            elaspsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.localPosition = target1;
        elaspsedTime = 0;

        while (time > elaspsedTime)
        {
            gameObject.transform.localPosition = Vector3.Lerp(target1, target2, (elaspsedTime / time));
            gameObject.transform.localScale = Vector3.Lerp(scale1, scale2, (elaspsedTime / time));
            elaspsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.localPosition = target2;
        elaspsedTime = 0;

        while (time > elaspsedTime)
        {
            gameObject.transform.localPosition = Vector3.Lerp(target2, targetPosition, (elaspsedTime / time));
            gameObject.transform.localScale = Vector3.Lerp(scale2, Vector3.one, (elaspsedTime / time));
            elaspsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.localPosition = targetPosition;
        yield return 0;
    }
}
