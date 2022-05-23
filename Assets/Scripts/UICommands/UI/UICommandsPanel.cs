using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bts {
  public class UICommandsPanel : MonoBehaviour {
    [SerializeField] List<UICommandButton> buttons;
    [SerializeField] SimpleUICommandData nextButtonData;
    [SerializeField] SimpleUICommandData prevButtonData;
    Dictionary<Selectable, List<UICommand>> commands;
    int commandsCount;
    NextPageUICommand nextPageCommand;
    PrevPageUICommand prevPageCommand;

    public int CurrentPage { get; private set; }
    public int Pages { get; private set; }

    void Awake() {
      nextPageCommand = new NextPageUICommand(nextButtonData, this, buttons[^2], buttons[^1]);
      prevPageCommand = new PrevPageUICommand(prevButtonData, this, buttons[^2], buttons[^1]);
    }

    public void UpdateUI(List<Selectable> selected) {
      DisableAllButtons();
      CurrentPage = 0;
      commands = selected.ToDictionary(s => s, s => s.UICommands.ToList());
      commandsCount = selected.First().UICommands.Count();
      if (commandsCount <= buttons.Count) {
        ShowAllOnFirstPage();
      }
      else {
        Pages = 1 + commandsCount / (buttons.Count - 2);
        ShowFirstPage();
        SetUpPrevPageButton();
        SetUpNextPageButton();
      }
    }

    void ShowAllOnFirstPage() {
      for (int i = 0; i < commandsCount; i++) {
        buttons[i].SetUp(commands.Select(c => c.Value[i]).ToList());
        buttons[i].gameObject.SetActive(true);
      }
    }

    void ShowFirstPage() {
      for (int i = 0; i < buttons.Count - 2; i++) {
        buttons[i].SetUp(commands.Select(c => c.Value[i]).ToList());
        buttons[i].gameObject.SetActive(true);
      }
    }

    void SetUpPrevPageButton() {
      buttons[^2].SetUp(prevPageCommand);
      buttons[^2].gameObject.SetActive(true);
      buttons[^2].DisableButton();
    }

    void SetUpNextPageButton() {
      buttons[^1].SetUp(nextPageCommand);
      buttons[^1].gameObject.SetActive(true);
    }

    public void ShowNextPage() {
      if (CurrentPage < Pages) {
        CurrentPage++;
        DisableCommandButtons();
        int commandsLeft = (commandsCount - (buttons.Count - 2) * CurrentPage) % (buttons.Count - 1);
        for (int i = 0; i < commandsLeft; i++) {
          buttons[i].SetUp(commands.Select(c => c.Value[i + (buttons.Count - 2) * CurrentPage]).ToList());
          buttons[i].gameObject.SetActive(true);
        }
      }
    }

    public void ShowPrevPage() {
      if (CurrentPage > 0) {
        CurrentPage--;
        for (int i = 0; i < buttons.Count - 2; i++) {
          buttons[i].SetUp(commands.Select(c => c.Value[i + (buttons.Count - 2) * CurrentPage]).ToList());
          buttons[i].gameObject.SetActive(true);
        }
      }
    }

    void DisableCommandButtons() {
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
