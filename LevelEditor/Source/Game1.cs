using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Input;

namespace LevelEditor;

public class Game1 : Core
{
    private Texture2D _texture;
    private Sandbox _sandbox;
    private Color[] _pixelBuffer;

    public Game1() : base("Dust in the Wind - Level Editor", 1920, 1080, false)
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        int w = 1920 / 4;
        int h = 1080 / 4;
        _sandbox = new(w, h);
        _pixelBuffer = new Color[_sandbox.Pixels.Length];
        _texture = new Texture2D(GraphicsDevice, _sandbox.Pixels.Width, _sandbox.Pixels.Height);

        for (int x = 0; x < _sandbox.Width / 2; x += 2)
        {
            for (int y = 0; y < _sandbox.Height; y += 2)
            {
                _sandbox.Pixels.SetPixel(x, y, new Random().Next(0, 2) == 0 ? Mat.Water : Mat.Sand);
            }
        }

        base.Initialize();
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
#if !__IOS__
        // Exit the game if the Back button (GamePad) or Escape key (Keyboard) is pressed.
        // (iOS does not allow programmatically closing an app.)
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
#endif

        CheckKeyboardInput();

        SandSimulation.Step(_sandbox);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(46, 46, 67, 1));

        var pixels = _sandbox.Pixels.Data;
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

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        SpriteBatch.Draw(
            _texture,           // texture
            new Vector2(0, 0),  // position
            null,               // sourceRectangle
            Color.White,        // color
            0.0f,               // rotation
            Vector2.Zero,       // origin
            4.0f,               // scale
            SpriteEffects.None, // effects
            0.0f                // layerDepth
        );
        SpriteBatch.End();

        base.Draw(gameTime);
    }

    private void CheckKeyboardInput()
    {
        MouseInfo mouse = Input.Mouse;

        if (Input.Mouse.IsButtonDown(MouseButton.Left))
        {
            _sandbox.Pixels.SetPixel(mouse.Position.X / 4, (1080 - 1 - mouse.Position.Y) / 4, Mat.Sand);
        }
    }
}