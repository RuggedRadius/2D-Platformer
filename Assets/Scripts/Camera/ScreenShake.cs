using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeInterval = 0.005f;
    public float maxShakeDistance;
    public static float currentDuration;

    public void ShakeScreen(float duration, int intensity)
    {
        currentDuration = duration;
        maxShakeDistance = ((float)intensity / 10f);
        StartCoroutine(Shake());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShakeScreen(0.5f, 1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShakeScreen(0.5f, 10);
        }
    }

    private IEnumerator Shake()
    {
        float timer = 0f;
        Vector3 posOrigin = Camera.main.transform.position;
        Vector3 posNextRumble;

        while (timer < currentDuration)
        {
            posNextRumble = new Vector3(
                posOrigin.x + Random.Range(-maxShakeDistance, maxShakeDistance),
                posOrigin.y + Random.Range(-maxShakeDistance, maxShakeDistance),
                posOrigin.z
                );

            Camera.main.transform.position = posNextRumble;

            yield return new WaitForSeconds(shakeInterval);

            timer += Time.deltaTime;
        }

        Camera.main.transform.position = posOrigin;
    }
}
