using System;
using System.Numerics;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public int Row {  get { return row; } set { row = value; } }
    public int KnitStitch { get { return knitStitch; } set { knitStitch = value; } }
    public static event Action<int> RowChanged;
    public static event Action<int> StitchChanged;

    private int row = 0;
    private int knitStitch = 1;
    private string knitStitchName = "KnitStitchCount";
    private string rowName = "RowCount";

    public static SaveSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

        // Start is called before the first frame update
    void Start()
    {
        row = LoadRow();
        knitStitch = LoadKnitStitch();
        Button.AddRowPressed += RowSave;
        ToggleButtons.OnStitchClicked += StitchSave;
        RowChanged?.Invoke(row);
        StitchChanged?.Invoke(knitStitch);
    }

    private void StitchSave(int stitch)
    {
        SaveKnitStitch(stitch);
    }

    private void RowSave(int factor)
    {
        int currentRow = LoadRow() + factor;
        if (currentRow < 0) currentRow = 0;
        SaveRow(currentRow);
        RowChanged?.Invoke(currentRow);
    }

    public int LoadKnitStitch()
    {
        return PlayerPrefs.GetInt(knitStitchName, 1);
    }

    public int LoadRow()
    {
        return PlayerPrefs.GetInt(rowName, 0);
    }

    public void SaveRow(int currentRow)
    {
        if (currentRow < 0) currentRow = 0;
        PlayerPrefs.SetInt(rowName, currentRow);
        PlayerPrefs.Save();
    }

    public void SaveKnitStitch(int loop)
    {
        if (loop < 0 || loop > 1) { return; }
        PlayerPrefs.SetInt(knitStitchName, loop);
        PlayerPrefs.Save();
    }

    public void Reset()
    {
        PlayerPrefs.SetInt(knitStitchName, 1);
        PlayerPrefs.SetInt(rowName, 0);
        PlayerPrefs.Save();

        RowChanged?.Invoke(0);
        StitchChanged?.Invoke(1);
    }

    private void OnDestroy()
    {
        Button.AddRowPressed -= RowSave;
        ToggleButtons.OnStitchClicked -= StitchSave;
    }
}
