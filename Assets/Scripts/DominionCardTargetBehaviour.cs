using UnityEngine;
using UnityEngine.UI;
using EasyAR;

namespace AugmentedDominion
{
  public class DominionCardTargetBehaviour : ImageTargetBehaviour
  {
    protected override void Awake()
    {
      base.Awake();

      TargetFound += OnTargetFound;
      TargetLost += OnTargetLost;
      TargetLoad += OnTargetLoad;
      TargetUnload += OnTargetUnload;
    }

    void OnTargetFound(TargetAbstractBehaviour behaviour)
    {
      Debug.Log("Found: " + Target.Name);
      DominionCard card = DominionCard.getCard(Target.Name);
      if (card == null) Debug.LogError("Card " + Target.Name + " not found in repository");
      else AugmentedDominionBehaviour.Instance.OnCardFound(card);
    }

    void OnTargetLost(TargetAbstractBehaviour behaviour)
    {
      Debug.Log("Lost: " + Target.Name);
      DominionCard card = DominionCard.getCard(Target.Name);
      if (card == null) Debug.LogError("Card " + Target.Name + " not found in repository");
      else AugmentedDominionBehaviour.Instance.OnCardLost(card);
    }

    void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
      Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
      AugmentedDominionBehaviour.Instance.OnCardLoad();
    }

    void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
      Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
      AugmentedDominionBehaviour.Instance.OnCardUnload();
    }
  }
}