using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageDisplay : MonoBehaviour
{
    public Stage stage;
    public TextMeshProUGUI title;
    public Image previewImage;

    // Start is called before the first frame update
    void Start()
    {
        title.text = stage.name;
        previewImage.sprite = stage.Preview;
    }

    public void setStage(Stage stage)
    {
        this.stage = stage;
        title.text = stage.name;
        previewImage.sprite = stage.Preview;
    }
}
