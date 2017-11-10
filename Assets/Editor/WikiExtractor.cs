using UnityEngine;
using UnityEditor;
using System.Xml;
using System;
using System.Net;

public class WikiExtractor : ScriptableObject
{
  static string WIKI_URL = "http://wiki.dominionstrategy.com";

  [MenuItem("Tools/Augmented Dominion/Extract Wiki")]
  static void ExtractWiki()
  {    
    try
    {
      Debug.Log("Loading index...");
      XmlDocument xmlWikiCardsIndex = new XmlDocument();
      xmlWikiCardsIndex.Load(WIKI_URL + "/index.php/Template:Navbox_Cards");
      Debug.Log("Index loaded");
      
      XmlDocument xmlCardsIndex = new XmlDocument();
      XmlDeclaration xmlDeclaration = xmlCardsIndex.CreateXmlDeclaration("1.0", "UTF-8", null);
      XmlElement root = xmlCardsIndex.DocumentElement;
      xmlCardsIndex.InsertBefore(xmlDeclaration, root);
      XmlElement elmCards = xmlCardsIndex.CreateElement("Cards");
      xmlCardsIndex.AppendChild(elmCards);

      int extractedCards = 0;
      XmlNodeList xmlSetsList = xmlWikiCardsIndex.SelectNodes("//th[@style='min-width:100px;background-color:#CCF;;']/a");
      foreach (XmlNode xmlSet in xmlSetsList)
      {
        extractedCards += ExtractSet(xmlSet, elmCards);
      }

      xmlCardsIndex.Save("Assets\\Resources\\cards.xml");
      EditorUtility.DisplayDialog("Augmented Dominion", extractedCards + " cards extracted", "OK", "");
    }
    catch (Exception ex)
    {
      Debug.LogError(ex);
    }

  }


  static int ExtractSet(XmlNode xmlSetA, XmlElement elmCards)
  {
    string setName = xmlSetA.InnerText;
    if ((setName != "Dominion") && (setName != "Hinterlands")) return 0;
    Debug.Log(setName + " => " + xmlSetA.Attributes["href"].InnerText);

    XmlElement elmSet = elmCards.OwnerDocument.CreateElement("Set");
    elmSet.SetAttribute("id", setName);
    elmCards.AppendChild(elmSet);

    XmlNode xmlSetTR = xmlSetA.ParentNode.ParentNode;
    XmlNodeList xmlCardsList = xmlSetTR.SelectNodes("./td//a");
    int extractedCards = 0;
    foreach (XmlNode xmlCard in xmlCardsList)
    {
      if (xmlCard.Attributes["title"] != null)
      {
        extractedCards+=ExtractCard(xmlCard, elmSet);
     //   if (extractedCards >= 3) break;
      }
    }
    return extractedCards;
  }


  static int ExtractCard(XmlNode xmlCardA, XmlElement elmSet)
  {
    string cardName = xmlCardA.Attributes["title"].InnerText;
    if (cardName == "Removed cards") return 0;
    string url= xmlCardA.Attributes["href"].InnerText;

    XmlElement elmCard = elmSet.OwnerDocument.CreateElement("Card");
    elmCard.SetAttribute("id", cardName);    
    elmSet.AppendChild(elmCard);

    XmlDocument xmlCardPage = new XmlDocument();
    xmlCardPage.Load(WIKI_URL + xmlCardA.Attributes["href"].InnerText);

    //text
    string text = "";
    XmlNode xmlUlFAQ = xmlCardPage.SelectSingleNode("//div[@id='mw-content-text']/ul");
    foreach (XmlNode xmlLiFAQ in xmlUlFAQ.ChildNodes)
    {
      text += "*"+xmlLiFAQ.InnerText;
    }
    elmCard.InnerText = text;

    //image
    XmlNode xmlDivThumbnail = xmlCardPage.SelectSingleNode("//div[@class='thumbinner']/a/img");
    string pathImage = xmlDivThumbnail.Attributes["src"].InnerText;
    Debug.Log(cardName+" => "+ pathImage);
    using (WebClient client = new WebClient())
    {
      client.DownloadFile(WIKI_URL+pathImage, "Assets\\StreamingAssets\\"+cardName+".jpg");
    }

    return 1;
  }
}