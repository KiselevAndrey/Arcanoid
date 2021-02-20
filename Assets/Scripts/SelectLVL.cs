using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLVL : MonoBehaviour
{
    [SerializeField] List<Button> levels;
    [SerializeField] GameOptionsSO gameOptions;

    private void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].name = i + 1 + " LVL Btn";
            Text text = levels[i].GetComponentInChildren<Text>();
            text.text = (i + 1).ToString();
        }
    }

    public void ActivateLVL()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (i + 1 > gameOptions.maxLevel)
                levels[i].interactable = false;
            else
                levels[i].interactable = true;
        }
    }

    public void LoadLVL(Button btn)
    {
        ManagerSceneStatic.LoadScene(LVLNames.IntToLVLName(int.Parse(btn.name[0].ToString())));
    }
}
