using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds; // An array of the background layers to be parallaxed.
    [SerializeField] private float[] parallaxScales; // The proportion of the camera's movement to move the backgrounds by.
    [SerializeField] private float smoothing = 1f; // How smooth the parallax effect will be.

    private Transform cam; // Reference to the main camera's transform.
    private Vector3 previousCamPos; // The position of the camera in the previous frame.

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPos = cam.position;

        if (backgrounds.Length != parallaxScales.Length)
        {
            Debug.LogError("Backgrounds and Parallax Scales arrays must have the same length.");
        }
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // The parallax is the opposite of the camera movement because the previous frame multiplied by the scale.
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // Set a target x position which is the current position plus the parallax.
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Create a target position which is the background's current position with its target x position.
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Smoothly interpolate between the current position and the target position.
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Set the previousCamPos to the camera's position at the end of the frame.
        previousCamPos = cam.position;
    }
}
