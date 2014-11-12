using UnityEngine;
using System;


[Serializable]
public class playerSave  {
    private string nameOfFile;
    public string fileName {
        get { return nameOfFile; }
        set { nameOfFile=value; }
    }
    
}
