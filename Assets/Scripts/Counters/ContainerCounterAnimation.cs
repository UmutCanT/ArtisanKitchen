using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterAnimation : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private Animator animator;
    [SerializeField] private ContainerCounter counter;

    private void Start()
    {
        counter.OnPlayerOpenContainer += Counter_OnPlayerOpenContainer;
    }

    private void Counter_OnPlayerOpenContainer(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
