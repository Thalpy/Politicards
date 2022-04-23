using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_MouseOverText : MonoBehaviour
{
    // Start is called before the first frame update

[SerializeField] string MouseOverText;

public string GetMouseOverText()
{
    return(MouseOverText);
}

}
