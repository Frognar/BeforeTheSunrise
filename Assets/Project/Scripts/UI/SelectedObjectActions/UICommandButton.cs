﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class UICommandButton : MonoBehaviour {
    [SerializeField] Button button;
    [SerializeField] Image icon;
    [SerializeField] TooltipTrigger tooltip;

    public void SetUp(List<UICommand> commands) {
      UICommand first = commands.First();
      tooltip.SetUp(first.TooltipData);
      icon.sprite = first.ButtonIcon;
      button.onClick.AddListener(delegate { commands.ForEach(c => c.Execute()); });
    }

    public void SetUp(UICommand command) {
      tooltip.SetUp(command.TooltipData);
      icon.sprite = command.ButtonIcon;
      button.onClick.AddListener(delegate { command.Execute(); });
    }

    void OnDisable() {
      button.onClick.RemoveAllListeners();
    }

    public void DisableButton() {
      button.interactable = false;
    }

    public void EnableButton() {
      button.interactable = true;
    }
  }
}