using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public GameObject selecttemp;
    public static bool speech_sym;
    public static bool stop_sym;
    public static bool dance_sym;
    // Use this for initialization
    void Start()
    {
        speech_sym = false;
        stop_sym = false;
        dance_sym = false;
        keywords.Clear();
        keywords.Add("weather", () =>
        {
            // Call the OnReset method on every descendant object.
            // this.BroadcastMessage("OnReset");
            Debug.Log("weather");
            GameManager.weather_screen.SetActive(true);
        });
        keywords.Add("dance one", () =>
        {
            Debug.Log("dance one");
            speech_sym = true;
            GameManager.selectedButton = this.transform.Find("Canvas").transform.Find("dance1").gameObject;
        });
        keywords.Add("dance two", () =>
        {
            Debug.Log("dance two");
            speech_sym = true;
            GameManager.selectedButton = this.transform.Find("Canvas").transform.Find("dance2").gameObject;
        });
        keywords.Add("model", () =>
        {
            GameManager.selectedButton = this.transform.Find("Canvas").transform.Find("model").gameObject;
        });
        keywords.Add("game", () =>
        {
            GameManager.selectedButton = this.transform.Find("Canvas").transform.Find("game").gameObject;
        });
        keywords.Add("close", () =>
        {
            GameManager.weather_screen.SetActive(false);
        });
        keywords.Add("stop", () =>
        {
            speech_sym = false;
            stop_sym = true;
            GameManager.selectedButton = null;

        });
        keywords.Add("go there", () =>
        {
            GameManager.weather_screen.SetActive(false);
            GameManager.hitpoint = GameManager.rayhitpoint;
            GameManager.selectedButton = selecttemp;
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
    void OnDestroy()
    {
        keywordRecognizer.Stop();
        keywordRecognizer.OnPhraseRecognized -= KeywordRecognizer_OnPhraseRecognized;
    }
}
