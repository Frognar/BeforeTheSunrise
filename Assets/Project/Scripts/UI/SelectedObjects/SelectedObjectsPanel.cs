using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace bts {
  public class SelectedObjectsPanel : MonoBehaviour {
    [SerializeField] List<SelectedObjectButton> selectedObjectsButtons;
    [SerializeField] Button next;
    [SerializeField] Button prev;
    List<Selectable> currentSelected;
    int offset;
    int currentSelectedIndex;

    public void SetUI(List<Selectable> selected) {
      currentSelected = selected;
      ResetUI();
      UpdateButtons(offset);
      SetSelectedIndex(currentSelectedIndex);
    }

    void ResetUI() {
      next.interactable = currentSelected.Count > selectedObjectsButtons.Count;
      prev.interactable = false;
      selectedObjectsButtons[currentSelectedIndex].DiselectButton();
      currentSelectedIndex = 0;
      offset = 0;
    }

    void UpdateButtons(int offset) {
      for (int i = 0; i < selectedObjectsButtons.Count; i++) {
        if (i + offset < currentSelected.Count) {
          selectedObjectsButtons[i].gameObject.SetActive(true);
          selectedObjectsButtons[i].SetUp(currentSelected[i + offset]);
        }
        else {
          selectedObjectsButtons[i].gameObject.SetActive(false);
        }
      }
    }

    public void SetSelectedIndex(int index) {
      selectedObjectsButtons[currentSelectedIndex].DiselectButton();
      currentSelectedIndex = index;
      selectedObjectsButtons[currentSelectedIndex].SelectButton();
    }

    public void Next() {
      UpdateButtons(++offset);
      if (currentSelectedIndex == 0) {
        selectedObjectsButtons[currentSelectedIndex].InvokeButtonClick();
      }
      else {
        selectedObjectsButtons[currentSelectedIndex - 1].InvokeButtonClick();
      }

      prev.interactable = true;
      if (offset == currentSelected.Count - selectedObjectsButtons.Count) {
        next.interactable = false;
      }
    }

    public void Prev() {
      UpdateButtons(--offset);
      if (currentSelectedIndex == selectedObjectsButtons.Count - 1) {
        selectedObjectsButtons[currentSelectedIndex].InvokeButtonClick();
      }
      else {
        selectedObjectsButtons[currentSelectedIndex + 1].InvokeButtonClick();
      }
      
      next.interactable = true;
      if (offset == 0) {
        prev.interactable = false;
      }
    }
  }
}
