using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float zoomMax = 12.0f;
    float zoomMin = 3.0f;
    [SerializeField] Camera mainCamera;
    
    public Camera getMainCamera => mainCamera;
    
    public void SetBounds(BoardData boardData)
    {
        float newSize = Mathf.Max(boardData.sizeX, boardData.sizeY);

        zoomMax = newSize * 2;
        zoomMin = newSize / 2;

        ScreenOrientation screenOrientation = Screen.orientation;

        if (screenOrientation == ScreenOrientation.LandscapeLeft || screenOrientation == ScreenOrientation.LandscapeRight)
        {
            newSize *= mainCamera.aspect;
        }

        ResizeCamera(newSize);
    }

    void ResizeCamera(float value)
    {
        mainCamera.orthographicSize = value;
    }

    public void Zoom(float value)
    {
        mainCamera.orthographicSize += value * Time.deltaTime;

        mainCamera.orthographicSize = Math.Clamp(mainCamera.orthographicSize, zoomMin, zoomMax);
    }
    
}
