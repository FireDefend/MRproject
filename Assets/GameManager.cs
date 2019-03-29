using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;
    public static int model_number = 0;

    public GameObject[] model;
    private Animator ani;
    // variables for menu

    public static GameObject selectedButton=null;
    public static Vector3 hitpoint;
    private Vector3 rayhitpoint;
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
    private Quaternion initRotateOfCam;
    private Quaternion initRotateOfCanvans;
    private Quaternion targetRotate;
    private Vector3 targetVector3;
    private Vector3 initRelative;
    private float lerpSpeed = 0.2f; public GameObject parentCanvas;
    // Use this for initialization
    void Start()
    {
        initRotateOfCanvans = parentCanvas.transform.rotation;
        initRelative = parentCanvas.transform.position - Camera.main.gameObject.transform.position;
        targetRotate = initRotateOfCanvans;
        targetVector3 = parentCanvas.transform.position;

        Instance = this;

        ani = GetComponent<Animator>();
        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        modelChoose = 1;
        danceChoose = 0;
        danceButtonIfHidden = true;
        foreach (GameObject button in danceButtons)
        {
            button.SetActive(!danceButtonIfHidden);
        }

        recognizer.Tapped += (args) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                hitpoint = rayhitpoint;
                //FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
                selectedButton = FocusedObject;
                if (selectedButton.name.Equals("model"))
                {
                    modelChoose = 1 - modelChoose;
                }
                if (selectedButton.name.Equals("dance"))
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
                    danceButtonIfHidden = !danceButtonIfHidden;
                    foreach (GameObject button in danceButtons)
                    {
                        button.SetActive(!danceButtonIfHidden);
                    }
                }


            }
        };
        recognizer.StartCapturingGestures();
    }
    bool checkIfCameraTooFar()
    {
        var camPos = Camera.main.gameObject.transform.position;
        var canPos = parentCanvas.transform.position;

        var tmpTargetRotate = Camera.main.gameObject.transform.rotation;

        var relativeRotation = Quaternion.Inverse(parentCanvas.transform.rotation) * tmpTargetRotate;

        var newRelative = Camera.main.gameObject.transform.rotation * initRelative;
        var tmpTargetVector3 = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.3f, 1.5f));


        var camMiddleWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        var camMiddle = Camera.main.WorldToScreenPoint(camMiddleWorldPoint);

        var screenPos = Camera.main.WorldToScreenPoint(canPos); var relativeScreenVector = screenPos - camMiddle;
        if (relativeScreenVector.x > 00 || relativeScreenVector.x < -1050 || relativeScreenVector.y < -340 || relativeScreenVector.y > 90)
        {
            Debug.Log(Camera.main.WorldToScreenPoint(tmpTargetVector3) + "  target vector "); targetRotate = tmpTargetRotate;
            targetVector3 = tmpTargetVector3;
        }
        Debug.Log(relativeScreenVector + "  relativeScreenVector"); return false;
    }
    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;
        //if(Input.GetMouseButtonDown(0))
        {
            checkIfCameraTooFar();
        }
        parentCanvas.transform.position = Vector3.Lerp(parentCanvas.transform.position, targetVector3, lerpSpeed);
        parentCanvas.transform.rotation = Quaternion.Lerp(parentCanvas.transform.rotation, targetRotate, lerpSpeed);
        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;
        
        

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
            rayhitpoint = hitInfo.point;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
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
