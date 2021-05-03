using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Navigator : Selectable, IEventSystemHandler {

    [Header("Navigator")]    
    [SerializeField] private Animator leftArrow;
    [SerializeField] private Animator rightArrow;
    [SerializeField] private TextMeshProUGUI display;

    [Space] [Tooltip("Send current index")]
    //[System.Serializable]
    [SerializeField] private UnityEvent<int> onValueChange;

    private string[] displayPackage;
    private int index;
    private bool leftEnd;
    private bool rightEnd;

    private const string END = "End";
    public void Init(string[] stringPackege, int currentIndex) {
        if(currentIndex < 0 || currentIndex >= stringPackege.Length) {
            Debug.LogError($"Index out of range. Length: {stringPackege.Length}, Current Index: {currentIndex}");
            return;
        }

        index = currentIndex;
        displayPackage = stringPackege;
        display.text = displayPackage[index];

        if (currentIndex == 0)
            leftEnd = true;

        if (currentIndex == displayPackage.Length - 1)
            rightEnd = true;

        ResetAnimations();
    }
    protected override void OnEnable() {
        base.OnEnable();
        ResetAnimations();
    }

    private void ResetAnimations() {
        if (leftEnd)
            leftArrow.Play(Constants.Animator.Disabled);
        else
            leftArrow.Play(Constants.Animator.Normal);

        if (rightEnd)
            rightArrow.Play(Constants.Animator.Disabled);
        else
            rightArrow.Play(Constants.Animator.Normal);
    }
    public override Selectable FindSelectableOnLeft() {     
        GoLeft();
        return this;
    }

    public override Selectable FindSelectableOnRight() {
        GoRight();
        return this;
    }
    private void GoLeft() {
        if (index - 1 < 0)
            return;

        index--;
        display.text = displayPackage[index];

        if (index == 0) {
            leftEnd = true;
            leftArrow.SetTrigger(END);
        }
        rightEnd = false;

        leftArrow.Play(Constants.Animator.Pressed);
        rightArrow.Play(Constants.Animator.Normal);

        UpdateAndInvoke();
    }

    public void GoRight() {
        if (index + 1 >= displayPackage.Length)
            return;

        index++;

        if (index == displayPackage.Length - 1) {
            rightEnd = true;
        }
        leftEnd = false;

        leftArrow.Play(Constants.Animator.Normal);
        rightArrow.Play(Constants.Animator.Pressed);

        UpdateAndInvoke();
    }

    private void UpdateAndInvoke() {
        display.text = displayPackage[index];

        leftArrow.SetBool(END, leftEnd);
        rightArrow.SetBool(END, rightEnd);

        onValueChange.Invoke(index);
    }

}
