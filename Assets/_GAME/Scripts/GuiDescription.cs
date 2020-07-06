using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuiDescription : MonoBehaviour
{

  public string caption { set => captionText.text = value; }
  public string description { set => descriptionText.text = value; }

  public Image iconImage;
  public TMP_Text captionText;
  public TMP_Text descriptionText;
  


  void Update()
  {
    if (!gameObject.activeInHierarchy)
      return;
    if (!Input.GetMouseButtonDown(0))
      return;

    gameObject.SetActive(false);
  }

  public void OnClick(BaseEventData eventData)
	{
    if (((PointerEventData)eventData).button == PointerEventData.InputButton.Right)
      gameObject.SetActive(false);
        
	}
}
