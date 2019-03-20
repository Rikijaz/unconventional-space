using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoomGameMaster : MonoBehaviour
{
    [SerializeField] Player player_;
    [SerializeField] GameObject room_0_;
    [SerializeField] GameObject room_1_;
    [SerializeField] GameObject room_2_;
    [SerializeField] GameObject room_3_;
    [SerializeField] GameObject goal_;
    [SerializeField] GameObject pseudo_wall_;

    private Room room_0_script_;
    private Room room_1_script_;
    private Room room_2_script_;
    private Room room_3_script_;

    private List<Color> color_list_;
    private int color_index_;

    enum room { room_0, room_1, room_2, room_3 };
    private bool changed_room_;
    private int current_room_;
    private int prev_room_;
    
    private bool went_backwards_;
    private bool went_forwards_;

    // Start is called before the first frame update
    void Start()
    {
        room_0_script_ = room_0_.GetComponent<Room>();
        room_1_script_ = room_1_.GetComponent<Room>();
        room_2_script_ = room_2_.GetComponent<Room>();
        room_3_script_ = room_3_.GetComponent<Room>();

        color_list_ = new List<Color> {
            Color.yellow,
            Color.red,
            Color.blue,
            Color.cyan,
            Color.gray,
            Color.green,
            Color.magenta,
            Color.white
        };

        color_index_ = 0;

        current_room_ = 0;
        prev_room_ = 0;
        changed_room_ = false;

        room_0_script_.ChangeRoomColor(Color.red);
        room_1_script_.ChangeRoomColor(Color.blue);
        room_2_script_.ChangeRoomColor(Color.yellow);
        room_3_script_.ChangeRoomColor(Color.yellow);
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerCurrentRoom();
        SetRoomColors();

        Debug.Log("Current room: " + current_room_);
        Debug.Log("prev room: " + prev_room_);
        //Debug.Log("Color index: " + color_index_);
        Debug.Log("b: " + went_backwards_);
        Debug.Log("f: " + went_forwards_);
    }

    private void GetPlayerCurrentRoom()
    {
        if (player_.IsTriggering(room_0_))
        {
            if (current_room_ != (int)room.room_0)
            {
                UpdateRoomHistory((int)room.room_0);
            }
            else
            {
                changed_room_ = false;
            }
            
        }
        if (player_.IsTriggering(room_1_))
        {
            if (current_room_ != (int)room.room_1)
            {
                UpdateRoomHistory((int)room.room_1);
            }
            else
            {
                changed_room_ = false;
            }

        }
        if (player_.IsTriggering(room_2_))
        {
            if (current_room_ != (int)room.room_2)
            {
                UpdateRoomHistory((int)room.room_2);
                pseudo_wall_.SetActive(false);
            }
            else
            {
                changed_room_ = false;
            }

        }
        if (player_.IsTriggering(room_3_))
        {
            if (current_room_ != (int)room.room_3)
            {
                UpdateRoomHistory((int)room.room_3);
            }
            else
            {
                changed_room_ = false;
            }

        }
    }

    private void UpdateRoomHistory(int room)
    {
        Debug.Log("Current room: " + current_room_);
        Debug.Log("prev room: " + prev_room_);

        prev_room_ = current_room_;
        current_room_ = room;
        changed_room_ = true;

        if ((current_room_ > prev_room_)
                || (current_room_ == 0 && prev_room_ == 3))
        {
            went_backwards_ = false;
            went_forwards_ = true;
        }
        else if ((current_room_ < prev_room_) 
            || (current_room_ == 3 && prev_room_ == 0))
        {
            went_backwards_ = true;
            went_forwards_ = false;
        }     
    }

    private void SetRoomColors()
    {
        if (changed_room_ && went_forwards_)
        {
            IncrementIndex();
        }
        else if (changed_room_ && went_backwards_)
        {
            DecrementIndex();
        }

        if (went_forwards_)
        {
            switch (current_room_)
            {
                case 0:
                    {
                        room_2_script_.ChangeRoomColor(color_list_[color_index_]);
                        goal_.transform.position = new Vector3(
                            -12,
                            goal_.transform.position.y,
                            -12);
                        break;
                    }

                case 1:
                    {
                        room_3_script_.ChangeRoomColor(color_list_[color_index_]);
                        goal_.transform.position = new Vector3(
                            2,
                            goal_.transform.position.y,
                            -12);
                        break;
                    }

                case 2:
                    {
                        room_0_script_.ChangeRoomColor(color_list_[color_index_]);
                        goal_.transform.position = new Vector3(
                            2,
                            goal_.transform.position.y,
                            2);
                        break;
                    }

                case 3:
                    {
                        room_1_script_.ChangeRoomColor(color_list_[color_index_]);
                        goal_.transform.position = new Vector3(
                            -12,
                            goal_.transform.position.y,
                            2);
                        break;
                    }

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
