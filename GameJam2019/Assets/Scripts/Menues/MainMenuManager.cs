using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using TMPro.Examples;

public class MainMenuManager : MonoBehaviour
{
    // General Variable
    [SerializeField] private float delay = 2;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip clipSwitch = null;
    [SerializeField] private AudioClip clipSelect = null;

    // Button objects
    private List<Button> buttonsPanel;

    // Text objects
    private List<TextMeshProUGUI> titleText;

    // Image objects
    private Image fadeImage;
	private Image imageGGJ;

    // Animation objects
    private List<Animator> animations;
	private bool buttonsEnabled = false;
	private int buttonIndex = 0;
	private float fontSizeFactor = 1.2f;
    
    void Awake()
    {
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        buttonsPanel = GameObject.FindGameObjectWithTag("Canvas").GetComponentsInChildren<Button>().ToList();
        titleText = GameObject.FindGameObjectWithTag("Canvas").GetComponentsInChildren<TextMeshProUGUI>().ToList();
        titleText.RemoveRange(2, titleText.Count - 2);
        animations = GameObject.Find("Canvas").GetComponentsInChildren<Animator>().ToList();
		imageGGJ = GameObject.Find("GGJ").GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(GenericFunctions.FadeOutImage(.5f, fadeImage, 1f));
        //Invoke("EnableMenuButtons", delay + 3);

        for (int i=0; i < animations.Count; i++)
        {
            animations[i].gameObject.SetActive(false);
        }   
        StartCoroutine(PlayMainMenuAnimation(animations[0], delay + 1.5f));
        StartCoroutine(PlayMainMenuAnimation(animations[1], delay + 2.25f));

        StartCoroutine(PlayMainMenuAnimation(animations[2], delay + 3.25f));
        StartCoroutine(PlayMainMenuAnimation(animations[3], delay + 3.50f));
        StartCoroutine(PlayMainMenuAnimation(animations[4], delay + 3.75f));

		audioSource.PlayDelayed(.3f);	
		StartCoroutine(GenericFunctions.FadeInSound(.4f, audioSource, 1f, .5f));
		StartCoroutine(GenericFunctions.FadeInImage(1.25f, imageGGJ, delay + 5f));

    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape)) // Only for test mode
		{
			Application.Quit();
		}

		if (!buttonsEnabled)
			return;

		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			EnableNextMenuButton();
			return;
		}

		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			EnablePreviousMenuButton();
			return;
		}

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
		{
			SelectMenuButton();
		}
    }


    // -----------------------------------------------
    // Main Menu Functions
    private IEnumerator PlayMainMenuAnimation(Animator anim, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        anim.gameObject.SetActive(true);
        anim.SetTrigger("playAnim");
        if (anim.gameObject.name == "Exit")
        {
            Invoke("EnableMenuButtons", 1f);
        }
        yield return null;
    }

    private void EnableMenuButtons()
	{
		buttonsPanel[0].GetComponentInChildren<TextMeshProUGUI>().fontSize = buttonsPanel[0].GetComponentInChildren<TextMeshProUGUI>().fontSize * fontSizeFactor;
		buttonsPanel[0].GetComponentInChildren<VertexJitter>().ShakeText();
        titleText[1].GetComponentInChildren<VertexJitter>().ShakeText();
        titleText[1].fontSize = titleText[1].fontSize * fontSizeFactor;

		buttonsEnabled = true;
	}

    private void EnableNextMenuButton()
	{
		buttonIndex += 1;
		if (buttonIndex > buttonsPanel.Count-1)
		{
			buttonIndex = buttonsPanel.Count - 1;
		}
		else
		{
			buttonsPanel[buttonIndex - 1].GetComponentInChildren<TextMeshProUGUI>().fontSize = buttonsPanel[buttonIndex - 1].GetComponentInChildren<TextMeshProUGUI>().fontSize / fontSizeFactor;
			buttonsPanel[buttonIndex - 1].GetComponentInChildren<VertexJitter>().StopText();
			buttonsPanel[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().fontSize = buttonsPanel[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().fontSize * fontSizeFactor;
			buttonsPanel[buttonIndex].GetComponentInChildren<VertexJitter>().ShakeText();
		}
		audioSource.PlayOneShot(clipSwitch);
        ChangeButtonText();
	}

	private void EnablePreviousMenuButton()
	{
		buttonIndex -= 1;
		if (buttonIndex < 0)
		{
			buttonIndex = 0;
		}
		else
		{
			buttonsPanel[buttonIndex + 1].GetComponentInChildren<TextMeshProUGUI>().fontSize = buttonsPanel[buttonIndex + 1].GetComponentInChildren<TextMeshProUGUI>().fontSize / fontSizeFactor;
			buttonsPanel[buttonIndex + 1].GetComponentInChildren<VertexJitter>().StopText();
			buttonsPanel[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().fontSize = buttonsPanel[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().fontSize * fontSizeFactor;
			buttonsPanel[buttonIndex].GetComponentInChildren<VertexJitter>().ShakeText();
		}
		audioSource.PlayOneShot(clipSwitch);
        ChangeButtonText();
	}

    private void ChangeButtonText()
    {
        switch (buttonIndex)
        {
            case 0:
                buttonsPanel[0].GetComponentInChildren<TextMeshProUGUI>().text = "fight for tv";
                buttonsPanel[1].GetComponentInChildren<TextMeshProUGUI>().text = "CREDITS";
                buttonsPanel[2].GetComponentInChildren<TextMeshProUGUI>().text = "EXIT";
                break;

            case 1:
                buttonsPanel[0].GetComponentInChildren<TextMeshProUGUI>().text = "FIGHT FOR TV";
                buttonsPanel[1].GetComponentInChildren<TextMeshProUGUI>().text = "credits";
                buttonsPanel[2].GetComponentInChildren<TextMeshProUGUI>().text = "EXIT";
                break;

            case 2:
                buttonsPanel[0].GetComponentInChildren<TextMeshProUGUI>().text = "FIGHT FOR TV";
                buttonsPanel[1].GetComponentInChildren<TextMeshProUGUI>().text = "CREDITS";
                buttonsPanel[2].GetComponentInChildren<TextMeshProUGUI>().text = "exit";
                break;

            default:
                break;
        }
    }

	private void SelectMenuButton()
	{
		buttonsEnabled = false;
		audioSource.PlayOneShot(clipSelect);
		StartCoroutine(GenericFunctions.FadeOutSound(.75f, audioSource, 0f));
		switch (buttonIndex)
		{
			case 0:
				StartCoroutine(GenericFunctions.FadeInImage(.75f, fadeImage, "CharacterSelection"));
				break;

			case 1:
				StartCoroutine(GenericFunctions.FadeInImage(.75f, fadeImage, "Credits"));
				break;

			case 2:
				Debug.Log("Exit Game");
				StartCoroutine(GenericFunctions.FadeInImage(.75f, fadeImage, ""));
				break;

			default:
				Debug.Log("MainMenuManager: error en SelectMenuButton");
				break;
		}
	}
}
