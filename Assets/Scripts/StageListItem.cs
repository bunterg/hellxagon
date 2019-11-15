using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageListItem : MonoBehaviour
{
    public Stage stage;
    public TextMeshProUGUI title;
    // Start is called before the first frame update
    void Start()
    {
        title.text = stage.name;
    }
}
