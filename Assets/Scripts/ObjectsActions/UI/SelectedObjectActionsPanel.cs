using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class SelectedObjectActionsPanel : MonoBehaviour {
    [SerializeField] List<ObjectActionButton> buttons;
    List<ObjectAction> actions;
    [SerializeField] Sprite nextArrow;
    [SerializeField] Sprite prevArrow;
    public int CurrentPage { get; private set; }
    public int Pages { get; private set; }

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
      buttons[10].SetUp(CreatePageAction<PrevPageAction>(icon: prevArrow));
      buttons[10].gameObject.SetActive(true);
      buttons[10].DisableButton();
    }

    PageAction CreatePageAction<T>(Sprite icon) where T : PageAction {
      PageAction action = ScriptableObject.CreateInstance<T>();
      action.actionPanel = this;
      action.prevPageButton = buttons[10];
      action.nextPageButton = buttons[11];
      action.icon = icon;
      return action;
    }

    void SetUpNextPageButton() {
      buttons[11].SetUp(CreatePageAction<NextPageAction>(icon: nextArrow));
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
      foreach (ObjectActionButton button in buttons) {
        button.gameObject.SetActive(false);
      }
    }
  }
}
