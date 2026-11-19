using System.Text;

public class Sandbox
{
    public Pixels Pixels;
    public List<Rigidbody> Rigidbodies;

    public Sandbox(int width, int height)
    {
        Pixels = new Pixels(width, height);
        Rigidbodies = new(128);
    }
}