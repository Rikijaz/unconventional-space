using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeRoomGameMaster : MonoBehaviour
{
    [SerializeField] Player player_;
    [SerializeField] Room room_0_;
    [SerializeField] Room room_1_;
    [SerializeField] Room room_2_;
    [SerializeField] Room room_3_;
    [SerializeField] GameObject room_0_game_obj_;
    [SerializeField] GameObject room_1_game_obj_;
    [SerializeField] GameObject room_2_game_obj_;
    [SerializeField] GameObject room_3_game_obj_;

    private List<Color> color_list_;
    private int color_index_;

    bool changed_room_;
    private int current_room_;
    private int prev_room_;
    enum room { room_0, room_1, room_2, room_3 };
    
    // Start is called before the first frame update
    void Start()
    {
        color_list_ = new List<Color> { Color.yellow, Color.red, Color.blue };
        color_index_ = 0;

        current_room_ = 0;
        prev_room_ = 0;
        changed_room_ = false;

        room_0_.ChangeRoomColor(Color.red);
        room_1_.ChangeRoomColor(Color.blue);
        room_2_.ChangeRoomColor(Color.yellow);
        room_3_.ChangeRoomColor(Color.yellow);
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerCurrentRoom();
        SetRoomColors();

        Debug.Log("Current room: " + current_room_);
        Debug.Log("prev room: " + prev_room_);
        //Debug.Log("Color index: " + color_index_);
        //Debug.Log(changed_room_);
    }

    private void GetPlayerCurrentRoom()
    {
        if (player_.IsTriggering(room_0_game_obj_))
        {
            if (current_room_ != (int)room.room_0)
            {
                prev_room_ = current_room_;
                current_room_ = (int)room.room_0;
                changed_room_ = true;
            }
            else
            {
                changed_room_ = false;
            }
            
        }
        if (player_.IsTriggering(room_1_game_obj_))
        {
            if (current_room_ != (int)room.room_1)
            {
                prev_room_ = current_room_;
                current_room_ = (int)room.room_1;
                changed_room_ = true;
            }
            else
            {
                changed_room_ = false;
            }

        }
        if (player_.IsTriggering(room_2_game_obj_))
        {
            if (current_room_ != (int)room.room_2)
            {
                prev_room_ = current_room_;
                current_room_ = (int)room.room_2;
                changed_room_ = true;
            }
            else
            {
                changed_room_ = false;
            }

        }
        if (player_.IsTriggering(room_3_game_obj_))
        {
            if (current_room_ != (int)room.room_3)
            {
                prev_room_ = current_room_;
                current_room_ = (int)room.room_3;
                changed_room_ = true;
            }
            else
            {
                changed_room_ = false;
            }

        }
    }

    private void SetRoomColors()
    {
        
        if (changed_room_ && ((current_room_ > prev_room_) || (current_room_ == 0 && prev_room_ == 3)))
        {
            IncrementIndex();
        }
        else if (changed_room_ && ((current_room_ < prev_room_) || (current_room_ == 3 && prev_room_ == 0)))
        {
            DecrementIndex();
        }

        switch(current_room_)
        {
            case 0:
                {
                    room_2_.ChangeRoomColor(color_list_[color_index_]);
                    break;
                }

            case 1:
                {
                    //room_0_.ChangeRoomColor(color_list_[DecrementIndex()]);
                    //room_2_.ChangeRoomColor(color_list_[IncrementIndex()]);
                    room_3_.ChangeRoomColor(color_list_[color_index_]);
                    break;
                }

            case 2:
                {
                    room_0_.ChangeRoomColor(color_list_[color_index_]);
                    break;
                }

            case 3:
                {
                    room_1_.ChangeRoomColor(color_list_[color_index_]);
                    break;
                }

        }
    }

    private void IncrementIndex()
    {
        color_index_++;
        color_index_ %= color_list_.Count;
    }

    private void DecrementIndex()
    {
        color_index_--;
        if (color_index_ < 0)
        {
            color_index_ = color_list_.Count - 1;
        }
    }
}
