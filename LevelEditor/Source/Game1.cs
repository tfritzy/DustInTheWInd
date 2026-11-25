using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LevelEditor;

public class Game1 : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _texture;
    private Sandbox _sandbox;
    private Effect _materialShader;
    private Color[] _pixelBuffer;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        int w = 750;
        int h = 400;
        _sandbox = new(w, h);
        _pixelBuffer = new Color[_sandbox.Pixels.Length];

        for (int x = 0; x < _sandbox.Width / 2; x += 2)
        {
            for (int y = 0; y < _sandbox.Height; y += 2)
            {
                _sandbox.Pixels.SetPixel(x, y, Mat.Water);
            }
        }

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _texture = new Texture2D(GraphicsDevice, _sandbox.Pixels.Width, _sandbox.Pixels.Height);
        _materialShader = Content.Load<Effect>("materialShader");
    }

    protected override void Update(GameTime gameTime)
    {
#if !__IOS__
        // Exit the game if the Back button (GamePad) or Escape key (Keyboard) is pressed.
        // (iOS does not allow programmatically closing an app.)
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
#endif

        // TODO: Add your update logic here
        SandSimulation.Step(_sandbox);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        var pixels = _sandbox.Pixels.Data;
        Console.WriteLine(new Color(MatProps.Colors[(int)Mat.Water]));
        var w = pixels.GetLength(0);
        var h = pixels.GetLength(1);
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                var mat = (int)Msk.GetMat(pixels[x, y]);
                _pixelBuffer[(h - y - 1) * w + x] = new Color(MatProps.Colors[mat]);
            }
        }
        _texture.SetData(_pixelBuffer);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_texture, new Vector2(0, 0), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}