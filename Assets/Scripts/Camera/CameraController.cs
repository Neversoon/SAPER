using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Camera getMainCamera => mainCamera;

    private float zoomMax = 9.0f;
    private float zoomMin = 3.0f;

    private Vector2 dragScreenOrigin;
    public UserInput userInput { private get; set; }

    private bool isDragging = false;

    private float boardSizeX;
    private float boardSizeY;

    private float lastAspect;

    public float lastDistanceMove { get; private set; } = 10f;

    public void SetBounds(BoardData boardData)
    {
        boardSizeX = boardData.sizeX;
        boardSizeY = boardData.sizeY;

        zoomMin = Mathf.Min(boardSizeX, boardSizeY) / 2f;
        zoomMax = Mathf.Max(boardSizeX, boardSizeY) * 2.5f;

        ResizeCamera();
        lastAspect = mainCamera.aspect;
    }

    private void ResizeCamera()
    {
        if (mainCamera == null) return;

        float targetSize = Mathf.Max(boardSizeY / 2f, (boardSizeX / mainCamera.aspect) / 2f);
        mainCamera.orthographicSize = Mathf.Clamp(targetSize, zoomMin, zoomMax);
    }

    public void Zoom(float value)
    {
        mainCamera.orthographicSize += value * Time.deltaTime;
    }

    public void StartDrag(InputAction.CallbackContext ctx)
    {
        lastDistanceMove = 0f;
        if (userInput.IsPointerOverUI(userInput.screenPosition))
        {
            isDragging = false;
            return;
        }
        dragScreenOrigin = userInput.screenPosition;
        isDragging = true;
    }

    public void StopDrag(InputAction.CallbackContext ctx)
    {
        isDragging = false;
        lastDistanceMove = 0f;
    }

    void Update()
    {
        if (Mathf.Abs(mainCamera.aspect - lastAspect) > 0.01f)
        {
            ResizeCamera();
            lastAspect = mainCamera.aspect;
        }
    }

    void LateUpdate()
    {
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, zoomMin, zoomMax);

        if (!isDragging) return;

        Vector2 currentScreenPos = userInput.screenPosition;

        if (Vector2.Distance(currentScreenPos, dragScreenOrigin) < 1.5f)
            return;

        Vector3 originWorld = mainCamera.ScreenToWorldPoint(dragScreenOrigin);
        Vector3 currentWorld = mainCamera.ScreenToWorldPoint(currentScreenPos);

        Vector3 delta = originWorld - currentWorld;
        delta.z = 0;

        transform.position += delta;

        lastDistanceMove += delta.magnitude;

        float limitX = zoomMax / mainCamera.orthographicSize;
        float limitY = zoomMax / mainCamera.orthographicSize;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -limitX, limitX),
            Mathf.Clamp(transform.position.y, -limitY, limitY),
            transform.position.z
        );

        dragScreenOrigin = currentScreenPos;
    }
}
