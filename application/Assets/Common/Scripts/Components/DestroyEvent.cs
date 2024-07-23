internal class DestroyEvent : Event
{
  public override void Invoke()
  {
    Destroy(gameObject);
  }
}
