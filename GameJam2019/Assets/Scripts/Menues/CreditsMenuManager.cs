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
    private List<string> theTeamA;
    private List<string> theRolesA;
    private List<string> theTeamB;
    private List<string> theRolesB;

    // Image objects
    private Image fadeImage;
    private Image imageGGJ;
    private List<Image> theTeamImagesA;
    private List<Image> theTeamImagesB;

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
        imageGGJ = GameObject.Find("GGJ").GetComponent<Image>();
        //titleText.RemoveRange(2, titleText.Count - 2);
        animations = GameObject.Find("Canvas").GetComponentsInChildren<Animator>().ToList();
        theTeamImagesA = GameObject.Find("TheTeamA").GetComponentsInChildren<Image>().ToList();
        theTeamImagesB = GameObject.Find("TheTeamB").GetComponentsInChildren<Image>().ToList();

        theTeamA = new List<string>();
        theRolesA = new List<string>();
        theTeamB = new List<string>();
        theRolesB = new List<string>();

        theTeamA.Add("leandro\namarillo");
        theTeamB.Add("guido\nbracalenti");
        theTeamA.Add("matias\ncordoba");
        theTeamB.Add("bruno andres\nscheffer");
        theTeamA.Add("matias\nmerino");

        theRolesA.Add("characters\ngame design\ndev");
        theRolesB.Add("sounds\ngame design\ndev");
        theRolesA.Add("fireman\ngame design\ndev");
        theRolesB.Add("animations\ngame design\ndev");
        theRolesA.Add("background art");
    }

    void Start()
    {
        for (int i=0; i < theTeamImagesA.Count; i++)
        {
            theTeamImagesA[i].gameObject.SetActive(false);
        }

        for (int i=0; i < theTeamImagesB.Count; i++)
        {
            theTeamImagesB[i].gameObject.SetActive(false);
        }

        for (int i=0; i < titleText.Count; i++)
        {
            titleText[i].gameObject.SetActive(false);
        }

        StartCoroutine(GenericFunctions.FadeOutImage(.5f, fadeImage, 0f));
        StartCoroutine(GenericFunctions.FadeInSound(.5f, audioSource, 1f, .5f));

        StartCoroutine(GenericFunctions.FadeInImage(1.25f, imageGGJ, delay));
        StartCoroutine(GenericFunctions.FadeOutImage(1.25f, imageGGJ, delay + 2.5f));
        StartCoroutine(GenericFunctions.FadeInImage(3f, imageGGJ, delay + 42f));

        StartCoroutine(GenericFunctions.ThisFunctionIsOnlyForGGJ(1f, titleText[0], theTeamA, delay + 5, 3f, 9f));
        StartCoroutine(GenericFunctions.ThisFunctionIsOnlyForGGJ(1f, titleText[1], theRolesA, delay + 5, 3f, 9f));
        StartCoroutine(GenericFunctions.SuperFadeInImage(1f, theTeamImagesA, delay + 5, 3f, 9f));

        StartCoroutine(GenericFunctions.ThisFunctionIsOnlyForGGJ(1f, titleText[2], theTeamB, delay + 12f, 3f, 9f));
        StartCoroutine(GenericFunctions.ThisFunctionIsOnlyForGGJ(1f, titleText[3], theRolesB, delay + 12f, 3f, 9f));
        StartCoroutine(GenericFunctions.SuperFadeInImage(1f, theTeamImagesB, delay + 12f, 3f, 9f));
        
    }

    // Update is called once per frame
    void Update()
    {
		if (PlayerInputs.GetKeyDown(Constants.PLAYER_1_TAG, Constants.ESCAPE) || PlayerInputs.GetKeyDown(Constants.PLAYER_1_TAG, Constants.SELECT) || PlayerInputs.GetKeyDown(Constants.PLAYER_1_TAG, Constants.ATTACK))
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
