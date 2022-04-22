using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crisus
{
    //Name that appears at the top
    public string Name;
    //Text that appears in the description box
    public string Description;
    //the name of the image to use
    public string ImageName;
    //The image used for the center of the panel
    private Sprite Image;
    //the length of days the Crisus has
    public int DayLength;
    //The name of the resultnames
    public List<string> ResultNames;
    //the results possible
    private List<Result> Results;

    //Conversion from string to result
    public Result GetResult(string name)
    {
        //TODO get results from gamemaster
        foreach (Result result in Results)
        {
            if (result.Name == name)
            {
                return result;
            }
        }
        return null;
    }
    public Sprite GetImage()
    {
        //convert imagename in to a sprite
        return Resources.Load<Sprite>("Images/" + ImageName);
    }

    public List<Result> GetResults()
    {
        //convert ResultNames into Results
        List<Result> results = new List<Result>();
        foreach (string resultName in ResultNames)
        {
            results.Add(GetResult(resultName));
        }
        return results;
    }
}
