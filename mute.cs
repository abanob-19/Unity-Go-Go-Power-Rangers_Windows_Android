using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mute : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle muteToggle;
    public static bool on= false;
    void Start()
    {
         muteToggle.isOn = on;
        // AudioListener.pause = false;
        //DontDestroyOnLoad(gameObject);
        muteToggle.onValueChanged.AddListener(SetMute);
    }
    void SetMute(bool isOn)
    {
        // Set the pause property to the opposite of the toggle's state
        
        AudioListener.pause = !on;
        on = !on;
    }
    // Update is called once per frame

    void Update()
    {
        
    }
}
