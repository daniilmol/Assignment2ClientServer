using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

public class Movable
{
    private PictureBox pictureBox { get; set; }
    static public Form mainForm;
    public Rectangle Bounds
    {
        get { return pictureBox.Bounds; }
    }
    public int Height
    {
        get { return pictureBox.Size.Height; }
    }
    public int Width
    {
        get { return pictureBox.Size.Width; }
    }
    public Size Size
    {
        get { return pictureBox.Size; }
        set
        {
            if (value.Height > 0 && value.Width > 0)
                pictureBox.Size = value;
        }
    }
    public Point Location
    {
        get { return pictureBox.Location; }
        set { pictureBox.Location = value; }
    }
    public Color BackColor
    {
        get { return pictureBox.BackColor; }
        set { pictureBox.BackColor = value; }
    }
    public Movable(Form form)
    {
        pictureBox = new PictureBox();
        form.Controls.Add(pictureBox);

    }
    public Movable()
    {
        pictureBox = new PictureBox();
        mainForm.Controls.Add(pictureBox);
    }
}
