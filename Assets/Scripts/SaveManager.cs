using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private bool newSave;
    private int currentSave;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetCurrentSave(int saveIndex)
    {
        currentSave = saveIndex;
    }

    public void SetNewSave(bool saveState)
    {
        newSave = saveState;
    }

    public bool IsNewSave()
    {
        return newSave;
    }

    public int GetCurrentSave()
    {
        return currentSave;
    }
}
