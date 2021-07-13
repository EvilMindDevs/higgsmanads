using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }

    public Sprite[] sprites = new Sprite[0];

    public float animationTime = 0.25f; //quarter of sec
    public int  animationFrame { get; private set; } // other scripts can read but not set

    public bool loop = true;
    private void Awake() // called when this object initialized
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime); // first time : initial time you want to wait
                                                                                    // second time param: every time after 
    }
    private void Advance() //repeatedly called function
    {
        if (!this.spriteRenderer.enabled) // if sprite render is disabled we dont want to continue to animate
        {
            return;
        }
        this.animationFrame++;
        if(this.animationFrame >= this.sprites.Length && this.loop) // if our animation frame is last one(overflowed) loop back to 0 if animation is looping
        {
            this.animationFrame = 0;
        }
        if(this.animationFrame >=0 && this.animationFrame < this.sprites.Length) // update sprite on renderer , ensure animation  less than sprite length
        {                                                                           //make sure to not get index out of range exp
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    public void Restart()
    {
        this.animationFrame = -1;
        Advance();
    }
}
