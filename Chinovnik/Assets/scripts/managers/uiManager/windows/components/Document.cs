using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class Document : RectComponent
{
    [SerializeField] private Animator animator;
    [SerializeField] private Text moneyCorruptionText;
    [SerializeField] private GameObject moneyCorruptionGO;
    [SerializeField] private Image stampYes;
    [SerializeField] private Image stampNo;

    [SerializeField] private float defaultStampRot;

    public void ShowNew(int moneyCorruption)
    {
        Clear();
        moneyCorruptionGO.SetActive(moneyCorruption > 0);
        moneyCorruptionText.text = moneyCorruption > 0 ? moneyCorruption + "$" : "";
        animator.SetBool("new", true);
    }

    public void Hide()
    {
        animator.SetBool("new", false);
    }

    public void Clear()
    {
        stampYes.gameObject.SetActive(false);
        stampNo.gameObject.SetActive(false);
    }

    public void SetDecision(bool decision)
    {
        stampYes.transform.rotation = Quaternion.Euler(new Vector3(0, 0, defaultStampRot + Random.Range(-10, 10)));
        stampNo.transform.rotation = Quaternion.Euler(new Vector3(0, 0, defaultStampRot + Random.Range(-10, 10)));
        stampYes.gameObject.SetActive(decision);
        stampNo.gameObject.SetActive(!decision);
    }
}
