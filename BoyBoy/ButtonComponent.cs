using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonComponent : MonoBehaviour
{
    public UnityEvent interactEvent;
    public Action action;
    public static bool buttonClicked;
    public bool hold;
    public bool over;
    Vector3 colliderSize;
    Vector3 scale;

    private void Start()
    {
        scale = gameObject.transform.localScale;
        if (gameObject.GetComponent<BoxCollider>() != null)
        {
            colliderSize = gameObject.GetComponent<BoxCollider>().size;
        }
    }

    void execute()
    {
        try
        {
            interactEvent.Invoke();
        }
        catch (NullReferenceException e)
        {
            interactEvent = new UnityEvent();
            interactEvent.AddListener(onClick);
            interactEvent.Invoke();
        }
    }
    private void OnMouseUp()
    {
        if (over)
        {
            execute();
        }
        buttonClicked = false;
        hold = false;
    }
    private void OnMouseDown()
    {
        buttonClicked = true;
        hold = true;
        StartCoroutine(onClickAnimation());
    }

    private void OnMouseEnter()
    {
        over = true;
        if (hold)
        {
            StartCoroutine(onClickAnimation());
        }
    }
    private void OnMouseExit()
    {
        over = false;
    }
    void onClick()
    {
        action.Invoke();
    }

    void calculateBoxColliderSize()
    {
        gameObject.GetComponent<BoxCollider>().size = colliderSize / gameObject.transform.localScale.x;
    }

    IEnumerator onClickAnimation()
    {
        float time = 0.05f;
        float elapsedTime = 0;

        Vector3 startScale = scale;
        Vector3 targetScale = new Vector3(startScale.x * 0.7f, startScale.x * 0.7f, 1);

        while (time > elapsedTime)
        {
            gameObject.transform.localScale = Vector3.Lerp(startScale, targetScale, (elapsedTime / time));
            calculateBoxColliderSize();
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        while (hold && over)
        {
            yield return new WaitForEndOfFrame();
        }

        elapsedTime = 0;
        targetScale = startScale;
        startScale = gameObject.transform.localScale;
        calculateBoxColliderSize();

        while (time > elapsedTime)
        {
            gameObject.transform.localScale = Vector3.Lerp(startScale, targetScale, (elapsedTime / time));
            calculateBoxColliderSize();
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.localScale = targetScale;
        calculateBoxColliderSize();

        yield return 0;
    }
}
