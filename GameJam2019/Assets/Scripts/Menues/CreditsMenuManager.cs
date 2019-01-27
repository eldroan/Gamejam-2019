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
    private List<string> theTeam;
    private List<string> theRoles;

    // Image objects
    private Image fadeImage;
    private Image imageGGJ;

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
        titleText[0].gameObject.SetActive(false);
        titleText[1].gameObject.SetActive(false);
        imageGGJ = GameObject.Find("GGJ").GetComponent<Image>();
        //titleText.RemoveRange(2, titleText.Count - 2);
        animations = GameObject.Find("Canvas").GetComponentsInChildren<Animator>().ToList();

        theTeam = new List<string>();
        theRoles = new List<string>();

        theTeam.Add("leandro\namarillo");
        theTeam.Add("guido\nbracalenti");
        theTeam.Add("matias\ncordoba");
        theTeam.Add("bruno andres\nscheffer");
        theTeam.Add("matias\nmerino");

        theRoles.Add("characters\ngame design\ndev");
        theRoles.Add("sounds\ngame design\ndev");
        theRoles.Add("fireman\ngame design\ndev");
        theRoles.Add("animations\ngame design\ndev");
        theRoles.Add("background menu art");
    }

    void Start()
    {
        StartCoroutine(GenericFunctions.FadeOutImage(.5f, fadeImage, 0f));
        StartCoroutine(GenericFunctions.FadeInSound(.5f, audioSource, 1f, .5f));

        StartCoroutine(GenericFunctions.FadeInImage(1.25f, imageGGJ, delay));
        StartCoroutine(GenericFunctions.FadeOutImage(1.25f, imageGGJ, delay + 1.75f));
        StartCoroutine(GenericFunctions.ThisFunctionIsOnlyForGGJ(1f, titleText[0], theTeam, delay + 4, 2.75f, .75f));
        StartCoroutine(GenericFunctions.ThisFunctionIsOnlyForGGJ(1f, titleText[1], theRoles, delay + 4, 2.75f, .75f));
        StartCoroutine(GenericFunctions.FadeInImage(3f, imageGGJ, delay + 33f));
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
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
}
