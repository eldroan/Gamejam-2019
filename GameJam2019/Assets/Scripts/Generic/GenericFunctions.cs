using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;
using TMPro.Examples;
using System;

public class GenericFunctions : MonoBehaviour
{
    // ////////////////////////////////////////////////////////
    // Image Processing Functions
    // --------------------------------------------------------
	// Fade in image with fade time and then load a new scene
    public static IEnumerator FadeInImage(float fadeTime, Image img, string nextScene)
	{
		if (!img.gameObject.activeSelf)
        {
            img.gameObject.SetActive(true);
        }
		
		img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a);
		while (img.color.a < 1f)
		{
			img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + (Time.deltaTime / fadeTime));
			if (img.color.a >= 1f)
			{
				if (nextScene != "")
				{
					SceneManager.LoadScene(nextScene);
				}
				else
				{
					Application.Quit();
				}
				
			}
			yield return null;
		}
	}

	// Fade in image with fade time and then load a new scene
    public static IEnumerator FadeInImage(float fadeTime, Image img, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		if (!img.gameObject.activeSelf)
        {
            img.gameObject.SetActive(true);
        }
		
		img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a);
		while (img.color.a < 1f)
		{
			img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + (Time.deltaTime / fadeTime));
			yield return null;
		}
	}

	// Fade in image with fade time and delay time
	public static IEnumerator FadeOutImage(float fadeTime, Image img, float delayTime)
	{
		if (!img.gameObject.activeSelf)
        {
            img.gameObject.SetActive(true);
        }

		yield return new WaitForSeconds(delayTime);
		img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);

		while (img.color.a > 0f)
		{
			img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - (Time.deltaTime / fadeTime));
			if (img.color.a < 0f)
			{
				img.gameObject.SetActive(false);
			}
			yield return null;
		}
	}

	// ////////////////////////////////////////////////////////
    // Audio Processing Functions
    // --------------------------------------------------------
	// Fade in sound with fade time
	public static IEnumerator FadeInSound(float fadeTime, AudioSource audioSource, float maxVolume, float waitTime)
    {
		audioSource.volume = 0f;
		yield return new WaitForSeconds(waitTime);
        while(audioSource.volume < maxVolume)
        {
            audioSource.volume += Time.deltaTime / fadeTime;
			yield return null;
        }
        audioSource.volume = maxVolume;
    }

	// Fade out sound with fade time
 	public static IEnumerator FadeOutSound(float fadeTime, AudioSource audioSource, float minVolume)
    {
		float startVolume = audioSource.volume;
        while(audioSource.volume > minVolume)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
			yield return null;
        }
        audioSource.volume = minVolume;
    }

	// ////////////////////////////////////////////////////////
    // Text Processing Functions
    // --------------------------------------------------------
	public static IEnumerator ThisFunctionIsOnlyForGGJ(float fadeTime, TextMeshProUGUI text, List<string> whatIsay, float delayTime, float timeTextIn, float timeTextOut)
	{
		yield return new WaitForSeconds(delayTime);
		if (!text.gameObject.activeSelf)
		{
			text.gameObject.SetActive(true);
		}	
	
		for (int i=0; i < whatIsay.Count; i++)
		{
			text.GetComponentInChildren<VertexJitter>().ShakeText();
			text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
			text.text = whatIsay[i];
			
			while (text.color.a < 1f)
			{
				text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / fadeTime));
				yield return null;
			}

			yield return new WaitForSeconds(timeTextIn);

			while (text.color.a > 0f)
			{
				text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / fadeTime));
				yield return null;
			}

			yield return new WaitForSeconds(timeTextOut);
		}
	}

    internal static IEnumerator FadeInImage(float v1, object fadeImage, string v2)
    {
        throw new NotImplementedException();
    }
}