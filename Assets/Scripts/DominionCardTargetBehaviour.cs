using UnityEngine;
using UnityEngine.UI;
using EasyAR;

namespace AugmentedDominion
{
  public class DominionCardTargetBehaviour : ImageTargetBehaviour
  {
    private Canvas canvasObject;
    private Text cardNameText;
    private Text cardDescriptionText;
    public string CardDescription { get;  set; }

    protected override void Awake()
    {
      base.Awake();
      canvasObject = GameObject.Find("CardInfoCanvas").GetComponent<Canvas>();
      canvasObject.enabled = false;
      cardNameText = GameObject.Find("CardNameText").GetComponent<Text>();
      cardDescriptionText = GameObject.Find("CardDescriptionText").GetComponent<Text>();
      TargetFound += OnTargetFound;
      TargetLost += OnTargetLost;
      TargetLoad += OnTargetLoad;
      TargetUnload += OnTargetUnload;
    }

    void OnTargetFound(TargetAbstractBehaviour behaviour)
    {
      Debug.Log("Found: " + Target.Name);
      canvasObject.enabled = true;
      cardNameText.text = Target.Name;
      cardDescriptionText.text = CardDescription;
    }

    void OnTargetLost(TargetAbstractBehaviour behaviour)
    {
      Debug.Log("Lost: " + Target.Name);
      canvasObject.enabled = false;
    }

    void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
      Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
    }

    void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
    {
      Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
    }
  }
}