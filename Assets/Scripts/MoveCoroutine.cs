using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MoveCoroutine : MonoBehaviour {

    public enum LerpType { Move, EaseIn, EaseOut, SmoothStep }
    public LerpType lerpType;

    public Vector3 startPos;
    public Vector3 endPos;
    public float speed;

    private void Start() {
        switch (lerpType) {
            case LerpType.Move:
                StartCoroutine(MoveToPosition(1.0f));
                break;
            case LerpType.EaseIn:
                StartCoroutine(EaseIn(1.0f));
                break;
            case LerpType.EaseOut:
                StartCoroutine(EaseOut(1.0f));
                break;
            case LerpType.SmoothStep:
                StartCoroutine(SmoothStep(1.0f, () => {
                    StartCoroutine(EaseOut(1.0f));
                }));
                break;

        }
    }

    IEnumerator MoveToPosition(float delay) {
        transform.position = startPos;
        yield return new WaitForSeconds(1.0f);
        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
    }

    IEnumerator EaseIn(float delay) {
        transform.position = startPos;
        yield return new WaitForSeconds(delay);
        float t = 0;
        float currentLerpTime = 0.0f;
        float lerpTime = 1.0f;
        while (t < 1.0) {
            currentLerpTime += Time.deltaTime * speed;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            t = currentLerpTime / lerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
    }

    IEnumerator EaseOut(float delay) {
        transform.position = startPos;
        yield return new WaitForSeconds(delay);
        float t = 0;
        float currentLerpTime = 0.0f;
        float lerpTime = 1.0f;
        while (t < 1.0) {
            currentLerpTime += Time.deltaTime * speed;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            t = currentLerpTime / lerpTime;
            t = 1.0f - Mathf.Cos(t * Mathf.PI * 0.5f);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
    }

    IEnumerator SmoothStep(float delay, Action onComplete) {
        transform.position = startPos;
        yield return new WaitForSeconds(delay);
        float t = 0;
        float currentLerpTime = 0.0f;
        float lerpTime = 1.0f;
        while(t < 1.0f) {
            currentLerpTime += Time.deltaTime * speed;
            if(currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            t = currentLerpTime / lerpTime;
            t = t * t * (3.0f - 2.0f * t);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        if(onComplete != null) {
            onComplete();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
