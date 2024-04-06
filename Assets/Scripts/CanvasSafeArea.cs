/*
 * CanvasSafeArea:
 * Adapts the canvas size to the safe area of the screen.
 */

using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasSafeArea : MonoBehaviour
{
    [SerializeField] private RectTransform m_safeAreaRect;

    private Rect m_lastSafeArea = Rect.zero;
    private Canvas m_canvas;

    private void Awake()
    {
        m_canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        ApplySafeArea();
    }

    private void Update()
    {
        if (m_lastSafeArea != Screen.safeArea)
        {
            ApplySafeArea();
        }
    }

    private void ApplySafeArea()
    {
        m_lastSafeArea = Screen.safeArea;
        if (m_safeAreaRect == null)
            return;

        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= m_canvas.pixelRect.width;
        anchorMin.y /= m_canvas.pixelRect.height;
        anchorMax.x /= m_canvas.pixelRect.width;
        anchorMax.y /= m_canvas.pixelRect.height;

        m_safeAreaRect.anchorMin = anchorMin;
        m_safeAreaRect.anchorMax = anchorMax;
    }
}