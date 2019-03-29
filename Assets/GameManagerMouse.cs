using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GameManagerMouse : MonoBehaviour
{

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    // variables for menu
    private GameObject selectedButton;
    public GameObject[] danceButtons;
    // 0 or 1 to choose different model
    public int modelChoose;
    // 0, 1, 2, 3 to choose different dance
    public int danceChoose;
    private bool danceButtonIfHidden;
    private Dictionary<string, int> danceButtonToNum = new Dictionary<string, int>()
        {
            { "dance1", 0},
            { "dance2", 1},
            { "dance3", 2},
            { "dance4", 3}
        };
    // Use this for initialization
    void Start()
    {

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        modelChoose = 1;
        danceChoose = 0;
        danceButtonIfHidden = true;
        foreach (GameObject button in danceButtons)
        {
            button.SetActive(!danceButtonIfHidden);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;
        
        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (FocusedObject != null)
            {
                //FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
                selectedButton = FocusedObject;
                print(selectedButton.name);
                if (selectedButton.transform.name=="model")
                {
                    print("sdggs");
                    modelChoose = 1 - modelChoose;
                }
                if (selectedButton.transform.name=="dance")
                {
                    danceButtonIfHidden = !danceButtonIfHidden;
                    foreach (GameObject button in danceButtons)
                    {
                        button.SetActive(!danceButtonIfHidden);
                    }
                }
                if (danceButtonToNum.ContainsKey(selectedButton.name))
                {
                    danceChoose = danceButtonToNum[selectedButton.name];
                }

               
            }
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}
