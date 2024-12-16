using Profe;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShowPage : MonoBehaviour
{
    private TextMeshProUGUI actualPageText;

    void Start()
    {
        actualPageText = GetComponent<TextMeshProUGUI>();   
    }

    // Update is called once per frame
    void Update()
    {
        ShowPageNumber();
    }

    private void ShowPageNumber()
    {
        float pageNumber = FindObjectOfType<UIHandler>().actualPage + 1;

        actualPageText.text = pageNumber.ToString();
    }
}
