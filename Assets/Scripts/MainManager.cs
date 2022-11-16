using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // this is the shared variable between any number of instances this class will have. This is directly accessible from any script.
    public static MainManager Instance { get; private set;  } // getter makes this variable read only & private setter allows to set the value of this variable within this class only.
    public Color teamColor; // now this variable is to store the selected color & pass it to the Main scene, for applying it onto forklift(vehicle). This variable is set in 
                            //"MenuUIHandler.cs"

    private void Awake()
    {
        // as only one instance exist of this class/gameobject, we first check if its null or not. This pattern is called SINGLETON.
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
           
        Instance = this; // store the current instance in this variable.
        DontDestroyOnLoad(gameObject); // telling the compiler to not destroy this gameobject to which this script is attached, when the scene changes.

        LoadColor(); // when the application starts we have to load the savedcolor (if one exists) & we will save the color on exit.
    }

    [System.Serializable] 
    public class SaveData // making this class serializable, we are making it available to JSON format to store the data.
    {
        public Color teamColor; // a variable to store the saved color.
    }

    // to save the color which is selected to JSON format.
    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.teamColor = teamColor; // assigning this's class team color to data's team color.

        // Serialization - storing our data in JSON format & writing it onto a file.
        string json = JsonUtility.ToJson(data); // converting the SaveData's object to JSON format.
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // write string to a file at the provided path & also mention the file name.
    }

    // this will be called when we want to load the stored color in the previous session.
    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json"; // getting the path.
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json); // getting the saved data from Json.

            teamColor = data.teamColor; // assigning the data's retrived teamColor of this instance to teamColor of this instance
        }

    }
}
