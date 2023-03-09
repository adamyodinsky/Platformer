using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
  public TextMeshProUGUI dialogText;
  public Line[] dialogLines;
  public float textSpeed = 0.1f;
  private int index;
  private RectTransform rectTransform;


  void Start()
  {
    rectTransform = GetComponent<RectTransform>();
    rectTransform.localScale = new Vector3(0, 0, 0);
    dialogText.text = string.Empty;
    index = 0;
  }


  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Return))
    {
      if (dialogText.text == dialogLines[index].Text)
      {
        NextLine();
      }
      else
      {
        StopAllCoroutines();
        dialogText.text = dialogLines[index].Text;
      }
    }
  }

  public void startDialog()
  {
    rectTransform.localScale = new Vector3(1, 1, 1);
    index = 0;
    StartCoroutine(TypeLine());
  }

  IEnumerator TypeLine()
  {
    foreach (char c in dialogLines[index].Text.ToCharArray())
    {
      dialogText.text += c;
      yield return new WaitForSeconds(textSpeed);
    }
  }

  void NextLine()
  {
    if (index < dialogLines.Length - 1)
    {
      index++;
      dialogText.text = string.Empty;
      StartCoroutine(TypeLine());
    }
    else
    {
      dialogText.text = string.Empty;
      rectTransform.localScale = new Vector3(0, 0, 0);
    }
  }
}
