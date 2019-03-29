using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public GameObject test;
    public int danceType = 0;
    public bool weatherButton = false;
    // Use this for initialization
    void Start()
    {
        
        keywords.Add("weather", () =>
        {
            // Call the OnReset method on every descendant object.
            // this.BroadcastMessage("OnReset");
            Debug.Log("weather");
            
            weatherButton = !weatherButton;
        });
        keywords.Add("dance one", () =>
        {
            Debug.Log("dance one");
            
            danceType = 1;
        });
        keywords.Add("dance two", () =>
        {
            Debug.Log("dance two");
            danceType = 2;
        });
        keywords.Add("dance three", () =>
        {
            danceType = 3;
        });
        keywords.Add("dance four", () =>
        {
            danceType = 4;
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
}
