using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Camera getMainCamera => mainCamera;

    private float zoomMax = 12.0f;
    private float zoomMin = 3.0f;

    private Vector2 dragScreenOrigin;
    public UserInput userInput { private get; set; }

    private bool isDragging = false;

    public void SetBounds(BoardData boardData)
    {
        float newSize = Mathf.Max(boardData.sizeX, boardData.sizeY);
        zoomMax = newSize * 2;
        zoomMin = newSize / 2;

        if (Screen.orientation is ScreenOrientation.LandscapeLeft or ScreenOrientation.LandscapeRight)
        {
            newSize *= mainCamera.aspect;
        }

        ResizeCamera(newSize);
    }

    private void ResizeCamera(float value)
    {
        mainCamera.orthographicSize = value;
    }

    public void Zoom(float value)
    {
        mainCamera.orthographicSize += value * Time.deltaTime;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, zoomMin, zoomMax);
    }

    public void StartDrag(InputAction.CallbackContext ctx)
    {
        dragScreenOrigin = userInput.screenPosition;
        isDragging = true;
    }

    public void StopDrag(InputAction.CallbackContext ctx)
    {
        isDragging = false;
    }

    void LateUpdate()
    {
        if (!isDragging) return;

        Vector2 currentScreenPos = userInput.screenPosition;

        if (Vector2.Distance(currentScreenPos, dragScreenOrigin) < 1.5f)
            return;

        Vector3 originWorld = mainCamera.ScreenToWorldPoint(dragScreenOrigin);
        Vector3 currentWorld = mainCamera.ScreenToWorldPoint(currentScreenPos);

        Vector3 delta = originWorld - currentWorld;
        delta.z = 0;

        transform.position += delta;

        dragScreenOrigin = currentScreenPos;
    }
}
