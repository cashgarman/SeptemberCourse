using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class DigitalCamera : Grabbable
{
    public AudioSource sound;
    public Light flash;
    public float flashDuration;
    public Camera camera;
    private int pictureCount;

    protected override void Start()
    {
        // Get the most up to date picture count
        pictureCount = PlayerPrefs.GetInt("PictureCount", 0);
    }

    public override void OnTriggerStart()
    {
        base.OnTriggerStart();

        // Play the shutter sound
        sound.Play();

        // Save the screenshot
        flash.enabled = true;
        SaveScreenshot();

        // Flash the camera light for a moment
        StartCoroutine(TurnOffFlash(flashDuration));
    }

    private void SaveScreenshot()
    {
        // Set the active render texture to be the camera's render texture
        RenderTexture.active = camera.targetTexture;
        
        // Force the camera to render
        camera.Render();

        // Grab all the rendered pixels from the camera's render target
        var image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        // Encode the data into the PNG format
        var bytes = image.EncodeToPNG();
        Destroy(image);

        // Increase the number of pictures ever taken
        pictureCount++;
        PlayerPrefs.SetInt("PictureCount", pictureCount);

        // Save the PNG data to a new screenshot
        File.WriteAllBytes($"{Application.dataPath}/../Photos/photo_{pictureCount}.png", bytes);
    }

    private IEnumerator TurnOffFlash(float duration)
    {
        yield return new WaitForSeconds(duration);
        flash.enabled = false;
    }
}
