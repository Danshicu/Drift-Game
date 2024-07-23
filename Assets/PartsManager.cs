using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PartsManager : MonoBehaviour
{

    public PartType[] partTypes;


    public void Start()
    {
        SetAllParts();
    }

    public PartType GetPartType(string partName)
    {
        for (int i = 0; i < partTypes.Length; i++)
        {
            if(partTypes[i].partName==partName)
            {
                return partTypes[i];
            }
        }
        Debug.LogWarning("Part name could not be found");
        return null;

    }

    public void SetPartFromName(string partName, int select)
    {
        PartType tempPart = GetPartType(partName);
        if (tempPart == null) return;
        tempPart.selected = select;
        PlayerPrefs.SetInt(tempPart.partName, select);
        for (int i = 0; i < tempPart.parts.Length; i++)
        {
            foreach (GameObject gb in tempPart.parts[i].partsObjects)
            {
                if (i == select)
                {
                    gb.SetActive(true);
                }
                else
                {
                    gb.SetActive(false);
                }
            }
        }
    }

    public void SetPartFromId(PartType partType, int select)
    {
        partType.selected = select;
        PlayerPrefs.SetInt(partType.partName, select);
        for(int i=0; i<partType.parts.Length; i++)
        {
            foreach(GameObject gb in partType.parts[i].partsObjects)
            {
                if (i == select)
                {
                    gb.SetActive(true);
                }
                else
                {
                    gb.SetActive(false);
                }
            }

        }
    }

    private void SetAllParts()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        foreach(PartType   pt in partTypes)
        {
            var partName = pt.partName;
            int selectedItem = PlayerPrefs.GetInt(partName, 0);
            SetPartFromId(pt, selectedItem);
            hash.Add(partName, selectedItem);
        }
        hash.Add("ChosenCar", PlayerPrefs.GetInt("ChosenCar"));

        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public void SetPartsFromHashtable(ExitGames.Client.Photon.Hashtable hash)
    {
        //hash.Remove("ChosenCar")
        foreach (var row in hash)
        {
            SetPartFromName((string)row.Key, (int)row.Value);
        }
    }

}

[System.Serializable]
public class PartType
{
    public string partName;
    public Part[] parts;
    public int selected;
}
[System.Serializable]
public class Part
{
    public GameObject[] partsObjects;
}
