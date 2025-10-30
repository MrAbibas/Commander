using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform rectTransform;
    private ScreenOrientation lastOrientation;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        lastOrientation = Screen.orientation;
        ApplySafeArea();
    }

    void Update()
    {
        // Перевіряємо, чи змінилася орієнтація екрана
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            ApplySafeArea();
        }
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
