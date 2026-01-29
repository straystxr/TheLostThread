using System.Collections;
using System.IO;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    [Header("Autosave")]
    public float autosaveInterval = 120f; // 2 minutes
    public SaveUIAnimator saveUI;

    private string savePath;
    private CheckpointData currentData;

    public Transform playerTransform;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        savePath = Application.persistentDataPath + "/save.json";
    }

    private void Start()
    {
        StartCoroutine(AutoSaveRoutine());
    }

    IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autosaveInterval);
            Save();
        }
    }

    public void SetCheckpoint(Vector3 position)
    {
        Debug.Log($"Checkpoint Set!{Time.time}");
        if (currentData == null)
            currentData = new CheckpointData();

        currentData.playerPosition = position;
        Save();
    }

    public void Save()
    {
        if (currentData == null) return;

        string json = JsonUtility.ToJson(currentData, true);
        File.WriteAllText(savePath, json);

        if (saveUI)
            saveUI.PlaySaveAnimation();

        Debug.Log("Game Saved");
    }

    public Vector3 LoadPosition()
    {
        if (!File.Exists(savePath)) return playerTransform
            ? playerTransform.position
            : Vector3.zero;

        string json = File.ReadAllText(savePath);
        currentData = JsonUtility.FromJson<CheckpointData>(json);

        return currentData.playerPosition;
    }
}

[System.Serializable]
public class CheckpointData
{
    public Vector3 playerPosition;
}