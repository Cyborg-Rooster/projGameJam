using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
class TriggerManager
{
    private bool Up, UpRight, Right, RightDown, Down, DownLeft, Left, LeftUp;
    
    public RaycastHit2D CheckIfExistCollider(Transform Transform, float horizontal, float vertical, string layer)
    {
        SetOrientation(horizontal, vertical);
        Vector3 NextPos = FindNextPosition(Transform.position);
        Debug.DrawLine(Transform.position, NextPos, Color.red);

        var collider = Physics2D.Linecast(Transform.position, NextPos, 1 << LayerMask.NameToLayer(layer));

        return collider;
    }
    private void SetOrientation(float horizontal, float vertical)
    {
        if (!(horizontal == 0 && vertical == 0)) 
        {
            ResetOrientation();
            SetAngle(horizontal, vertical); 
        }
    }

    private void ResetOrientation()
    {
        Up = false;
        UpRight = false; 
        Right = false; 
        RightDown = false; 
        Down = false; 
        DownLeft = false;
        Left = false; 
        LeftUp = false;
    }

    private void SetAngle(float horizontal, float vertical)
    {
        
        if (vertical > 0 && horizontal == 0) Up = true;
        else if (vertical > 0 && horizontal > 0) UpRight = true;
        else if (vertical == 0 && horizontal > 0) Right = true;
        else if (vertical < 0 && horizontal > 0) RightDown = true;
        else if (vertical < 0 && horizontal == 0) Down = true;
        else if (vertical < 0 && horizontal < 0) DownLeft = true;
        else if (vertical == 0 && horizontal < 0) Left = true;
        else if (vertical > 0 && horizontal < 0) LeftUp = true;
    }

    private Vector3 FindNextPosition(Vector3 Position)
    {
        if (Up) return new Vector3(Position.x, Position.y + 0.16f, Position.z);
        if (UpRight) return new Vector3(Position.x + 0.16f, Position.y + 0.16f, Position.z);
        if (Right) return new Vector3(Position.x + 0.16f, Position.y, Position.z);
        if (RightDown) return new Vector3(Position.x + 0.16f, Position.y - 0.16f, Position.z);
        if (Down) return new Vector3(Position.x, Position.y - 0.16f, Position.z);
        if (DownLeft) return new Vector3(Position.x - 0.16f, Position.y - 0.16f, Position.z);
        if (Left) return new Vector3(Position.x - 0.16f, Position.y, Position.z);
        if (LeftUp) return new Vector3(Position.x - 0.16f, Position.y + 0.16f, Position.z);
        else return new Vector3(Position.x, Position.y, Position.z);
    }
}
