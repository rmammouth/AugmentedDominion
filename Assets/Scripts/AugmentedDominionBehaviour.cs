using UnityEngine;
using UnityEngine.UI;
using EasyAR;
using System.Xml;


namespace AugmentedDominion
{
  public class AugmentedDominionBehaviour : MonoBehaviour
  {
    private static AugmentedDominionBehaviour instance;

    public static AugmentedDominionBehaviour Instance
    {
      get
      {
        return instance;
      }
    }

    public GameObject cardPrefab;
    private ImageTrackerBehaviour imageTrackerBehaviour;    
    private int cardsLoaded = 0;
    
    private Text loadingText;
    private CardInfoPanelBehaviour currentUIBehaviour;

    // Use this for initialization
    void Awake()
    {
      instance = this;

      //init UI
      imageTrackerBehaviour=GameObject.Find("ImageTracker").GetComponent<ImageTrackerBehaviour>();
      loadingText= GameObject.Find("LoadingText").GetComponent<Text>();
      currentUIBehaviour = GameObject.Find("CardInfoPanel").GetComponent<CardInfoPanelBehaviour>();
    //  currentUIBehaviour.gameObject.SetActive(false);

      //load cards.xml
      TextAsset textAsset = (TextAsset)Resources.Load("cards", typeof(TextAsset));
      XmlDocument xmlCards = new XmlDocument();
      xmlCards.LoadXml(textAsset.text);
      XmlNodeList xmlCardsList=xmlCards.SelectNodes("//Card");
      foreach (XmlNode xmlCard in xmlCardsList)
      {
        InitCard(xmlCard);
      }
    }

    private void InitCard(XmlNode xmlCard)
    {
      string cardId = xmlCard.Attributes["id"].InnerText;
      DominionCard card = new DominionCard(cardId, cardId, xmlCard.ParentNode.Attributes["id"].InnerText, xmlCard.InnerText);

      Debug.Log("instantiating " + card.getId());
      GameObject goCard=Instantiate(cardPrefab);      
      DominionCardTargetBehaviour behaviour= goCard.GetComponent<DominionCardTargetBehaviour>();
      behaviour.Path = cardId + ".jpg";
      behaviour.Name = cardId;
      behaviour.Bind(imageTrackerBehaviour);
    }

    public void OnCardLoad()
    {
      cardsLoaded++;
      UpdateLoadingText();
    }

    public void OnCardUnload()
    {
      cardsLoaded--;
      UpdateLoadingText();
    }

    public void OnCardFound(DominionCard card)
    {
      currentUIBehaviour.OnCardFound(card);
    }

    public void OnCardLost(DominionCard card)
    {
      currentUIBehaviour.OnCardLost(card);
    }

    private void UpdateLoadingText()
    {
      if (loadingText != null)
      {
        loadingText.text = cardsLoaded + "/" + DominionCard.getCardsCount() + " cards loaded";
        if (cardsLoaded >= DominionCard.getCardsCount())
        {
          loadingText.enabled = false;
        }
      }
    }
  }
}