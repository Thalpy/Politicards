using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrisusBox : MonoBehaviour
{
    //name text
    public TextMesh NameText;
    //description text
    public TextMesh DescriptionText;
    //image
    public SpriteRenderer Image;
    //results
    public List<Result> Results;


    //Feeling variable might comment later
    void ChangeEvent(Crisus SussyCrisis)
    {
        //set the name
        NameText.text = SussyCrisis.Name;
        //set the description
        DescriptionText.text = SussyCrisis.Description;
        //set the image
        Image.sprite = SussyCrisis.GetImage();
        //set the results
        Results = SussyCrisis.GetResults();
    }
}
