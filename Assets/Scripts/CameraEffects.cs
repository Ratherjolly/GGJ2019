using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{

    public static CameraEffects instance;
    [HideInInspector]
    public Camera cam;

    private Vector3 originalPos;
    //CAMERA FOLLOW
    private Transform target;
    private float smoothSpeed = 0.0325F;
    private Vector3 offset = new Vector3(0, 0, -10.0F);
    private bool isFollow;

    void Awake()
    {
        instance = this;
        cam = this.GetComponent<Camera>();
        originalPos = transform.localPosition;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        isFollow = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFollow)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);// * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    /// <summary>
    /// Shake the specified duration and amount.
    /// </summary>
    /// <param name="duration">Duration in this case is how many cycles/steps a capera is going to move</param>
    /// <param name="amount">Amount is how craze the camera is going to move</param>
    public void Shake(int duration, float amount)
    {
        instance.StopAllCoroutines();
        isFollow = false;
        originalPos = transform.localPosition;
        instance.StartCoroutine(instance.cShake(duration, amount));
    }

    public IEnumerator cShake(int duration, float amount)
    {
        while (duration >=0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * amount;
            duration--;
            yield return new WaitForSeconds(0.01F);
        }

        transform.localPosition = originalPos;
        isFollow = true;
        yield return null;
    }
}
