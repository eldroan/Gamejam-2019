using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using TMPro.Examples;

public class CreditsMenuManager : MonoBehaviour
{
    // Button objects
    //private List<Button> buttonsPanel;

    // Text objects
    private List<TextMeshProUGUI> titleText;

    // Image objects
    private Image fadeImage;

    // Audio objects
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip clipSwitch = null;
    [SerializeField] private AudioClip clipSelect = null;
    [SerializeField] private float delay = 2;

    // Animation objects
    private List<Animator> animations;
	private bool buttonsEnabled = false;
	private int buttonIndex = 0;
	private float fontSizeFactor = 1.2f;
    
    void Awake()
    {
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        //buttonsPanel = GameObject.FindGameObjectWithTag("Canvas").GetComponentsInChildren<Button>().ToList();
        titleText = GameObject.FindGameObjectWithTag("Canvas").GetComponentsInChildren<TextMeshProUGUI>().ToList();
        //titleText.RemoveRange(2, titleText.Count - 2);
        animations = GameObject.Find("Canvas").GetComponentsInChildren<Animator>().ToList();
    }

    void Start()
    {
        StartCoroutine(GenericFunctions.FadeOutImage(.5f, fadeImage, 0f));

        for (int i=0; i < animations.Count; i++)
        {
            animations[i].gameObject.SetActive(false);
        }   
        StartCoroutine(PlayMainMenuAnimation(animations[0], delay + 1.5f));
        StartCoroutine(PlayMainMenuAnimation(animations[1], delay + 2.25f));

        Invoke("EnableMenuButtons", 3.75f);
        StartCoroutine(GenericFunctions.FadeInSound(.5f, audioSource, 1f, .5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!buttonsEnabled)
			return;

		if (Input.GetKeyDown(KeyCode.Escape)) // Only for test mode
		{
            audioSource.PlayOneShot(clipSelect);
            StartCoroutine(GenericFunctions.FadeOutSound(.75f, audioSource, 0f));
			StartCoroutine(GenericFunctions.FadeInImage(.75f, fadeImage, "MainMenu"));
            buttonsEnabled = false;
		}
    }


    // -----------------------------------------------
    // Main Menu Functions
    private IEnumerator PlayMainMenuAnimation(Animator anim, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        anim.gameObject.SetActive(true);
        anim.SetTrigger("playAnim");
        yield return null;
    }

    private void EnableMenuButtons()
	{
		//buttonsPanel[0].GetComponentInChildren<TextMeshProUGUI>().fontSize = buttonsPanel[0].GetComponentInChildren<TextMeshProUGUI>().fontSize * fontSizeFactor;
		//buttonsPanel[0].GetComponentInChildren<VertexJitter>().ShakeText();
        titleText[1].GetComponentInChildren<VertexJitter>().ShakeText();
        titleText[1].fontSize = titleText[1].fontSize * fontSizeFactor;

		buttonsEnabled = true;
	}
}
