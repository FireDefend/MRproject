using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;

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
    public static Vector3 rayhitpoint;

	public int menuChoose;
	public GameObject[] menuButtons;
	private bool menuButtonIfHidden;

	// 0 or 1 to choose different model
	public int modelChoose;

    public GameObject[] danceButtons;
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

	public int gameChoose;
	private bool gameButtonIfHidden;
	public GameObject[] gameButtons;
	private Dictionary<string, int> gameButtonToNum = new Dictionary<string, int>()
	{
		{ "jump", 0},
		{ "survive", 1}

	};

	public GameObject weather_screen;

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
		menuChoose = 0;
        modelChoose = 1;
        danceChoose = 0;
        danceButtonIfHidden = true;
		menuButtonIfHidden = true;
		gameChoose = 0;
		gameButtonIfHidden = true;
		foreach (GameObject button in menuButtons)
		{
			button.SetActive(!menuButtonIfHidden);
		}

        foreach (GameObject button in danceButtons)
        {
            button.SetActive(!danceButtonIfHidden);
        }

		foreach (GameObject button in gameButtons)
		{
			button.SetActive(!danceButtonIfHidden);
		}
		if (this.transform.Find ("stage/Hatsune Miku_PjD/weather_screen")) {
			weather_screen = this.transform.Find ("stage/Hatsune Miku_PjD/weather_screen").gameObject;
		} else {
			weather_screen = null;
		}


        recognizer.Tapped += (args) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
			menuResponse();
            
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
//        Debug.Log(relativeScreenVector + "  relativeScreenVector"); 
		return false;
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


//		test_by_mouse ();
			
    }

	void menuResponse(){
		if (FocusedObject != null)
		{
			hitpoint = rayhitpoint;
			//FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
			selectedButton = FocusedObject;
			// Debug.LogError ("asdasdasdas" + selectedButton.name);
			if(selectedButton.name.Equals("menu"))
			{
				menuButtonIfHidden = !menuButtonIfHidden;
				foreach (GameObject button in menuButtons)
				{
					button.SetActive(!menuButtonIfHidden);
				}
				danceButtonIfHidden = true;
				foreach (GameObject button in danceButtons)
				{
					button.SetActive(!danceButtonIfHidden);
				}
				gameButtonIfHidden = true;
				foreach (GameObject game in gameButtons)
				{
					game.SetActive(!gameButtonIfHidden);
				}
				if (weather_screen != null) {
					weather_screen.SetActive (false);
				}
			}

			if (selectedButton.name.Equals("model"))
			{
				modelChoose = 1 - modelChoose;
				menuButtonIfHidden = true;
				foreach (GameObject button in menuButtons)
				{
					button.SetActive(!menuButtonIfHidden);
				}

			}

			if (selectedButton.name.Equals("dance"))
			{
				danceButtonIfHidden = !danceButtonIfHidden;
				foreach (GameObject button in danceButtons)
				{
					button.SetActive(!danceButtonIfHidden);
				}

			}
			if (selectedButton.name.Equals("weather"))
			{
				
				if (weather_screen != null) {
					weather_screen.SetActive (true);
				}
				menuButtonIfHidden = true;
				foreach (GameObject button in menuButtons)
				{
					button.SetActive(!menuButtonIfHidden);
				}


			}

			if(selectedButton.name.Equals("game"))
			{
				gameButtonIfHidden = !gameButtonIfHidden;
				foreach (GameObject game in gameButtons)
				{
					game.SetActive(!gameButtonIfHidden);
				}
			}

			if (danceButtonToNum.ContainsKey(selectedButton.name))
			{
				menuButtonIfHidden = !menuButtonIfHidden;
				danceChoose = danceButtonToNum[selectedButton.name];
				danceButtonIfHidden = !danceButtonIfHidden;
				foreach (GameObject button in danceButtons)
				{
					button.SetActive(!danceButtonIfHidden);
				}
				foreach (GameObject button in menuButtons)
				{
					button.SetActive(!menuButtonIfHidden);
				}

			}
			if (gameButtonToNum.ContainsKey(selectedButton.name))
			{
				menuButtonIfHidden = !menuButtonIfHidden;
				foreach (GameObject button in menuButtons)
				{
					button.SetActive(!menuButtonIfHidden);
				}
				//pending...
				SceneManager.LoadSceneAsync(selectedButton.name);

			}



		}
	
	}

	void test_by_mouse(){

		var headPosition = Camera.main.transform.position;
		var gazeDirection = Camera.main.transform.forward;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Input.GetMouseButtonDown (0)) {
			if(Physics.Raycast(ray, out hit)){
				FocusedObject = hit.collider.gameObject;
				menuResponse ();
			}

		}
	}
}
