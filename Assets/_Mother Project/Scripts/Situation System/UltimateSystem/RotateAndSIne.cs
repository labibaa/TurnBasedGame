using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndSIne : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationDuration = 2f; // Duration for one rotation (in seconds)
    public float amplitude = 1f; // Amplitude of the sine wave motion
    public float frequency = 1f; // Frequency of the sine wave motion

    void Start()
    {
        // Rotate the GameObject on its axis
        transform.DOLocalRotate(new Vector3(0f, 360f, 0f), rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart); // Loop the rotation indefinitely

        // Apply sine curve motion on the Y-axis
        transform.DOLocalMoveY(Mathf.Sin(Time.time * frequency) * amplitude, rotationDuration / 2f)
            .SetEase(Ease.InOutSine) // Ease the motion with a sine curve
            .SetLoops(-1, LoopType.Yoyo); // Loop the motion back and forth indefinitely
    }
}
