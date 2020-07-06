using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
	public class TextConsoleSimulator : MonoBehaviour
	{
		public float speed = 0.1f;
		public float delay = 0;

		private TMP_Text m_TextComponent;
		private bool hasTextChanged;

		private string lastText;

		void Awake()
		{
			m_TextComponent = gameObject.GetComponent<TMP_Text>();
		}


		void Start()
		{
			//StartCoroutine(RevealCharacters(m_TextComponent));
			//StartCoroutine(RevealWords(m_TextComponent));
		}


		void OnEnable()
		{
			lastText = m_TextComponent.text;

			// Subscribe to event fired when text object has been regenerated.
			TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
			StartCoroutine(RevealCharacters(m_TextComponent));
		}

		void OnDisable()
		{
			TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
		}


		// Event received when the text object has changed.
		void ON_TEXT_CHANGED(Object obj)
		{
			if (lastText != m_TextComponent.text)
			{
				hasTextChanged = true;
				lastText = m_TextComponent.text;
				//Debug.Log("Text changed");
			}
		}


		/// <summary>
		/// Method revealing the text one character at a time.
		/// </summary>
		/// <returns></returns>
		IEnumerator RevealCharacters(TMP_Text textComponent)
		{
			if (delay > 0)
				yield return new WaitForSeconds(delay);
			textComponent.ForceMeshUpdate();

			TMP_TextInfo textInfo = textComponent.textInfo;

			int totalVisibleCharacters = textInfo.characterCount; // Get # of Visible Character in text object
			int visibleCount = 0;

			while (true)
			{
				if (hasTextChanged)
				{
					totalVisibleCharacters = textInfo.characterCount; // Update visible character count.
					hasTextChanged = false;
					visibleCount = 0;
				}

				if (visibleCount > totalVisibleCharacters)
				{
					//yield return new WaitForSeconds(1.0f);
					yield return null;
					//visibleCount = 0;
				}

				textComponent.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

				visibleCount += 1;

				yield return new WaitForSeconds(Time.deltaTime * speed);
			}
		}


		/// <summary>
		/// Method revealing the text one word at a time.
		/// </summary>
		/// <returns></returns>
		IEnumerator RevealWords(TMP_Text textComponent)
		{
			textComponent.ForceMeshUpdate();

			int totalWordCount = textComponent.textInfo.wordCount;
			int totalVisibleCharacters = textComponent.textInfo.characterCount; // Get # of Visible Character in text object
			int counter = 0;
			int currentWord = 0;
			int visibleCount = 0;

			while (true)
			{
				currentWord = counter % (totalWordCount + 1);

				// Get last character index for the current word.
				if (currentWord == 0) // Display no words.
					visibleCount = 0;
				else if (currentWord < totalWordCount) // Display all other words with the exception of the last one.
					visibleCount = textComponent.textInfo.wordInfo[currentWord - 1].lastCharacterIndex + 1;
				else if (currentWord == totalWordCount) // Display last word and all remaining characters.
					visibleCount = totalVisibleCharacters;

				textComponent.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

				// Once the last character has been revealed, wait 1.0 second and start over.
				if (visibleCount >= totalVisibleCharacters)
				{
					yield return new WaitForSeconds(1.0f);
				}

				counter += 1;

				yield return new WaitForSeconds(0.1f);
			}
		}

	}
}