public abstract class Bullet
{
    protected float damage;

    public void Setup(float damage)
    {
        this.damage = damage;
        Activate();
    }

    protected abstract void Activate();
}