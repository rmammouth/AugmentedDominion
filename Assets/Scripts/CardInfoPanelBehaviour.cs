using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace AugmentedDominion
{
  public class CardInfoPanelBehaviour : MonoBehaviour
  {
    private Text cardNameText;
    private TextMeshProUGUI cardDescriptionText;
    private Scrollbar cardInfoScrollbar;
    private bool autoCloseMode = false;
    private bool autoReplaceMode = true;

    // Use this for initialization
    void Start()
    {
      gameObject.SetActive(true);
      cardNameText = GameObject.Find("CardNameText").GetComponent<Text>();
      cardDescriptionText = GameObject.Find("CardDescriptionText").GetComponent<TextMeshProUGUI>();
      cardInfoScrollbar = GameObject.Find("CardInfoScrollbar").GetComponent<Scrollbar>();
      gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnCardFound(DominionCard card)
    {
      if (!gameObject.activeInHierarchy || autoReplaceMode)
      {
        gameObject.SetActive(true);
        cardNameText.text = card.getName();
        cardDescriptionText.text = card.getDescription();
        cardInfoScrollbar.value = 1;
      }
    }

    public void OnCardLost(DominionCard card)
    {
      if (autoCloseMode)
      {
        gameObject.SetActive(false);
      }
    }

    public void OnCloseButtonClicked()
    {
      gameObject.SetActive(false);
    }
  }
}