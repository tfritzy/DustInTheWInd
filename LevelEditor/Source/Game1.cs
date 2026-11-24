using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LevelEditor;

public class Game1 : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _batTexture;
    private Sandbox _sandbox;
    private Effect _materialShader;
    private int[] _pixelBuffer;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _sandbox = new(1920, 1080);
        _pixelBuffer = [_sandbox.Pixels.Length];

        for (int x = 0; x < _sandbox.Width; x += 2)
        {
            for (int y = 0; y < _sandbox.Height; y += 2)
            {
                _sandbox.Pixels.SetPixel(x, y, Mat.Sand);
            }
        }


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _batTexture = Content.Load<Texture2D>("bat");
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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Buffer.BlockCopy(_sandbox.Pixels.Data, 0, _pixelBuffer, 0, _sandbox.Pixels.Length);
        // _materialShader.Parameters["Pixels"].SetValue(_pixelBuffer);

        Texture2D texture = new Texture2D(GraphicsDevice, _sandbox.Pixels.Width, _sandbox.Pixels.Height);

        var pixels = _sandbox.Pixels.Data;
        uint[] data = new uint[pixels.GetLength(0) * pixels.GetLength(1)];
        for (int x = 0; x < pixels.GetLength(0); x++)
        {
            for (int y = 0; y < pixels.GetLength(1); y++)
            {
                var mat = Msk.GetMat(pixels[x, y]);
                data[y * pixels.GetLength(0) + x] = MatProps.Props[mat].Color;
            }
        }

        //set the color
        texture.SetData(data);

        _spriteBatch.Begin();
        _spriteBatch.Draw(texture, new Vector2(100, 100), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}