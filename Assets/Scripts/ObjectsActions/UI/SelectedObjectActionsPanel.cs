using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class SelectedObjectActionsPanel : MonoBehaviour {
    [SerializeField] List<UICommandButton> buttons;
    UICommandButton PrevButton => buttons[^2];
    UICommandButton NextButton => buttons[^1];
    List<UICommand> actions;
    [SerializeField] Sprite nextArrow;
    [SerializeField] Sprite prevArrow;
    NextPageAction nextPageAction;
    PrevPageAction prevPageAction;
    public int CurrentPage { get; private set; }
    public int Pages { get; private set; }

    void Awake() {
      GemstoneDictionary empty = new GemstoneDictionary();
      nextPageAction = new NextPageAction(nextArrow, new TooltipData(string.Empty, "Next", empty), this, PrevButton, NextButton);
      prevPageAction = new PrevPageAction(prevArrow, new TooltipData(string.Empty, "Prev", empty), this, PrevButton, NextButton);
    }

    public void UpdateUI(Selectable selected) {
      DisableAllButtons();
      CurrentPage = 0;
      actions = selected.Actions.ToList();
      if (actions.Count < 12) {
        ShowAllOnFirstPage();
      }
      else {
        Pages = Mathf.CeilToInt(actions.Count / 10f);
        ShowFirstPage();
        SetUpPrevPageButton();
        SetUpNextPageButton();
      }
    }

    void ShowAllOnFirstPage() {
      for (int i = 0; i < actions.Count; i++) {
        buttons[i].SetUp(actions[i]);
        buttons[i].gameObject.SetActive(true);
      }
    }

    void ShowFirstPage() {
      for (int i = 0; i < 10; i++) {
        buttons[i].SetUp(actions[i]);
        buttons[i].gameObject.SetActive(true);
      }
    }

    void SetUpPrevPageButton() {
      buttons[10].SetUp(prevPageAction);
      buttons[10].gameObject.SetActive(true);
      buttons[10].DisableButton();
    }

    void SetUpNextPageButton() {
      buttons[11].SetUp(nextPageAction);
      buttons[11].gameObject.SetActive(true);
    }

    public void ShowNextPage() {
      if (CurrentPage < Pages) {
        CurrentPage++;
        DisableActionButtons();
        int actionsLeft = (actions.Count - 10 * CurrentPage) % 11;
        for (int i = 0; i < actionsLeft; i++) {
          buttons[i].SetUp(actions[i + 10 * CurrentPage]);
          buttons[i].gameObject.SetActive(true);
        }
      }
    }

    public void ShowPrevPage() {
      if (CurrentPage > 0) {
        CurrentPage--;
        for (int i = 0; i < 10; i++) {
          buttons[i].SetUp(actions[i + 10 * CurrentPage]);
          buttons[i].gameObject.SetActive(true);
        }
      }
    }

    void DisableActionButtons() {
      for (int i = 0; i < 10; i++) {
        buttons[i].gameObject.SetActive(false);
      }
    }

    void DisableAllButtons() {
      foreach (UICommandButton button in buttons) {
        button.gameObject.SetActive(false);
      }
    }
  }
}
