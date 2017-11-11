using System.Collections.Generic;

namespace AugmentedDominion
{
  public class DominionCard
  {
    private static Dictionary<string, DominionCard> cards = new Dictionary<string, DominionCard>();

    private string id;
    private string name;
    private string setName;
    private string description;

    public DominionCard(string id, string name, string setName, string description)
    {
      this.id = id;
      this.name = name;
      this.setName = setName;
      this.description = description;
      cards[id] = this;
    }

    public string getId()
    {
      return id;
    }

    public string getName()
    {
      return name;
    }

    public string getDescription()
    {
      return description;
    }

    public string getSetName()
    {
      return setName;
    }


    public static DominionCard getCard(string id)
    {
      return cards[id];
    }


    public static int getCardsCount()
    {
      return cards.Count;
    }
  }
}