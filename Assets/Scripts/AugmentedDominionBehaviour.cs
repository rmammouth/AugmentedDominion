using UnityEngine;
using System.Collections;
using EasyAR;
using System.Xml;

namespace AugmentedDominion
{
  public class AugmentedDominionBehaviour : MonoBehaviour
  {
    public GameObject cardPrefab;
    private ImageTrackerBehaviour imageTrackerBehaviour;

    // Use this for initialization
    void Start()
    {
      imageTrackerBehaviour=GameObject.Find("ImageTracker").GetComponent<ImageTrackerBehaviour>();
      TextAsset textAsset = (TextAsset)Resources.Load("cards", typeof(TextAsset));
      XmlDocument xmlCards = new XmlDocument();
      xmlCards.LoadXml(textAsset.text);
      XmlNodeList xmlCardsList=xmlCards.SelectNodes("/Cards/Card");
      foreach (XmlNode xmlCard in xmlCardsList)
      {
        InitCard(xmlCard);
      }
    }

    private void InitCard(XmlNode xmlCard)
    {
      string cardId = xmlCard.Attributes["id"].InnerText;

      Debug.Log("instantiating " + cardId);
      GameObject card=Instantiate(cardPrefab);      
      DominionCardTargetBehaviour behaviour=card.GetComponent<DominionCardTargetBehaviour>();
      behaviour.Path = cardId + ".jpg";
      behaviour.Name = cardId;
      behaviour.Bind(imageTrackerBehaviour);
      behaviour.CardDescription = xmlCard.InnerText;
    }
  }
}