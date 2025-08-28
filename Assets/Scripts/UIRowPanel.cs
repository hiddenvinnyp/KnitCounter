using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRowPanel : MonoBehaviour
{
    private TextMeshProUGUI rowText;
    // Start is called before the first frame update
    void Awake()
    {
        rowText = GetComponent<TextMeshProUGUI>();
        SaveSystem.RowChanged += AddRowTextChange;
    }

    private void AddRowTextChange(int currentRow)
    {
        rowText.text = "Ðÿä: " + currentRow.ToString();
    }

    private void OnDisable()
    {
        SaveSystem.RowChanged -= AddRowTextChange;
    }
}
