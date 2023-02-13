using UnityEngine;

public  class CuttingCounterAnimation : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private Animator animator;
    [SerializeField] private CuttingCounter counter;

    private void Start()
    {
        counter.OnCut += Counter_OnCut;
    }

    private void Counter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
