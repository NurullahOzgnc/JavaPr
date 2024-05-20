using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterDataBase characterDB;

    public SpriteRenderer artworkSprite;


    private int selectedOption = 0;

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);
    }

    internal object damage(ArrowEntagle arrowEntagle)
    {
        throw new NotImplementedException();
    }

  
}

   

 