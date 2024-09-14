using UnityEngine;
using UnityEngine.UI;

public abstract class Bullet
{
    protected float damage;
    protected Transform startPos;
    protected Vector3 endPos;

    public void Setup(float damage,Transform startPos,Vector3 endPos)
    {
        this.damage = damage;
        this.startPos = startPos;
        this.endPos = endPos;
        Activate();
    }

    protected abstract void Activate();
}