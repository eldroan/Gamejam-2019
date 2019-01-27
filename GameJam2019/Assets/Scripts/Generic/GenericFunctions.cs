using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;

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

	// Fade in image with fade time and delay time
	public static IEnumerator FadeOutImage(float fadeTime, Image img, float delayTime)
	{
		if (!img.gameObject.activeSelf)
        {
            img.gameObject.SetActive(true);
        }

		img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);

		yield return new WaitForSeconds(delayTime);

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
}