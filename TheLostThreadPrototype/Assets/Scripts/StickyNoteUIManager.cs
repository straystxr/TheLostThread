using UnityEngine;
using UnityEngine.UI;

public class StickyNoteUIManager : MonoBehaviour
{
    public static StickyNoteUIManager Instance;

    [Header("UI Prefab & Panel")]
    public GameObject stickyNoteUIPrefab;
    public Transform stickyNotePanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddStickyNoteUI(int noteNumber, Color noteColor)
    {
        GameObject noteUI = Instantiate(stickyNoteUIPrefab, stickyNotePanel);
        Image img = noteUI.GetComponent<Image>();
        if (img != null) img.color = noteColor;

        Text txt = noteUI.GetComponentInChildren<Text>();
        if (txt != null) txt.text = noteNumber.ToString();
    }
}