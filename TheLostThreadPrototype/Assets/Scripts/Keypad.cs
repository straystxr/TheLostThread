using UnityEngine;

public class Keypad : MonoBehaviour
{
    [Header("Code Settings")]
    [SerializeField] private string correctCode = "1234";
    [SerializeField] private Door door;

    private string currentInput = "";

    public void PressKey(string key)
    {
        if (currentInput.Length >= correctCode.Length)
            return;

        currentInput += key;
        Debug.Log("Input: " + currentInput);

        CheckCode();
    }

    public void Clear()
    {
        currentInput = "";
    }

    private void CheckCode()
    {
        if (currentInput.Length < correctCode.Length)
            return;

        if (currentInput == correctCode)
        {
            Debug.Log("Correct Code!");
            door.OpenDoor();
        }
        else
        {
            Debug.Log("Wrong Code");
            Clear();
        }
    }
}