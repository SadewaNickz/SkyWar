using UnityEngine;
using UnityEngine.UI;

public class UIHandController : MonoBehaviour
{
    [Header("Pengaturan Jari")]
    public float pinchThreshold = 0.05f;
    public float clickCooldown = 1f; // Jeda 
    private float nextClickTime = 0f;

    [Header("Referensi UI")]
    public RectTransform cursor; 
    public Button[] interactableButtons; // tombol UI 

    public void UpdateHandUI(Vector2 middleTip, Vector2 thumbTip)
    {
        //kursor ke posisi jari tengah
        Vector2 screenPos = new Vector2(middleTip.x * Screen.width, (1f - middleTip.y) * Screen.height);
        if (cursor != null)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
            worldPos.z = 0f; // Samakan posisinya dengan Canvas
            cursor.position = worldPos;
        }

        // Cek Interaksi (Jari Tengah + Jempol)
        float pinchDistance = Vector2.Distance(middleTip, thumbTip);
        if (pinchDistance < pinchThreshold && Time.unscaledTime >= nextClickTime)
        {
            CheckButtonClick(screenPos);
        }
    }

    private void CheckButtonClick(Vector2 screenPos)
    {
        foreach (Button btn in interactableButtons)
        {
            if (btn.gameObject.activeInHierarchy && 
                RectTransformUtility.RectangleContainsScreenPoint(btn.GetComponent<RectTransform>(), screenPos, Camera.main))
            {
                btn.onClick.Invoke(); 
                nextClickTime = Time.unscaledTime + clickCooldown;
                break; 
            }
        }
    }
}